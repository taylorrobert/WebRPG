using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Lib;
using RPG.Lib.Enums;
using RPG.Lib.Schema;
using RPG.Models;

namespace RPG.Services
{
    public static class SchemaService
    {
        public static void ConfigureTestSchema(ApplicationDbContext context)
        {
            var region = new SchemaRegion();
            region.ReferenceName = "CYDONIA";
            region.Name = "Cydonia";
            region.Description = "A region on the planet Mars that has attracted both scientific and popular interest.";
            context.SchemaRegions.Add(region);

            var location = new SchemaLocation();
            location.ReferenceName = "ARANDAS_CRATER";
            location.Name = "Arandas Crater";
            location.Region = region;
            location.Description = "A big crater in Cydonia.";
            context.SchemaLocations.Add(location);

            var quest = new SchemaQuest();
            quest.ReferenceName = "DEV_FIND_THE_THING";
            quest.Name = "Find the Thing";
            quest.Description = "Find the thing. Simple.";
            quest.DiscoveryTeaser = "There is a sign posted next to you: Find the thing.";
            context.SchemaQuests.Add(quest);

            var queststate1 = new SchemaQuestState();
            queststate1.ReferenceName = "DEV_FIND_THE_THING_START";
            queststate1.Description = "Begin to find the thing";
            context.SchemaQuestStates.Add(queststate1);


            var trigger1 = new SchemaTrigger();
            trigger1.ReferenceName = "DEV_FIND_THE_THING_START_TRIGGER1";
            trigger1.TriggerMethod = TriggerMethod.OnEnterQuestState;
            trigger1.TriggerAction = TriggerAction.OpenDialog;
            trigger1.TargetReferenceName = "DEV_FIND_THE_THING_DIALOG1";
            context.SchemaTriggers.Add(trigger1);

            var dialog1 = new SchemaDialogOption();
            dialog1.ReferenceName = "DEV_FIND_THE_THING_DIALOG1";
            dialog1.Message = "Would you like to go get the thing?";
            dialog1.Option1Text = "Go get the thing";
            dialog1.Option2Text = "Don't find the thing";
            dialog1.Option2Trigger = null;
            context.SchemaDialogOptions.Add(dialog1);

            queststate1.CompleteDialogOption = dialog1;

            var trigger2 = new SchemaTrigger();
            trigger2.ReferenceName = "DEV_FIND_THE_THING_START_TRIGGER2";
            trigger2.TriggerMethod = TriggerMethod.Instant;
            trigger2.TriggerAction = TriggerAction.AdvanceQuestState;
            trigger2.TargetReferenceName = "DEV_FIND_THE_THING_2";
            trigger2.TargetValue = Constants.TriggerAdvanceQuestCompleted;
            context.SchemaTriggers.Add(trigger2);


            dialog1.Option1Trigger = trigger2;

            var questState2 = new SchemaQuestState();
            questState2.ReferenceName = "DEV_FIND_THE_THING_2";
            questState2.Description = "You've found the thing!";
        }
    }
}
