using System;
using System.Collections.Generic;
using RPG.Lib.Schema;

namespace RPG.Models
{
    public class Character
    {
        public Character()
        {
            PublicId = Guid.NewGuid().ToString();
        }
        
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public ApplicationUser User { get; set; }
        public SchemaRegion Region { get; set; }
        public SchemaRegion Location { get; set; }
    }
}
