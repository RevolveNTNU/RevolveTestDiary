using RevolveTestDiaryXf.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace RevolveTestDiaryXf.Models
{
    public class DiaryEntry
    {
        public DateTime Timestamp { get; set; }
        public event EventHandler<DiaryEntry> TriggerAutoSaveEvent;
        public event EventHandler<DiaryEntry> DeleteMeEvent;

        public DiaryEntry(DateTime timestamp, EntryType entryType, string body)
        {
            Timestamp = timestamp;
            EntryType = entryType;
            Body = body;
        }
        public DiaryEntry()
        {

        }

        public string TimestampString => Timestamp.ToShortTimeString();

        [JsonIgnore]
        public ObservableCollection<EntryType> EntryTypes => new ObservableCollection<EntryType>(Enum.GetValues(typeof(EntryType)).Cast<EntryType>());
        public EntryType EntryType { get => entryType; set { entryType = value; TriggerAutoSaveEvent?.Invoke(this, this); } }

        private string _body;
        private EntryType entryType;

        public string Body
        {
            get { return _body; }
            set { _body = value; TriggerAutoSaveEvent?.Invoke(this, this); }
        }


        public DiaryEntry(EntryType entryType, string body)
        {
            Timestamp = DateTime.Now;
            EntryType = entryType;
            Body = body;
        }


        public void SelfDestructCommand()
        {
            DeleteMeEvent?.Invoke(this, this);
        }
    }
}
