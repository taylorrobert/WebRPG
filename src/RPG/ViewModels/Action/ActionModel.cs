using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Lib.Enums;
using RPG.Lib.Schema;
using RPG.Models;

namespace RPG.ViewModels.Action
{
    public class ActionModel
    {
        public ActionModel()
        {
            Quests = new List<Quest>();
            QuestsToStart = new List<string>();
            QuestsToAdvance = new List<QuestStatusResult>();
            Triggers = new List<Trigger>();
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public Character Character { get; set; }
        public SchemaRegion Region { get; set; }
        public SchemaLocation Location { get; set; }
        public string Output { get; set; }
        public DialogOption ActiveDialog { get; set; }
        public List<Quest> Quests { get; set; } 
        public List<string> QuestsToStart { get; set; } 
        public List<QuestStatusResult> QuestsToAdvance { get; set; } 
        public List<Trigger> Triggers { get; set; } 
        public bool FirstLogin { get; set; }
    }

    public class QuestStatusResult
    {
        public bool Completed { get; set; }
        public Quest Quest { get; set; }
    }

    public class TriggerResult
    {
        public TriggerResult(string reference, TriggerMethod method, TriggererType type)
        {
            TriggererReferenceName = reference;
            TriggererType = type;
            TriggerMethod = method;
        }

        public string TriggererReferenceName { get; set; }
        public TriggerMethod TriggerMethod { get; set; }
        public TriggererType TriggererType { get; set; }
    }
}
