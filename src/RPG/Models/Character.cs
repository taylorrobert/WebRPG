using System;
using System.Collections.Generic;

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
        public ICollection<CharacterQuest> Quests { get; set; } 
    }
}
