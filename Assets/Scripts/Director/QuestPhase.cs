using System.Collections.Generic;
using Novel;
using UnityEngine;

namespace Director
{
    public class QuestPhase : MonoBehaviour
    {
        public Dialogue PhaseDialogue;
        public List<QuestPhase> QuestBlockers;
        public bool IsCompleted;
    }
}