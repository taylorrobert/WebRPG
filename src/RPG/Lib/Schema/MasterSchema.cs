using System;
using System.Collections.Generic;
using RPG.Lib.Enums;

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

    public static class SchemaService
    {
        public static MasterSchema GetMasterSchemaDevSample()
        {
            var master = new MasterSchema();

            var region = new SchemaRegion();
            region.ReferenceName = "CYDONIA";
            region.Name = "Cydonia";
            region.Description="A region on the planet Mars that has attracted both scientific and popular interest.";
            master.Regions.Add(region);

            var location = new SchemaLocation();
            location.ReferenceName = "ARANDAS_CRATER";
            location.Name = "Arandas Crater";
            location.Region = region;
            location.Description = "A big crater in Cydonia.";
            master.Locations.Add(location);

            var quest = new SchemaQuest();
            quest.ReferenceName = "DEV_FIND_THE_THING";
            quest.Name = "Find the Thing";
            quest.Description = "Find the thing. Simple.";
            quest.DiscoveryTeaser = "There is a sign posted next to you: Find the thing.";
            master.Quests.Add(quest);

            var queststate1 = new SchemaQuestState();
            queststate1.ReferenceName = "DEV_FIND_THE_THING_START";
            queststate1.Description = "Begin to find the thing";
            
            master.QuestStates.Add(queststate1);

            var trigger1 = new SchemaTrigger();
            trigger1.ReferenceName = "DEV_FIND_THE_THING_START_TRIGGER1";
            trigger1.TriggerMethod = TriggerMethod.OnEnterQuestState;
            trigger1.TriggerAction = TriggerAction.OpenDialog;
            trigger1.TargetReferenceName = "DEV_FIND_THE_THING_DIALOG1";
            master.Triggers.Add(trigger1);

            var dialog1 = new SchemaDialogOption();
            dialog1.ReferenceName = "DEV_FIND_THE_THING_DIALOG1";
            dialog1.Message = "Would you like to go get the thing?";
            dialog1.Option1Text = "Go get the thing";
            dialog1.Option2Text = "Don't find the thing";
            dialog1.Option2Trigger = null;
            master.DialogOptions.Add(dialog1);
            queststate1.CompleteDialogOption = dialog1;

            var trigger2 = new SchemaTrigger();
            trigger2.ReferenceName = "DEV_FIND_THE_THING_START_TRIGGER2";
            trigger2.TriggerMethod = TriggerMethod.Instant;
            trigger2.TriggerAction = TriggerAction.AdvanceQuestState;
            trigger2.TargetReferenceName = "DEV_FIND_THE_THING_2";
            trigger2.TargetValue = Constants.TriggerAdvanceQuestCompleted;
            master.Triggers.Add(trigger2);

            dialog1.Option1Trigger = trigger2;

            var questState2 = new SchemaQuestState();
            questState2.ReferenceName = "DEV_FIND_THE_THING_2";
            questState2.Description = "You've found the thing!";

            master.QuestStates.Add(questState2);

            return master;
        }
    }


    
}
