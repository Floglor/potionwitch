using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Director
{
    public class Quest : MonoBehaviour
    {
        public QuestCondition Condition;
        public string QuestName;
        public string QuestDescription;

        public bool ChangingVariables;
        
        [ShowIf("@this.ChangingVariables")]
        public string CompletionVariableName;

        [ShowIf("@this.ChangingVariables")]
        public bool IsChangingBool;
        
        [ShowIf("@this.IsChangingBool && this.ChangingVariables")]
        public bool CompletionVariableBool;
        
        [ShowIf("@!this.IsChangingBool && this.ChangingVariables")]
        public int CompletionVariableInt;

        public int date;
        
        public bool IsCompleted;
        public bool IsStarted;
        
        public List<QuestPhase> Phases;
        public QuestCompletion Reward;
    }
}
