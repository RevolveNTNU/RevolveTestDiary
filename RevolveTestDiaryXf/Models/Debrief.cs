using System;

namespace RevolveTestDiaryXf.Models
{
    public class Debrief
    {
        private string whatWentWell = "";

        public string WhatWentWell
        {
            get { return whatWentWell; }
            set { whatWentWell = value; TriggerAutoSaveEvent?.Invoke(this, this); }
        }

        public Debrief() { }
        public Debrief(string whatWentWell, string whatCanBeImproved, string issuesDiscovered)
        {
            WhatWentWell = whatWentWell;
            WhatCanBeImproved = whatCanBeImproved;
            IssuesDiscovered = issuesDiscovered;
        }

        private string whatCanBeImproved = "";

        public string WhatCanBeImproved
        {
            get { return whatCanBeImproved; }
            set { whatCanBeImproved = value; TriggerAutoSaveEvent?.Invoke(this, this); }
        }

        private string issuesDiscovered = "";

        public string IssuesDiscovered
        {
            get { return issuesDiscovered; }
            set { issuesDiscovered = value; TriggerAutoSaveEvent?.Invoke(this, this); }
        }


        public event EventHandler<Debrief> TriggerAutoSaveEvent;
    }
}
