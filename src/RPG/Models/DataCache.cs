using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using Newtonsoft.Json;
using RPG.Models.SchemaModels;
using RPG.Services;

namespace RPG.Models
{
    public class DataCache
    {
        
        public ApplicationUser User { get; set; }
        public Corporation Corporation { get; set; }
        private ApplicationDbContext db { get; set; }

        public List<ResearchNode> ResearchNodes { get; set; }
        public List<ResearchNode> LearnedResearchNodes { get; set; }
        public List<string> LearnableResearchNodeNames { get; set; }
        public List<ResearchNode> EnabledResearchNodes { get; set; } 
        public List<ResearchNode> ActiveResearchNodesSchemaNodes { get; set; } 
        public List<ActiveResearchNode> ActiveResearchNodes { get; set; }
        public List<Person> Persons { get; set; }
        public List<CorporationPerson> CorporationPersons { get; set; }
        public List<Contract> Contracts { get; set; } 
        public List<ContractInMemory> ContractsInMemory { get; set; } 
        public List<CorporationContract> CorporationContracts { get; set; }

        public List<ContractInMemory> PreviousContracts
        {
            get { return ContractsInMemory.Where(c => c.HasCorrespondingCorporationContract && !c.Active).ToList(); }
        }

        public List<ContractInMemory> AcceptedContractsInMemory
        {
            get { return ContractsInMemory.Where(c => c.HasCorrespondingCorporationContract && c.Active && c.Accepted).ToList(); }
        }

        public List<ContractInMemory> AvailableContracts
        {
            get { return ContractsInMemory.Where(c => c.HasCorrespondingCorporationContract && c.Active && !c.Accepted).ToList(); }
        } 

        public DataCache(ApplicationDbContext context, ApplicationUser user, Corporation corporation)
        {
            db = context;
            User = user;
            Corporation = corporation;
            RefreshCache();
        }

        public static DataCache GetDataCache(HttpContext context)
        {
            var cache = context.Session.GetString(context.User.Identity.Name + "DataCache");
            var deserialized = JsonConvert.DeserializeObject<DataCache>(cache);
            return deserialized;
        }

        public static void SaveDataCache(HttpContext context, DataCache cache)
        {
            context.Session.SetString(context.User.Identity.Name + "DataCache", JsonConvert.SerializeObject(cache));
        }

        public void RefreshCache()
        {
            RefreshStaticItems();

            RefreshLearnedResearchNodes();
            RefreshLearnableResearchNodeNames();
            RefreshActiveResearchNodesVirtualToSchema();
            RefreshActiveResearchNodesRealNodes();
            RefreshCorporationPersons();
            RefreshCorporationContracts();
            RefreshContractsInMemory();
        }

        public void RefreshStaticItems()
        {
            RefreshResearchNodes();
            RefreshPersons();
            RefreshContracts();
        }

        

        public void RefreshLearnedResearchNodes()
        {
            var learnedNodes = db.LearnedResearchNodes.Where(n => n.Corporation.Id == Corporation.Id).ToList();
            LearnedResearchNodes = learnedNodes.Select(n => n.ResearchNode).ToList();
        }

        public void RefreshLearnableResearchNodeNames()
        {
            var learnableNodes = new List<string>();
            LearnedResearchNodes.ForEach(nn => nn.GetNextNodes(db).ToList().ForEach(dnode => learnableNodes.Add(dnode.Name)));
            learnableNodes = learnableNodes.Except(LearnedResearchNodes.Select(n => n.Name)).ToList();
            LearnableResearchNodeNames = learnableNodes;
        }

        public void RefreshEnabledResearchNodes()
        {
            var enabledNodes = new List<ResearchNode>(LearnedResearchNodes);
            LearnedResearchNodes.ForEach(nn => nn.GetNextNodes(db).ToList().ForEach(dnode => enabledNodes.Add(dnode)));
            EnabledResearchNodes = enabledNodes;
        }

        public void RefreshActiveResearchNodesVirtualToSchema()
        {
            var activeResearchNodes = db.ActiveResearchNodes.Where(n => n.Corporation.Id == Corporation.Id).ToList();
            ActiveResearchNodesSchemaNodes = activeResearchNodes.Select(n => n.ResearchNode).ToList();
        }

        public void RefreshActiveResearchNodesRealNodes()
        {
            ActiveResearchNodes = db.ActiveResearchNodes.Where(n => n.Corporation.Id == Corporation.Id).ToList();
        }

        public void RefreshCorporationPersons()
        {
            CorporationPersons = db.CorporationPersons.Where(p => p.Corporation.Id == Corporation.Id).ToList();
        }

        public void RefreshCorporationContracts()
        {
            CorporationContracts = db.CorporationContracts.Where(c => c.Corporation.Id == Corporation.Id).ToList();
        }

        public void RefreshContractsInMemory()
        {
            ContractsInMemory = ContractService.ParseContractFiles(Contracts, CorporationContracts);
        }

        //Static, don't change
        public void RefreshContracts()
        {
            Contracts = db.Contracts.Where(c => true).ToList();
        }

        public void RefreshPersons()
        {
            Persons = db.Persons.Where(p => true).ToList();
        }

        public void RefreshResearchNodes()
        {
            ResearchNodes = db.ResearchNodes.Where(n => true).ToList();
        }
    }
}
