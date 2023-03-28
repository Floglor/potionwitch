using System.Collections.Generic;
using UnityEngine;

namespace Director
{
    public class Quest : MonoBehaviour
    {
        public string QuestName;
        public string QuestDescription;

        public int date;
        
        public bool IsCompleted;
        public bool IsStarted;
        
        public List<QuestPhase> Phases;
    }
}
