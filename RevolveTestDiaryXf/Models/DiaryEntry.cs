using RevolveTestDiaryXf.Enums;
using RevolveTestDiaryXf.Interfaces;
using System;

namespace RevolveTestDiaryXf.Models
{
    public class DiaryEntry : IDiaryEntry
    {
        public DateTime Timestamp { get; set; }

        public string TimestampString => Timestamp.ToShortTimeString();

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
