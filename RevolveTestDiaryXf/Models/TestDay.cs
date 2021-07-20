using ReactiveUI;
using RevolveTestDiaryXf.Interfaces;
using RevolveTestDiaryXf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RevolveTestDiaryXf.Models
{
    public class TestDay : ViewModelBase, ITestDay
    {
        public DateTime Timestamp { get; set; }

        public IPerson EsoAsr { get; set; }

        public ILocation Location { get; set; }

        public ObservableCollection<IDiaryGoal> Goals { get; set; }

        private string newGoalBody;

        public string NewGoalBody
        {
            get { return newGoalBody; }
            set { this.RaiseAndSetIfChanged(ref newGoalBody, value); }
        }

        public ObservableCollection<ISession> Sessions { get; set; }

        private string newSessionTitle;

        public string NewSessionTitle
        {
            get { return newSessionTitle; }
            set { this.RaiseAndSetIfChanged(ref newSessionTitle, value); }
        }

        public TestDay(ILocation location, IPerson esoAsr)
        {
            Location = location;
            Timestamp = DateTime.Now;
            EsoAsr = esoAsr;

            Goals = new ObservableCollection<IDiaryGoal>();
            Sessions = new ObservableCollection<ISession>();
        }

        public void AddGoalCommand()
        {
            var goal = new DiaryGoal(NewGoalBody);
            NewGoalBody = null;
            AddGoal(goal);
        }

        public void AddGoal(IDiaryGoal goal)
        {
            Goals.Add(goal);
            this.RaisePropertyChanged(nameof(Goals));
        }

        public void AddSessionCommand()
        {
            var session = new Session(NewSessionTitle);
            NewSessionTitle = null;
            AddSession(session);
        }

        public void AddSession(ISession session)
        {
            Sessions.Add(session);
            this.RaisePropertyChanged(nameof(Sessions));
        }
    }
}
