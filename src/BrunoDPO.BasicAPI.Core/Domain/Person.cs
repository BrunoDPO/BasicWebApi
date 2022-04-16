using BrunoDPO.BasicAPI.Core.Domain.Enumeration;
using System;

namespace BrunoDPO.BasicAPI.Core.Domain
{
    public class Person
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }
    }
}
