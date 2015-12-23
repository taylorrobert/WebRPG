using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using Newtonsoft.Json;
using RPG.Models.SchemaModels;

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
            RefreshResearchNodes();
            RefreshLearnedResearchNodes();
            RefreshLearnableResearchNodeNames();
            RefreshActiveResearchNodesVirtualToSchema();
            RefreshActiveResearchNodesRealNodes();
            RefreshPersons();
            RefreshCorporationPersons();
        }

        public void RefreshResearchNodes()
        {
            ResearchNodes = db.ResearchNodes.Where(n => true).ToList();
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

        public void RefreshPersons()
        {
            Persons = db.Persons.Where(p => true).ToList();
        }

        public void RefreshCorporationPersons()
        {
            CorporationPersons = db.CorporationPersons.Where(p => p.Corporation.Id == Corporation.Id).ToList();
        }
    }
}
