using System.Collections.Generic;
using RPG.Lib.Schema;

namespace RPG.Models
{
    public class MasterCharacterModel
    {
        public int Id { get; set; }
        public Character Character { get; set; }
        public IEnumerable<QuestState> QuestStates { get; set; }
        public string Log { get; set; }

    }

    public class Quest
    {
        public SchemaQuest SchemaQuest { get; set; }
        public string Id { get; set; }
        public string CharacterId { get; set; }
        public bool Active { get; set; }
        public bool IsCurrentQuest { get; set; }
        public bool Complete { get; set; }
        public bool ShowInQuestLog { get; set; }
    }

    public class QuestState
    {
        public Quest Quest { get; set; }
        public string Id { get; set; }
        public string CharacterId { get; set; }
        public bool Active { get; set; }
    }

    public class DialogOption
    {
        public SchemaDialogOption SchemaDialogOption { get; set; }
        public string Id { get; set; }
        public Character Character { get; set; }
        public bool Active { get; set; }
        public string Message { get; set; }
        public int OptionChosen { get; set; }
    }

    public class Trigger
    {
        public SchemaTrigger SchemaTrigger { get; set; }
        public string Id { get; set; }
        public Character Character { get; set; }
        public bool Triggered { get; set; }
    }
}
