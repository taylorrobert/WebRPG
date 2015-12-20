using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RPG.Models.SchemaModels;

namespace RPG.Models
{
    public class ActiveResearchNode
    {
        public ActiveResearchNode()
        {
            RDInvested = 0;
        }

        public int Id { get; set; }
        public ResearchNode ResearchNode { get; set; }
        public Corporation Corporation { get; set; }
        public int RDInvested { get; set; }
        public bool Active { get; set; }

        [NotMapped]
        public bool Complete { get { return RDInvested >= ResearchNode.RDCost; } }

        [NotMapped]
        public string TurnsRemaining {
            get
            {
                if (RDInvested == 0) return "Infinite";
                var div = ResearchNode.RDCost/(double) RDInvested;
                return Math.Ceiling(div).ToString();
            } }

        
    }
}
