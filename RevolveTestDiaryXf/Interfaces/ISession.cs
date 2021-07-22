using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolveTestDiaryXf.Interfaces
{
    public interface ISession : IBaseDiaryEntry
    {
        event EventHandler<ISession> TriggerAutoSaveEvent;
        string Title { get; set; }
        ObservableCollection<IDiaryEntry> SessionEntries { get; set; }
        void AddDiaryEntryCommand();
    }
}
