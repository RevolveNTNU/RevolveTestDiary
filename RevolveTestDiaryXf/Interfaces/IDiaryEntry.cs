using RevolveTestDiaryXf.Enums;

namespace RevolveTestDiaryXf.Interfaces
{
    public interface IDiaryEntry : IBaseDiaryEntry
    {
        EntryType EntryType { get; set; }
        string Body { get; set; }
    }
}
