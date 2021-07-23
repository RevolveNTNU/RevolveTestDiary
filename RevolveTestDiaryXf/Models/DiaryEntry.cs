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
        public EntryType EntryType { get; set; }

        public string Body { get; set; }

        public DiaryEntry(EntryType entryType, string body)
        {
            Timestamp = DateTime.Now;
            EntryType = entryType;
            Body = body;
        }
    }
}
