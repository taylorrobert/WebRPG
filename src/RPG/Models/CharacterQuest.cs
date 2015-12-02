using System;

namespace RPG.Models
{
    public class CharacterQuest
    {
        public CharacterQuest()
        {
            PublicId = Guid.NewGuid().ToString();
        }

        public int Id { get; set; }
        public string PublicId { get; set; }
        public string QuestId { get; set; }
        public int Progress { get; set; }
        public bool Complete { get; set; }

        public Character Character { get; set; }
    }
}
