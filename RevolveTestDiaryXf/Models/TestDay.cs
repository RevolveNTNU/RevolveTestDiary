using Avalonia.Controls;
using ReactiveUI;
using RevolveTestDiaryXf.ViewModels;
using RevolveTestDiaryXf.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RevolveTestDiaryXf.Models
{
    public class TestDay : ViewModelBase
    {
        public DateTime Timestamp { get; set; }

        public Person EsoAsr { get; set; }

        public TestLocation Location { get; set; }
        public Debrief Debrief { get; set; }

        public ObservableCollection<DiaryGoal> Goals { get; set; }
        public ObservableCollection<DiaryGoal> DvCheckList { get; set; }

        private string newGoalBody;

        [JsonIgnore]
        public string NewGoalBody
        {
            get { return newGoalBody; }
            set { this.RaiseAndSetIfChanged(ref newGoalBody, value); }
        }

        public ObservableCollection<Session> Sessions { get; set; }

        private string newSessionTitle;

        public event EventHandler<TestDay> TriggerAutoSaveEvent;
        public event EventHandler<TestDay> CloseTestDayEvent;

        [JsonIgnore]
        public string NewSessionTitle
        {
            get { return newSessionTitle; }
            set { this.RaiseAndSetIfChanged(ref newSessionTitle, value); }
        }

        public TestDay(TestLocation location, Person esoAsr)
        {
            Location = location;
            Timestamp = DateTime.Now;
            EsoAsr = esoAsr;
            Debrief = new Debrief();
            Debrief.TriggerAutoSaveEvent += TriggerAutoSaveFromDebrief;

            Goals = new ObservableCollection<DiaryGoal>();
            Sessions = new ObservableCollection<Session>();
            DvCheckList = new ObservableCollection<DiaryGoal>()
            {
                new DiaryGoal(){Goal="Disconnect Accu Wires"},
                new DiaryGoal(){Goal="Put RES on charger"},
                new DiaryGoal(){Goal="Put walkie-talkie on charger"},
                new DiaryGoal(){Goal="Tidy up the PU"},
                new DiaryGoal(){Goal="Upload Analyze logs"}
            };
        }

        public TestDay(DateTime timestamp, Person esoAsr, TestLocation location, Debrief debrief, ObservableCollection<DiaryGoal> goals, string newGoalBody, ObservableCollection<Session> sessions, string newSessionTitle)
        {
            Timestamp = timestamp;
            EsoAsr = esoAsr;
            Location = location;
            Debrief = debrief;
            Goals = goals;
            NewGoalBody = newGoalBody;
            Sessions = sessions;
            NewSessionTitle = newSessionTitle;
        }

        public TestDay()
        {

        }

        public void AddGoalCommand()
        {
            var goal = new DiaryGoal(NewGoalBody);
            goal.TriggerAutoSaveEvent += TriggerAutoSaveFromGoal;
            NewGoalBody = null;
            AddGoal(goal);
            TriggerAutoSaveEvent?.Invoke(this, this);
        }
        public void AddGoal(DiaryGoal goal)
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
            TriggerAutoSaveEvent?.Invoke(this, this);
        }

        public void TriggerAutoSaveFromSession(object? sender, Session e)
        {
            TriggerAutoSaveEvent?.Invoke(this, this);
        }

        public void TriggerAutoSaveFromGoal(object? sender, DiaryGoal e)
        {
            TriggerAutoSaveEvent?.Invoke(this, this);
        }

        public void TriggerAutoSaveFromDebrief(object? sender, Debrief e)
        {
            TriggerAutoSaveEvent?.Invoke(this, this);
        }

        public void TriggerAutoSaveFromEntry(object? sender, DiaryEntry e)
        {
            TriggerAutoSaveEvent?.Invoke(this, this);
        }

        public void AddSession(Session session)
        {
            Sessions.Add(session);
            this.RaisePropertyChanged(nameof(Sessions));
        }

        public void CloseTestDay()
        {
            CloseTestDayEvent?.Invoke(this, this);
        }

        public async Task ExportToMarkdown()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filters.Add(new FileDialogFilter() { Name = "MarkDown", Extensions = { "md" } });
            var fileName = await saveFileDialog.ShowAsync(MainWindow.Instance);
            if (fileName == null)
                return;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"# {Timestamp}");
            stringBuilder.AppendLine("");
            // Goals
            stringBuilder.AppendLine($"## Goals");
            foreach (var goal in Goals)
            {
                stringBuilder.AppendLine($"- [{(goal.Achieved ? "X" : " ")}] {goal.Goal}");
            }
            stringBuilder.AppendLine("");
            // Sessions
            stringBuilder.AppendLine($"## Sessions");
            foreach (var session in Sessions)
            {
                stringBuilder.AppendLine($"### {session.Timestamp} - {session.Title}");

                foreach (var entry in session.SessionEntries)
                {
                    var tabs = entry.EntryType == Enums.EntryType.EVENT || entry.EntryType == Enums.EntryType.ISSUE ? "\t\t" : "\t";
                    stringBuilder.AppendLine($"* {entry.Timestamp} - {entry.EntryType}:{tabs}{entry.Body}");
                }
                stringBuilder.AppendLine("");
            }

            //Debrief
            stringBuilder.AppendLine($"## Debrief");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("### What went well today?");
            stringBuilder.AppendLine(Debrief.WhatWentWell);
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("### What can be improved on from today?");
            stringBuilder.AppendLine(Debrief.WhatCanBeImproved);
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("### What issues did we discover?");
            stringBuilder.AppendLine(Debrief.IssuesDiscovered);

            await File.WriteAllTextAsync(fileName, stringBuilder.ToString());
        }
    }
}
