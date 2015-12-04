using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Lib.Schema;
using RPG.Models;

namespace RPG.ViewModels.Action
{
    public class ActionModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public Character Character { get; set; }
        public SchemaRegion Region { get; set; }
        public SchemaLocation Location { get; set; }
        public string Output { get; set; }
        public DialogOption ActiveDialog { get; set; }
        public List<Quest> Quests { get; set; } 
        public List<Trigger> Triggers { get; set; } 
        public List<QuestState> QuestStates { get; set; } 
    }
}
