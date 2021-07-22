using RevolveTestDiaryXf.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevolveTestDiaryXf.Models
{
    public class Debrief : IDebrief
    {
        private string whatWentWell;

        public string WhatWentWell
        {
            get { return whatWentWell; }
            set { whatWentWell = value; TriggerAutoSaveEvent?.Invoke(this, this); }
        }

        private string whatCanBeImproved;

        public string WhatCanBeImproved
        {
            get { return whatCanBeImproved; }
            set { whatCanBeImproved = value; TriggerAutoSaveEvent?.Invoke(this, this); }
        }

        private string issuesDiscovered;

        public string IssuesDiscovered
        {
            get { return issuesDiscovered; }
            set { issuesDiscovered = value; TriggerAutoSaveEvent?.Invoke(this, this); }
        }


        public event EventHandler<IDebrief> TriggerAutoSaveEvent;
    }
}
