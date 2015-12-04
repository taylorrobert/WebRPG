using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Lib;
using RPG.Lib.Enums;
using RPG.Lib.Schema;
using RPG.Models;
using RPG.ViewModels.Action;
using ActionModel = RPG.ViewModels.Action.ActionModel;

namespace RPG.Services
{
    public static class SchemaService
    {
        public static void AdvanceActionModel(ApplicationDbContext context, ActionModel model)
        {
            //Lists for the output
            var advancedQuests = new List<QuestStatusResult>();
            var possibleTriggers = new List<TriggerResult>();

            //Handle quests
            foreach (var questName in model.QuestsToStart)
            {
                possibleTriggers.Add(BeginQuest(context, questName, model.Character));
            }
            model.QuestsToStart = new List<string>(); //Clean up quests

            foreach (var q in model.QuestsToAdvance) // Add triggers for exiting quest stages
            {
                possibleTriggers.Add(new TriggerResult(q.Quest.ActiveQuestState.ReferenceName,
                    TriggerMethod.OnExitQuestState, TriggererType.QuestStatus));
            }

            foreach (var questResult in model.QuestsToAdvance) //Set the next quest state to the correct one based on whether the quest completed or failed
            {
                if (!model.Quests.Select(q => q.Id).Contains(questResult.Quest.Id))
                    throw new Exception("WTF. You don't have quest " + questResult.Quest.SchemaQuest.ReferenceName);

                questResult.Quest.ActiveQuestState = questResult.Completed
                    ? questResult.Quest.ActiveQuestState.CompleteNextQuestState
                    : questResult.Quest.ActiveQuestState.FailNextQuestState;

                if (questResult.Quest.ActiveQuestState.ReferenceName ==
                    questResult.Quest.SchemaQuest.EndingQuestStateSuccess.ReferenceName)
                {
                    questResult.Quest.Status = QuestResult.Success;
                    possibleTriggers.Add(new TriggerResult(questResult.Quest.SchemaQuest.ReferenceName,
                    TriggerMethod.OnQuestComplete, TriggererType.Quest));
                }
                else if (questResult.Quest.ActiveQuestState.ReferenceName ==
                         questResult.Quest.SchemaQuest.EndingQuestStateFailure.ReferenceName)
                {
                    questResult.Quest.Status = QuestResult.Fail;
                    possibleTriggers.Add(new TriggerResult(questResult.Quest.SchemaQuest.ReferenceName,
                    TriggerMethod.OnQuestFailed, TriggererType.Quest));
                }
                else questResult.Quest.Status = QuestResult.InProgress;


                advancedQuests.Add(questResult);
            }

            foreach (var q in model.QuestsToAdvance) // Add triggers for entering quest stages
            {
                possibleTriggers.Add(new TriggerResult(q.Quest.ActiveQuestState.ReferenceName,
                    TriggerMethod.OnEnterQuestState, TriggererType.QuestStatus));
            }
            context.SaveChanges();


            //Handle triggers
            foreach (var t in possibleTriggers)
            {
                var hit = HasTriggered(context, t, model.Triggers);
                if (hit != null)
                {
                    hit.Triggered = true;
                }
            }

            model.Triggers = GetTriggersForLocation(context, model.Character, model.Location);
        }

        public static Trigger HasTriggered(ApplicationDbContext context, TriggerResult possibleTrigger, List<Trigger> triggers)
        {
            var hit =
                triggers.FirstOrDefault(
                    t =>
                        t.Triggered == false && t.SchemaTrigger.TriggerOwner == possibleTrigger.TriggererReferenceName &&
                        t.SchemaTrigger.TriggererType == possibleTrigger.TriggererType &&
                        t.SchemaTrigger.TriggerMethod == possibleTrigger.TriggerMethod);

            return hit;
        }

        public static TriggerResult BeginQuest(ApplicationDbContext context, string referenceName, Character character)
        {
            var questSchema = context.SchemaQuests.FirstOrDefault(s => s.ReferenceName == referenceName);

            if (questSchema == null)
                throw new Exception("Unable to find quest with reference ID: " + referenceName);

            var q = new Quest(questSchema, character);
            q.Active = true;
            q.ShowInQuestLog = true;
            q.Status = QuestResult.InProgress;
            q.ActiveQuestState = q.SchemaQuest.StartingQuestState;
            context.Quests.Add(q);
            context.SaveChanges();

            return new TriggerResult(referenceName, TriggerMethod.OnBeginQuest, TriggererType.Quest);
        }

        public static List<Trigger> GetTriggersForLocation(ApplicationDbContext context, Character character,
            SchemaLocation location)
        {
            var newTriggers = new List<Trigger>();

            var triggers =
                context.Triggers.Where(s => s.SchemaTrigger.Location.ReferenceName == location.ReferenceName).ToList();

            var newTriggerSchemas =
                context.SchemaTriggers.Where(
                    st =>
                        st.Location.ReferenceName == location.ReferenceName &&
                        !triggers.Select(t => t.SchemaTrigger.ReferenceName).Contains(st.ReferenceName)).ToList();

            foreach (var t in newTriggerSchemas)
            {
                newTriggers.Add(new Trigger
                {
                    Character = character,
                    SchemaTrigger = t,
                    Triggered = false
                });
            }

            context.Add(newTriggers);
            context.SaveChanges();

            return context.Triggers.Where(s => s.SchemaTrigger.Location.ReferenceName == location.ReferenceName).ToList();
        } 

        public static void ConfigureCharacterFirstLogin(ApplicationDbContext context, Character character, ActionModel model)
        {
            character.Location = DataService.GetStartingLocation(context);
            character.Region = DataService.GetStartingRegion(context);

            model.QuestsToStart.Add(Constants.StartingQuestName);
        }

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
