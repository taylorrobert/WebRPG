using System;
using System.Collections.Generic;
using Lib.Enums;

namespace Lib.Schema
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

    [Serializable]
    public class SchemaEntity
    {
        public SchemaEntity() { }
        public string PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SchemaDialogOption : SchemaEntity
    {
        public SchemaDialogOption() { }
        public SchemaDialogOption(string publicId, string message, string option1Text, string option1TriggerId, string option2Text,
            string option2TriggerId)
        {
            PublicId = publicId;
            Message = message;
            Option1Text = option1Text;
            Option1TriggerId = option1TriggerId;
            Option2Text = option2Text;
            Option2TriggerId = option2TriggerId;
        }

        public string Message { get; set; }
        public string Option1Text { get; set; }
        public string Option1TriggerId { get; set; }
        public string Option2Text { get; set; }
        public string Option2TriggerId { get; set; }
    }

    public class SchemaQuestState : SchemaEntity
    {
        public SchemaQuestState() { }
        public SchemaQuestState(string publicId, string name, string questId, string description)
        {
            PublicId = publicId;
            Name = name;
            Description = description;
            QuestId = questId;
        }

        public string QuestId { get; set; }
        public int QuestProgress { get; set; }
        public string CompleteDialogOptionId { get; set; }
        public string CompleteNextQuestStateId { get; set; }
        public string FailDialogOptionId { get; set; }
        public string FailNextQuestStateId { get; set; }

        //When this quest state is active, each click will cause a check for the complete and fail triggers.
        //The corresponding action will happen.
        public string CompleteTriggerId { get; set; }
        public string FailTriggerId { get; set; }
    }

    

    public class SchemaRegion : SchemaEntity
    {
        public SchemaRegion() { }
        public SchemaRegion(string publicId, string name, string description)
        {
            PublicId = publicId;
            Name = name;
            Description = description;
        }
    }

    public class SchemaLocation : SchemaEntity
    {
        public SchemaLocation() { }
        public SchemaLocation(string publicId, string name, string regionId, string description)
        {
            PublicId = publicId;
            Name = name;
            RegionId = regionId;
            Description = description;
        }

        public string RegionId { get; set; }
        public List<string> TriggerIds { get; set; } 
    }

    public class SchemaQuest : SchemaEntity
    {
        public SchemaQuest() { }
        public SchemaQuest(string publicId, string name, string locationId, string description)
        {
            PublicId = publicId;
            Name = name;
            LocationId = locationId;
            Description = description;
        }

        public string LocationId { get; set; }
        public List<string> QuestStateIds { get; set; }
        public string StartingQuestStateId { get; set; }

        //Used to alert the player that this quest can be picked up
        public string DiscoveryTeaser { get; set; }
    }

    public class SchemaTrigger : SchemaEntity
    {
        public SchemaTrigger() { }
        public SchemaTrigger(string publicId, TriggerMethod triggerMethod, TriggerAction triggerAction, string targetId,
            string targetValue)
        {
            PublicId = publicId;
            TriggerMethod = triggerMethod;
            TriggerAction = triggerAction;
            TargetId = targetId;
            TargetValue = targetValue;
        }

        public TriggerMethod TriggerMethod { get; set; }
        public TriggerAction TriggerAction { get; set; }
        public string TargetId { get; set; }
        public string TargetValue { get; set; }
        public bool Repeatable { get; set; }
    }

    public class SchemaNPC : SchemaEntity
    {
        public SchemaNPC() { }
        public string LocationId { get; set; } 
    }

    public static class SchemaService
    {
        public static MasterSchema GetMasterSchemaDevSample()
        {
            var master = new MasterSchema();

            var region = new SchemaRegion("CYDONIA", "Cydonia",
                "A region on the planet Mars that has attracted both scientific and popular interest.");
            master.Regions.Add(region);

            var location = new SchemaLocation("ARANDAS_CRATER", "Arandas Crater", "CYDONIA", "A big crater in Cydonia.");
            master.Locations.Add(location);

            var quest = new SchemaQuest("DEV_FIND_THE_THING", "Find the Thing", "ARANDAS_CRATER", "Find the thing. Simple.");
            quest.DiscoveryTeaser = "There is a sign posted next to you: Find the thing.";
            master.Quests.Add(quest);

            var queststate1 = new SchemaQuestState("DEV_FIND_THE_THING_START", "Begin to find the thing", "DEV_FIND_THE_THING",
                "Ask around. Find the thing.");
            queststate1.CompleteDialogOptionId = "DEV_FIND_THE_THING_YES";
            queststate1.CompleteTriggerId = "DEV_FIND_THE_THING_START_TRIGGER1";
            master.QuestStates.Add(queststate1);

            var trigger1 = new SchemaTrigger("DEV_FIND_THE_THING_START_TRIGGER1", TriggerMethod.OnEnterQuestState,
                TriggerAction.OpenDialog, "DEV_FIND_THE_THING_DIALOG1", null);
            master.Triggers.Add(trigger1);

            var dialog1 = new SchemaDialogOption("DEV_FIND_THE_THING_DIALOG1", "Would you like to go get the thing?",
                "Go get the thing", "DEV_FIND_THE_THING_TRIGGER2", "Don't find the thing",
                "DEV_FIND_THE_THING_TRIGGER_START");
            master.DialogOptions.Add(dialog1);

            var trigger2 = new SchemaTrigger("DEV_FIND_THE_THING_START_TRIGGER2", TriggerMethod.Instant,
                TriggerAction.AdvanceQuestState, "DEV_FIND_THE_THING_2", null);
            master.Triggers.Add(trigger2);

            var questState2 = new SchemaQuestState("DEV_FIND_THE_THING_2", "You've found the thing!", "DEV_FIND_THE_THING",
                "Now that you've found the thing, you've won.");
            master.QuestStates.Add(questState2);

            return master;
        }
    }


    
}
