using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RPG.Models.SchemaModels;

namespace RPG.Models
{
    public class Corporation
    {
        public Corporation(ApplicationUser user, string name)
        {
            User = user;
            Name = name;
            RD = 0;
            Reputation = 0;
            Readiness = 0;
            Cash = 10000;
            PublicInterest = 0;
            TurnsRemaining = 10;
            TurnCount = 1;
        }

        public Corporation() { }

        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public int RD { get; set; }
        public int Reputation { get; set; }
        public int Readiness { get; set; }
        public long Cash { get; set; }
        public int PublicInterest { get; set; }
        public int TurnsRemaining { get; set; }
        public long TurnCount { get; set; }
    }
}
