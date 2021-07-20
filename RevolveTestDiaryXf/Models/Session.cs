using ReactiveUI;
using RevolveTestDiaryXf.Enums;
using RevolveTestDiaryXf.Interfaces;
using RevolveTestDiaryXf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolveTestDiaryXf.Models
{
    public class Session : ViewModelBase, ISession
    {
        public DateTime Timestamp { get; set; }
        public string Title { get; set; }
        public ObservableCollection<IDiaryEntry> SessionEntries { get; set; }

        private string newEntryBody;

        public string NewEntryBody
        {
            get { return newEntryBody; }
            set { this.RaiseAndSetIfChanged(ref newEntryBody, value); }
        }

        private EntryType entryType;

        public EntryType NewEntryType
        {
            get { return entryType; }
            set { entryType = value; }
        }

        public ObservableCollection<EntryType> EntryTypes => new ObservableCollection<EntryType>(Enum.GetValues(typeof(EntryType)).Cast<EntryType>());

        public Session(string title)
        {
            Timestamp = DateTime.Now;
            Title = title;
            SessionEntries = new ObservableCollection<IDiaryEntry>();
        }

        public void AddDiaryEntryCommand()
        {
            var diaryEntry = new DiaryEntry(NewEntryType, NewEntryBody);
            NewEntryBody = null;
            AddDiaryEntry(diaryEntry);
        }

        public void AddDiaryEntry(IDiaryEntry entry)
        {
            SessionEntries.Add(entry);
            this.RaisePropertyChanged(nameof(SessionEntries));
        }
    }
}
