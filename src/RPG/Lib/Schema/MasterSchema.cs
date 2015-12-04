using System;
using System.Collections.Generic;
using RPG.Lib.Enums;
using RPG.Models;

namespace RPG.Lib.Schema
{
    public class MasterSchema
    {
        public MasterSchema()
        {
            Regions = new List<SchemaRegion>();
            Locations = new List<SchemaLocation>();
            Quests = new List<SchemaQuest>();
            QuestStates = new List<SchemaQuestState>();
            DialogOptions = new List<SchemaDialogOption>();
            Triggers = new List<SchemaTrigger>();
        }

        public List<SchemaRegion> Regions { get; set; }
        public List<SchemaLocation> Locations { get; set; }
        public List<SchemaQuest> Quests { get; set; }
        public List<SchemaQuestState> QuestStates { get; set; }
        public List<SchemaDialogOption> DialogOptions { get; set; }
        public List<SchemaTrigger> Triggers { get; set; } 
    }

    public class SchemaEntity
    {
        public int Id { get; set; }
        public string ReferenceName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<SchemaTrigger> Triggers { get; set; }
    }

    public class SchemaDialogOption : SchemaEntity
    {
        public string Message { get; set; }
        public string Option1Text { get; set; }
        public SchemaTrigger Option1Trigger { get; set; }
        public string Option2Text { get; set; }
        public SchemaTrigger Option2Trigger { get; set; }
    }

    public class SchemaQuestState : SchemaEntity
    {
        public int QuestProgress { get; set; }
        public SchemaDialogOption BeginDialogOption { get; set; }
        public SchemaDialogOption CompleteDialogOption { get; set; }
        public string CompletedNextQuestStateReferenceName { get; set; }
        public SchemaDialogOption FailDialogOption { get; set; }
        public string FailedNextQuestStateReferenceName { get; set; }
    }

    

    public class SchemaRegion : SchemaEntity
    {

    }

    public class SchemaLocation : SchemaEntity
    {
        public SchemaRegion Region { get; set; }
    }

    public class SchemaQuest : SchemaEntity
    {
        public ICollection<SchemaQuestState> QuestStates { get; set; }
        public SchemaQuestState StartingQuestState { get; set; }
        public SchemaQuestState EndingQuestState { get; set; }

        //Used to alert the player that this quest can be picked up
        public string DiscoveryTeaser { get; set; }
    }

    public class SchemaTrigger : SchemaEntity
    {
        public TriggerMethod TriggerMethod { get; set; }
        public TriggerAction TriggerAction { get; set; }
        public string TargetReferenceName { get; set; }
        public string TargetValue { get; set; }
        public bool Repeatable { get; set; }
    }

    public class SchemaNPC : SchemaEntity
    {
        public string LocationId { get; set; } 
    }

    


    
}
