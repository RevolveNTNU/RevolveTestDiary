﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolveTestDiaryXf.Interfaces
{
    public interface IDiaryGoal
    {
        event EventHandler<IDiaryGoal> TriggerAutoSaveEvent;
        bool Achieved { get; set; }

        string Goal { get; set; }
    }
}
