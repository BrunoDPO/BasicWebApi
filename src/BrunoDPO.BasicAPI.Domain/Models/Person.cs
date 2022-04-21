using BrunoDPO.BasicAPI.Domain.Enumerations;
using System;

namespace BrunoDPO.BasicAPI.Domain.Models
{
    public class Person
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }
    }
}
