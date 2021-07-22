using RevolveTestDiaryXf.Interfaces;
using System;

namespace RevolveTestDiaryXf.Models
{
    public class DiaryGoal : IDiaryGoal
    {
        private bool achieved;

        public bool Achieved
        {
            get { return achieved; }
            set
            {
                achieved = value;
                TriggerAutoSaveEvent?.Invoke(this, this);
            }
        }


        private string goal;

        public string Goal
        {
            get { return goal; }
            set
            {
                goal = value;
                TriggerAutoSaveEvent?.Invoke(this, this);
            }
        }

        public DiaryGoal(string goal)
        {
            Goal = goal;
            Achieved = false;
        }

        public event EventHandler<IDiaryGoal> TriggerAutoSaveEvent;
    }
}
