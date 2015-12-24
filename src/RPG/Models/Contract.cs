using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Script { get; set; }
    }

    public class CorporationContract
    {
        public int Id { get; set; }
        public Corporation Corporation { get; set; }
        public Contract Contract { get; set; }
        public bool Active { get; set; }
        public bool Accepted { get; set; }
        public bool Complete { get; set; }
        public int NodeNumber { get; set; }
    }

    public class ContractInMemory
    {
        public ContractInMemory()
        {
            ContractNodes = new List<ContractNodeInMemory>();
        }

        public string Name { get; set; }
        public List<ContractNodeInMemory> ContractNodes { get; set; } 
        public TriggerConditionInMemory TriggerCondition { get; set; }
        public bool Active { get; set; }
        public bool Accepted { get; set; }
        public bool Complete { get; set; }
        public bool HasCorrespondingCorporationContract { get; set; }
        public int NodeNumber { get; set; }
    }

    public class ContractNodeInMemory
    {
        public ContractNodeInMemory()
        {
            ContractOptions = new List<ContractOptionInMemory>();
        }

        public string Text { get; set; }
        public int NodeNumber { get; set; }
        public List<ContractOptionInMemory> ContractOptions { get; set; } 
    }

    public class ContractOptionInMemory
    {
        public string OptionCommand { get; set; }
        public string NextNode { get; set; }
        public string OptionText { get; set; }
    }

    public class TriggerConditionInMemory
    {
        public string Condition { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }
    }







    
}
