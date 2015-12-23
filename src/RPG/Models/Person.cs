using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public int TurnSalary { get; set; }
        public bool Celebrity { get; set; } //Extra 10 Public Interest and Reputation
        public int Intelligence { get; set; } //Bonus to R&D
        public int Experience { get; set; } //Bonus to Readiness 
        public int Business { get; set; } //Bonus to cash sources, such as loans, stock
        public int Fitness { get; set; } //Related to the possibility of things going wrong in missions
    }
}
