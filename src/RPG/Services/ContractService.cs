using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RPG.Models;
using System.Reflection;

namespace RPG.Services
{
    public class ContractService
    {
        public static List<ContractInMemory> ParseContractFiles(List<Contract> contracts, List<CorporationContract> corpContracts)
        {
            var items = new List<ContractInMemory>();
            foreach (var c in contracts)
            {
                var corpCon = corpContracts.FirstOrDefault(cc => cc.Contract.Name == c.Name);
                var cim = ParseContractFile(c, corpCon);
                items.Add(cim);
            }
            return items;
        }

        public static ContractInMemory ParseContractFile(Contract con, CorporationContract corpCon)
        {
            var c = new ContractInMemory();
            c.Name = con.Name;

            if (corpCon != null)
            {
                c.Active = corpCon.Active;
                c.Complete = corpCon.Complete;
                c.Accepted = corpCon.Accepted;
                c.NodeNumber = corpCon.NodeNumber;
                c.HasCorrespondingCorporationContract = true;
            }
            else
            {
                c.HasCorrespondingCorporationContract = false;
            }

            var script = Regex.Replace(con.Script, @"\t|\n|\r", "");
            var splitByPipes = script.Split('|');

            var triggerCondition =
                splitByPipes.FirstOrDefault(sbp => sbp.StartsWith(Constants.Constants.TriggerCondition));
            var triggerInner = triggerCondition.Split('=');
            var triggerinner2 = triggerInner[1].Split('{', '}');
            var triggerInner3 = triggerinner2[1].Split(':');
            var triggerAttributeAndValue = triggerInner3[1].Split(',');

            c.TriggerCondition = new TriggerConditionInMemory()
            {
                Condition = triggerInner3[0],
                Attribute = triggerAttributeAndValue[0],
                Value = triggerAttributeAndValue[1]
            };

            var nodes = splitByPipes.Where(sbp => sbp.StartsWith(Constants.Constants.Node)).ToList();
            foreach (var n in nodes)
            {
                var contractNode = new ContractNodeInMemory();
                var splitByHash = n.Split('#');
                var nodeNumber = splitByHash[0].Substring(4);
                contractNode.NodeNumber = Convert.ToInt32(nodeNumber);
                    //We have split by equals, so we want the number after the word "Node"
                var inner = splitByHash[1].Split('{', '}')[1].Split(';');

                foreach (var op in inner)
                {
                    if (op.StartsWith("Text")) contractNode.Text = op.Split('=')[1];
                    else if (op.StartsWith("Option"))
                    {
                        var option = new ContractOptionInMemory();
                        var splitByEqualsThenColon = op.Split('=')[1].Split(':');
                        option.OptionCommand = splitByEqualsThenColon[0];
                        option.NextNode = splitByEqualsThenColon[1];
                        option.OptionText = splitByEqualsThenColon[2];
                        contractNode.ContractOptions.Add(option);
                    }
                }

                c.ContractNodes.Add(contractNode);
            }
            return c;
        }

        public static void MoveContractsToActive(ApplicationDbContext db, DataCache data)
        {
            var newContracts = false;
            var exclude = data.CorporationContracts.Select(cc => cc.Contract.Name).ToList();
            foreach (var c in data.ContractsInMemory.Where(c => !exclude.Contains(c.Name)))
            {
                //HasAttributeGreaterThan
                if (c.TriggerCondition.Condition == Constants.Constants.HasAttributeGreaterThan)
                {
                    var attribute = data.Corporation.GetType().GetProperty(c.TriggerCondition.Attribute);
                    if (Convert.ToInt32(attribute.GetValue(data.Corporation)) >
                        Convert.ToInt32(c.TriggerCondition.Value))
                    {
                        var schemaContract = db.Contracts.FirstOrDefault(con => con.Name == c.Name);

                        var active = new CorporationContract()
                        {
                            Active = true,
                            Accepted = false,
                            Complete = false,
                            Contract = schemaContract,
                            Corporation = data.Corporation
                        };
                        db.CorporationContracts.Add(active);
                        newContracts = true;
                    }
                }
            }

            if (newContracts) LogService.Log(db, data.Corporation, "New contracts are available.");
        }
    }
}

