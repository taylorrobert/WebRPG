﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Constants
{
    public class Constants
    {
        //Alert styles
        public const string DismissableAlertNormal = "alert alert-info alert-dismissable";

        //Colors
        public const string Money = "#5cb85c";

        //KobolScript Keywords-----------------------------------

        //Top Level Keywords
        public const string Cost = "Cost";
        public const string Condition = "Condition";
        public const string Success = "Success";
        public const string Fail = "Fail";
        public const string Unlocks = "Unlocks";
        public const string PrereqNode = "Prereq";
        public const string Action = "Action";
        public const string ActionType = "ActionType";
        public const string ActionParameter = "ActionParameter";
        public const string TriggerCondition = "TriggerCondition";
        public const string Node = "Node";

        //Action Types
        public const string ActionTypeResearch = "Research";
        public const string HR = "HR";
        public const string Contract = "Contract";

        //Actions
        public const string AlterAttribute = "AlterAttribute";
        public const string LearnResearch = "LearnResearch";
        public const string CancelActiveResearch = "CancelActiveResearch";
        public const string HirePerson = "HirePerson";
        public const string FirePerson = "FirePerson";
        public const string AdvanceContract = "AdvanceContract";
        public const string StartContract = "StartContract";

        //Parameters
        public const string LearnByCash = "LearnByCash";
        public const string SetActiveResearch = "SetActiveResearch";
        public const string Empty = "Empty";
        public const string Complete = "Complete";

        //Conditionals
        public const string HasAttributeGreaterThan = "HasAttributeGreaterThan";
        public const string HasAttributeLessThan = "HasAttributeLessThan";
        public const string HasAttributeEqualTo = "HasAttributeEqualTo";
        public const string HasResearch = "HasResearch";

        //End KobolScript Keywords--------------------------------



        public const int CelebrityBonus = 10;

    }
}
