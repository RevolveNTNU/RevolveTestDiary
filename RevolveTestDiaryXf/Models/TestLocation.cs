using RevolveTestDiaryXf.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolveTestDiaryXf.Models
{
    public class TestLocation : ILocation
    {
        public string Location { get; set; }
        public TestLocation(string location)
        {
            Location = location;
        }

    }
}
