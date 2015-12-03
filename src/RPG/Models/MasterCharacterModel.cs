using System.Collections.Generic;

namespace RPG.Models
{
    public class MasterCharacterModel
    {
        public Character Character { get; set; }
        public Region Region { get; set; }
        public Location Location { get; set; }
        public IEnumerable<QuestState> QuestStates { get; set; }
        public string Log { get; set; }

    }

    public class Region
    {
        public string SchemaId { get; set; }
    }

    public class Location
    {
        public string SchemaId { get; set; }
    }

    public class Quest
    {
        public string SchemaId { get; set; }
        public string Id { get; set; }
        public string CharacterId { get; set; }
        public bool Active { get; set; }
        public bool IsCurrentQuest { get; set; }
        public bool Complete { get; set; }
        public bool ShowInQuestLog { get; set; }
    }

    public class QuestState
    {
        public string SchemaId { get; set; }
        public string Id { get; set; }
        public string CharacterId { get; set; }
        public bool Active { get; set; }
    }

    public class DialogOption
    {
        public string SchemaId { get; set; }
        public string Id { get; set; }
        public string CharacterId { get; set; }
        public bool Active { get; set; }
        public string Message { get; set; }
        public int OptionChosen { get; set; }
    }

    public class Trigger
    {
        public string SchemaId { get; set; }
        public string Id { get; set; }
        public string CharacterId { get; set; }
        public bool Triggered { get; set; }
    }
}
