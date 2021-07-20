using RevolveTestDiaryXf.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolveTestDiaryXf.Models
{
    public class Person : IPerson
    {
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }
    }
}
