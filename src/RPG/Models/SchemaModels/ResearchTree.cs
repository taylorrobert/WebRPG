using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using RPG.Services;

namespace RPG.Models.SchemaModels
{
    public class ResearchTree
    {
        public string Name { get; set; }
        public ResearchNode StartingNode { get; set; }
        public int Id { get; set; }

        public ResearchTree()
        {

        }

        public static void CreateTestTreeInDB(ApplicationDbContext db, Corporation corp)
        {
            var node1 = new ResearchNode
            {
                Name = "Basic Rocketry",
                Description = "The basics.",
                RDCost = 10,
                CashCost = 50000,
                Script = "Action=LearnResearch:Basic Rocketry;Unlocks=Intermediate Rocketry;"
            };

            var node2 = new ResearchNode
            {
                Name = "Intermediate Rocketry",
                Description = "Werner von Braun would be delighted.",
                RDCost = 30,
                CashCost = 400000,
                Script = "Action=LearnResearch:Intermediate Rocketry;Prereq=Basic Rocketry;Unlocks=Advanced Rocketry"
            };

            var node3 = new ResearchNode
            {
                Name = "Advanced Rocketry",
                Description = "Einstein ain't got shit on these badass rockets.",
                RDCost = 75,
                CashCost = 4000000,
                Script = "Action=LearnResearch:Advanced Rocketry;Prereq=Intermediate Rocketry&Basic Rocketry;"
            };

            var learnedNode1 = new LearnedResearchNode()
            {
                Corporation = corp,
                ResearchNode = node1
            };

            db.Add(node1);
            db.Add(node2);
            db.Add(node3);
            db.Add(learnedNode1);
            db.SaveChanges();
        }

        public static ResearchTree BuildTreeForView(ActionModel model, ApplicationDbContext db)
        {
            var nodes = db.ResearchNodes.Where(x => true).ToList();
            var learnedNodes = db.LearnedResearchNodes.Where(lrn => model.Corporation.Id == lrn.Corporation.Id).Include(n => n.ResearchNode).Select(n=>n.ResearchNode.Name).ToList();
            var activeNodes =
                db.ActiveResearchNodes.Where(arn => arn.Corporation.Id == model.Corporation.Id).Select(n => n.ResearchNode.Name).ToList();

            var t = new ResearchTree
            {
                Name = "Rocketry",
                StartingNode = nodes.FirstOrDefault(n => n.Name == "Basic Rocketry")
            };

            nodes.ForEach(
                node =>
                    node.Enabled =
                        !learnedNodes.Contains(node.Name) 
                        && !activeNodes.Contains(node.Name)//Not actively being researched or already researched
                        && node.PrereqsMet(db, model.DataCache.LearnedResearchNodes)); //has prereqs met or has none

            return t;
        }

        public static string GetTreeBody(ResearchTree tree, ApplicationDbContext db, ActionModel model)
        {
            var text = "";
            text = SerializeRecursive(tree.StartingNode, text, db, model);
            

            return text;


        }

        private static string SerializeRecursive(ResearchNode node, string text, ApplicationDbContext db, ActionModel model)
        {
            text += "<tr>";
            text += "<td>" + node.Name + "</td>";
            
            string pre = "";
            node.GetPrerequisiteNodes(db).ToList().ForEach(n => pre += n.Name + ", ");
            text += "<td>" + pre + "</td>";

            string nextNodes = "";
            node.GetNextNodes(db).ToList().ForEach(n => nextNodes += n.Name + ", ");
            text += "<td>" + nextNodes + "</td>";

            text += "<td>" + node.RDCost + "</td>";

            text += "<td>" + node.CashCost + "</td>";

            var activeNodes =
                db.ActiveResearchNodes.Where(arn => arn.Corporation.Id == model.Corporation.Id).Select(n => n.ResearchNode.Name).ToList();

            if (model.DataCache.LearnedResearchNodes.Select(n => n.Name).Contains(node.Name))
            {
                text += "<td><button disabled class='btn btn-default'>Learned</button></td>";
            }
            else if (activeNodes.Contains(node.Name))
            {
                text += "<td><button class='btn btn-default' data-toggle='button' type='button' onclick='addSingleAction(&quot;" +
                    ActionService.GetActionStringForView(Constants.Constants.ActionTypeResearch, "Action="+Constants.Constants.CancelActiveResearch + ":" + node.Name,
                        Constants.Constants.Empty) + @";&quot;, this)'>Cancel</button></td>";
            }
            else
            {
                var cashEnabledText = model.DataCache.Corporation.Cash >= node.CashCost && node.Enabled ? "enabled" : "disabled";

                text +=
                    @"<td><button " + cashEnabledText +
                    " class='btn btn-default' type='button' data-toggle='button' onclick='addSingleAction(&quot;" +
                    ActionService.GetActionStringForView(Constants.Constants.ActionTypeResearch, node.Script,
                        Constants.Constants.LearnByCash) + @";&quot;, this)'>Cash ("+node.CashCost+")</button>";

                
                var researchEnabledText = node.Enabled ? "enabled" : "disabled"; ;

                text +=
                    @"<button " + researchEnabledText + " data-toggle='button' class='btn btn-default' type='button' onclick='addSingleAction(&quot;" +
                    ActionService.GetActionStringForView(Constants.Constants.ActionTypeResearch, node.Script,
                        Constants.Constants.SetActiveResearch) + @";&quot;, this)'>Set Active Research (" +
                    node.GetTurnsToResearch(model.Corporation.RD) + ")</button></td>";
            }

            text += "</tr>";

            if (node.GetNextNodes(db).Any()) node.GetNextNodes(db).ToList().ForEach(n => text = SerializeRecursive(n, text, db, model));
            return text;
        }
    }

    public class ResearchNode
    {
        public ResearchNode()
        {
            _prerequisiteNodes = new List<ResearchNode>();
            _nextNodes = new List<ResearchNode>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RDCost { get; set; }
        public long CashCost { get; set; }
        public string Script { get; set; }

        [NotMapped]
        private List<ResearchNode> _prerequisiteNodes { get; set; }

        [NotMapped]
        private List<ResearchNode> _nextNodes { get; set; }

        [NotMapped]
        public bool Enabled { get; set; }

        public string GetTurnsToResearch(int corpRD)
        {
            if (corpRD == 0) return "Infinite";
            var turns = Math.Ceiling((decimal)RDCost/(decimal) corpRD).ToString();
            return turns;
        }

        public string GetRemainingTurnsToResearch(int corpRD, int invested)
        {
            if (corpRD == 0) return "Infinite";
            var turns = Math.Ceiling((RDCost - invested) / (double)corpRD).ToString();
            return turns;
        }

        public bool PrereqsMet(ApplicationDbContext db, IEnumerable<ResearchNode> learnedNodes)
        {
            var prereqs = GetPrerequisiteNodes(db).Select(n => n.Name).ToList();
            var learnedNames = learnedNodes.Select(n => n.Name).ToList();
            bool ok = true;

            if (!learnedNames.Any()) return ok;

            prereqs.ForEach(n =>
            {
                if (!learnedNames.Contains(n)) ok = false;
            });
            return ok;
        }

        public List<ResearchNode> GetPrerequisiteNodes(ApplicationDbContext db)
        {
            if (!_prerequisiteNodes.Any())
            {
                var nodeList = ActionService.ParseScriptValuesForParameter(Script, Constants.Constants.PrereqNode);
                _prerequisiteNodes = db.ResearchNodes.Where(n => nodeList.Contains(n.Name)).ToList();
            }
            return _prerequisiteNodes;
        }

        public List<ResearchNode> GetNextNodes(ApplicationDbContext db)
        {
            if (!_nextNodes.Any())
            {
                var nodeList = ActionService.ParseScriptValuesForParameter(Script, Constants.Constants.Unlocks);
                _nextNodes = db.ResearchNodes.Where(n => nodeList.Contains(n.Name)).ToList();
            }
            return _nextNodes;
        }
    }

    public class LearnedResearchNode
    {
        public LearnedResearchNode() { }

        public int Id { get; set; }
        public Corporation Corporation { get; set; }
        public ResearchNode ResearchNode { get; set; }
    }

    

}
