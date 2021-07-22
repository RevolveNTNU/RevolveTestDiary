using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolveTestDiaryXf.Interfaces
{
    public interface ITestDay : IBaseDiaryEntry
    {
        event EventHandler<ITestDay> TriggerAutoSaveEvent;
        IPerson EsoAsr { get; set; }

        ILocation Location { get; set; }

        IDebrief Debrief { get; set; }

        ObservableCollection<IDiaryGoal> Goals { get; set; }

        ObservableCollection<ISession> Sessions { get; set; }

        void AddGoalCommand();

        void AddSessionCommand();
    }
}
