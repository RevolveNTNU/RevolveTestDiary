using Avalonia.Controls.Selection;
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
        public IDebrief Debrief { get; set; }

        public ObservableCollection<IDiaryGoal> Goals { get; set; }
        public SelectionModel<IDiaryGoal> SelectionGoals { get; }

        private string newGoalBody;

        public string NewGoalBody
        {
            get { return newGoalBody; }
            set { this.RaiseAndSetIfChanged(ref newGoalBody, value); }
        }

        public ObservableCollection<ISession> Sessions { get; set; }

        private string newSessionTitle;

        public event EventHandler<ITestDay> TriggerAutoSaveEvent;

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
            Debrief = new Debrief();
            Debrief.TriggerAutoSaveEvent += TriggerAutoSaveFromDebrief;

            Goals = new ObservableCollection<IDiaryGoal>();
            Sessions = new ObservableCollection<ISession>();
            SelectionGoals = new SelectionModel<IDiaryGoal>();
            SelectionGoals.SelectionChanged += SelectionGoalsChanged;

        }


        private void SelectionGoalsChanged(object? sender, SelectionModelSelectionChangedEventArgs<IDiaryGoal> e)
        {
            return;
        }

        public void AddGoalCommand()
        {
            var goal = new DiaryGoal(NewGoalBody);
            goal.TriggerAutoSaveEvent += TriggerAutoSaveFromGoal;
            NewGoalBody = null;
            AddGoal(goal);
            TriggerAutoSaveEvent.Invoke(this, this);
        }
        public void AddGoal(IDiaryGoal goal)
        {
            Goals.Add(goal);
            this.RaisePropertyChanged(nameof(Goals));
        }

        public void AddSessionCommand()
        {
            var session = new Session(NewSessionTitle);
            session.TriggerAutoSaveEvent += TriggerAutoSaveFromSession;
            NewSessionTitle = null;
            AddSession(session);
            TriggerAutoSaveEvent.Invoke(this, this);
        }

        private void TriggerAutoSaveFromSession(object? sender, ISession e)
        {
            TriggerAutoSaveEvent.Invoke(this, this);
        }

        private void TriggerAutoSaveFromGoal(object? sender, IDiaryGoal e)
        {
            TriggerAutoSaveEvent.Invoke(this, this);
        }

        private void TriggerAutoSaveFromDebrief(object? sender, IDebrief e)
        {
            TriggerAutoSaveEvent.Invoke(this, this);
        }

        public void AddSession(ISession session)
        {
            Sessions.Add(session);
            this.RaisePropertyChanged(nameof(Sessions));
        }
    }
}
