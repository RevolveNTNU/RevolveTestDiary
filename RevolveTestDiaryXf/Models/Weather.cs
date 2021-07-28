using System;
using System.Collections.Generic;
using System.Text;

namespace RevolveTestDiaryXf.Models
{
    public class Weather
    {
        public double Temperature { get; set; }
        public string Description { get; set; }
        public bool IsLoaded { get; set; }

        public Weather()
        {
            IsLoaded = false;
        }
    }
}
