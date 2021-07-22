using System;
using System.Collections.Generic;
using System.Text;

namespace RevolveTestDiaryXf.Interfaces
{
    public interface IDebrief
    {
        event EventHandler<IDebrief> TriggerAutoSaveEvent;
        string WhatWentWell { get; set; }
        string WhatCanBeImproved { get; set; }
        string IssuesDiscovered { get; set; }
    }
}
