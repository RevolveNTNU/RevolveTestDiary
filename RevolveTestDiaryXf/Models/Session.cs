using ReactiveUI;
using RevolveTestDiaryXf.Enums;
using RevolveTestDiaryXf.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace RevolveTestDiaryXf.Models
{
    public class Session : ViewModelBase
    {
        public DateTime Timestamp { get; set; }

        public string Title { get; set; }
        public ObservableCollection<DiaryEntry> SessionEntries { get; set; }

        private string newEntryBody;
        [JsonIgnore]
        public string NewEntryBody
        {
            get { return newEntryBody; }
            set { this.RaiseAndSetIfChanged(ref newEntryBody, value); }
        }

        private EntryType entryType;

        public event EventHandler<Session> TriggerAutoSaveEvent;
        [JsonIgnore]
        public EntryType NewEntryType
        {
            get { return entryType; }
            set { entryType = value; }
        }

        [JsonIgnore]
        public ObservableCollection<EntryType> EntryTypes => new ObservableCollection<EntryType>(Enum.GetValues(typeof(EntryType)).Cast<EntryType>());

        public Session(string title)
        {
            Timestamp = DateTime.Now;
            Title = title;
            SessionEntries = new ObservableCollection<DiaryEntry>();
        }

        public Session() { }

        public void AddDiaryEntryCommand()
        {
            var diaryEntry = new DiaryEntry(NewEntryType, NewEntryBody);
            NewEntryBody = null;
            AddDiaryEntry(diaryEntry);
            TriggerAutoSaveEvent?.Invoke(this, this);
        }

        public void AddDiaryEntry(DiaryEntry entry)
        {
            SessionEntries.Add(entry);
            this.RaisePropertyChanged(nameof(SessionEntries));
        }
    }
}
