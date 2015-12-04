using System.Collections.Generic;
using RPG.Lib.Enums;
using RPG.Lib.Schema;

namespace RPG.Models
{
    public class Quest
    {
        public Quest(SchemaQuest schemaQuest, Character character)
        {
            SchemaQuest = schemaQuest;
            Character = character;
        }

        public SchemaQuest SchemaQuest { get; set; }
        public string Id { get; set; }
        public Character Character { get; set; }
        public bool Active { get; set; }
        public bool IsCurrentQuest { get; set; }
        public QuestResult Status { get; set; }
        public bool ShowInQuestLog { get; set; }
        public SchemaQuestState ActiveQuestState { get; set; }
    }

    //public class QuestState
    //{
    //    public QuestState(Quest quest, SchemaQuestState schemaQuestState)
    //    {
    //        Quest = quest;
    //        SchemaQuestState = schemaQuestState;
    //    }

    //    public SchemaQuestState SchemaQuestState { get; set; }
    //    public Quest Quest { get; set; }
    //    public string Id { get; set; }
    //    public Character Character { get; set; }
    //    public bool Active { get; set; }
    //    public bool Started { get; set; }
    //}

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
