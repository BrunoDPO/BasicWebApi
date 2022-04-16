using BrunoDPO.BasicAPI.Core.Enumeration;
using System;

namespace BrunoDPO.BasicAPI.Core.Model
{
    public class Person
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }
    }
}
