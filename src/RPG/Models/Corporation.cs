using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DefaultValue(20)]
        public int RD { get; set; }

        [DefaultValue(20)]
        public int Reputation { get; set; }

        [DefaultValue(20)]
        public int Readiness { get; set; }

        [DefaultValue(1000)]
        public long Cash { get; set; }

        [DefaultValue(20)]
        public int PublicInterest { get; set; }

        [DefaultValue(100)]
        public int TurnsRemaining { get; set; }

        [DefaultValue(1)]
        public long TurnCount { get; set; }

        [DefaultValue(1.00)]
        public double BusinessMultiplier { get; set; }
    }
}
