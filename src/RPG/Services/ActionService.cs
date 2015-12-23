using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using RPG.Models;
using RPG.Models.SchemaModels;
using Constants = RPG.Constants.Constants;

namespace RPG.Services
{
    public static class ActionService
    {
        public static void PerformActions(ApplicationDbContext db, ClientActionModel model, DataCache data)
        {
            var actionCounter = new Dictionary<string, int>();
            actionCounter[Constants.Constants.ActionTypeResearch] = 0;
            string currentObjectName = "";

            if (string.IsNullOrEmpty(model.Actions))
            {
                LogService.Log(db, data.Corporation, "You made no actions the previous turn.");
                return;
            }

            var actions = model.Actions.Split('|');
            foreach (var actionType in actions)
            {
                var currentActionDict = new Dictionary<string, string>();
                var actionItems = actionType.Split(';');
                foreach (var a in actionItems)
                {
                    var parameters = a.Split('=');
                    if (string.IsNullOrEmpty(parameters[0])) continue;
                    currentActionDict[parameters[0]] = parameters[1];
                }

                //Handle action types---
                if (currentActionDict.ContainsKey(Constants.Constants.ActionType))
                {
                    if (currentActionDict[Constants.Constants.ActionType] == Constants.Constants.ActionTypeResearch)
                        actionCounter[Constants.Constants.ActionTypeResearch]++;
                }

                //Handle actions---
                if (currentActionDict.ContainsKey(Constants.Constants.Action))
                {
                    var specifierSplit = currentActionDict[Constants.Constants.Action].Split(':');
                    currentObjectName = specifierSplit[1];

                    //#LearnResearch
                    if (specifierSplit[0] == Constants.Constants.LearnResearch)
                    {
                        var researchNode = db.ResearchNodes.FirstOrDefault(n => n.Name == currentObjectName);

                        if (currentActionDict[Constants.Constants.ActionParameter] == Constants.Constants.LearnByCash) //Research is bought with cash
                        {
                            var node = data.ResearchNodes.FirstOrDefault(n => n.Name == currentObjectName);
                            if (node == null)
                                LogService.Log(db, data.Corporation, "ERROR: Attempting to add node " + currentObjectName +
                                             " that does not exist.");
                            else
                            {
                                if (node.CashCost > data.Corporation.Cash)
                                    LogService.Log(db, data.Corporation, "Error: Not enough cash to purchase research node " + currentObjectName);
                                else
                                {
                                    data.Corporation.Cash -= node.CashCost;
                                    var lrn = new LearnedResearchNode
                                    {
                                        Corporation = data.Corporation,
                                        ResearchNode = researchNode
                                    };
                                    db.Add(lrn);
                                    LogService.Log(db, data.Corporation, "Research " + currentObjectName + " purchased.");
                                }
                            }
                        }
                        else if (currentActionDict[Constants.Constants.ActionParameter] == Constants.Constants.SetActiveResearch) //Research is set to active
                        {

                            data.ActiveResearchNodes.ForEach(n => n.Active = false);

                            var active =
                                data.ActiveResearchNodes.FirstOrDefault(n => n.ResearchNode.Name == currentObjectName);
                            
                            if (active != null) //If not null, we have already started researching it, so set to false
                            {
                                active.Active = true;
                                var toRemove = data.ActiveResearchNodes.Where(n => n.Id != active.Id);
                                db.Remove(toRemove);
                            }
                            else
                            {
                                active = new ActiveResearchNode();
                                active.ResearchNode = researchNode;
                                active.Corporation = data.Corporation;
                                active.Active = true;
                                db.Add(active);
                            }
                            LogService.Log(db, data.Corporation, "Set " + currentObjectName + " to active research. ");
                        }

                        
                        var validated = data.LearnableResearchNodeNames.Contains(currentObjectName);
                        if (!validated)
                        {
                            LogService.Log(db, data.Corporation, "Error: Already learned node " + currentObjectName);
                        }
                    }
                    //#CancelActiveResearch
                    else if (specifierSplit[0] == Constants.Constants.CancelActiveResearch)
                    {
                        if (!data.ActiveResearchNodesSchemaNodes.Select(n => n.Name).Contains(specifierSplit[1]))
                        {
                            LogService.Log(db, data.Corporation, "Cannot cancel research for " + specifierSplit[1] + ". It is not currently being researched.");
                            continue;
                        }
                        var nodeToCancel = db.ActiveResearchNodes.FirstOrDefault(
                            n => n.Corporation.Id == data.Corporation.Id && n.ResearchNode.Name == specifierSplit[1]);
                        if (nodeToCancel == null)
                        {
                            LogService.Log(db, data.Corporation, specifierSplit[1] + " is not a valid research node name.");
                            continue;
                        }
                        db.Remove(nodeToCancel);
                        LogService.Log(db, data.Corporation, "Canceled research for " + specifierSplit[1] + ".");
                    }
                    //#HirePerson
                    else if (specifierSplit[0] == Constants.Constants.HirePerson)
                    {
                        var alreadyHired =
                            CorporationPerson.GetPeopleByHiredStatus(true, data.CorporationPersons).ToList();
                        var entity = data.CorporationPersons.FirstOrDefault(cp => cp.Person.Name == currentObjectName);
                        if (entity == null)
                        {
                            LogService.Log(db, data.Corporation, "Cannot find entity with name: " + currentObjectName + "!");
                            continue;
                        }
                        if (alreadyHired.Select(h => h.Person.Name).Contains(currentObjectName))
                        {
                            LogService.Log(db, data.Corporation, "Already hired " + currentObjectName + "! Action could not be completed.");
                            continue;
                        }
                        if (data.Corporation.Cash < entity.Person.TurnSalary)
                        {
                            LogService.Log(db, data.Corporation, "Not enough funds to hire " + entity.Person.Name + "!");
                            continue;
                        }
                        entity.Hired = true;
                        data.Corporation.PublicInterest += entity.Person.Celebrity ? Constants.Constants.CelebrityBonus : 0;
                        data.Corporation.Reputation += entity.Person.Celebrity ? Constants.Constants.CelebrityBonus : 0;
                        data.Corporation.RD += entity.Person.Intelligence;
                        data.Corporation.Readiness += entity.Person.Experience;
                        data.Corporation.BusinessMultiplier += (double)entity.Person.Business / 100;
                        LogService.Log(db, data.Corporation, "Hired " + currentObjectName + " as " + entity.Person.Position + " for $" + entity.Person.TurnSalary + " per turn.");
                    }
                    //#FirePerson
                    else if (specifierSplit[0] == Constants.Constants.FirePerson)
                    {
                        var alreadyHired =
                            CorporationPerson.GetPeopleByHiredStatus(true, data.CorporationPersons).ToList();
                        var entity = data.CorporationPersons.FirstOrDefault(cp => cp.Person.Name == currentObjectName);
                        if (entity == null)
                        {
                            LogService.Log(db, data.Corporation, "Cannot find entity with name: " + currentObjectName + "!");
                            continue;
                        }
                        if (!alreadyHired.Select(h => h.Person.Name).Contains(currentObjectName))
                        {
                            LogService.Log(db, data.Corporation, "Cannot fire " + currentObjectName + " because they are not hired!");
                            continue;
                        }
                        entity.Hired = false;
                        data.Corporation.PublicInterest -= entity.Person.Celebrity ? Constants.Constants.CelebrityBonus : 0;
                        data.Corporation.Reputation -= entity.Person.Celebrity ? Constants.Constants.CelebrityBonus : 0;
                        data.Corporation.RD -= entity.Person.Intelligence;
                        data.Corporation.Readiness -= entity.Person.Experience;
                        data.Corporation.BusinessMultiplier -= (double)entity.Person.Business / 100;
                        LogService.Log(db, data.Corporation, "Fired " + currentObjectName + ", saving $" + entity.Person.TurnSalary + " per turn.");
                    }
                }
            }

            db.SaveChanges();
        }



        public static void ResolveState(ApplicationDbContext db, ClientActionModel model, DataCache data)
        {
            data.RefreshCache();
            //Advance research node states
            var nodes = data.ActiveResearchNodes.Where(n => n.Corporation.Id == data.Corporation.Id).ToList();
            nodes.ForEach(n =>
            {
                n.RDInvested += data.Corporation.RD;
                if (n.RDInvested >= n.ResearchNode.RDCost)
                {
                    db.LearnedResearchNodes.Add(new LearnedResearchNode()
                    {
                        Corporation = data.Corporation,
                        ResearchNode = n.ResearchNode,
                    });
                    db.ActiveResearchNodes.Remove(n);
                    LogService.Log(db, data.Corporation, "Learned research node " + n.ResearchNode.Name);
                }
            });
            
            //Take out salaries
            var hired = CorporationPerson.GetPeopleByHiredStatus(true, data.CorporationPersons);
            foreach (var p in hired)
            {
                data.Corporation.Cash -= p.Person.TurnSalary;
                if (data.Corporation.Cash < 0) LogService.Log(db, data.Corporation, "You are unabel to pay your employees! When they don't get paid, their happiness drops, and they might quit.");
            }

            data.Corporation.TurnCount++;
            db.SaveChanges();
        }

        

        public static List<string> ParseScriptValuesForParameter(string script, string keyword)
        {
            var result = new List<string>();
            var splitSemi = script.Split(';');
            foreach (var s in splitSemi)
            {
                var splitEquals = s.Split('=');
                if (splitEquals[0] == keyword)
                {
                    foreach (var item in splitEquals[1].Split('&'))
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public static string GetActionStringForView(string actionType, string actionScript, string actionParameter)
        {
            var result = Constants.Constants.ActionType + "=" + actionType+";";
            var splitSemi = actionScript.Split(';');
            foreach (var s in splitSemi)
            {
                if (s.StartsWith(Constants.Constants.Action))
                {
                    result += s + ";";
                }
            }
            result += Constants.Constants.ActionParameter + "=" + actionParameter + ";|";
            return result;
        }
    }
}
