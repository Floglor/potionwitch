using System.Collections.Generic;
using Novel;
using UnityEngine;

namespace Director
{
    public class QuestPhase : MonoBehaviour
    {
        public bool isBlocked;
        public int NextPhaseTime;
        public Dialogue PhaseDialogue;
        public List<QuestPhase> BlockedPhases;

        public bool IsCompleted
        {
            get => IsCompleted;
            set
            {
                IsCompleted = value;

                if (value == true)
                {
                    UnblockPhases();
                }
            }
        }

        private void UnblockPhases() => BlockedPhases.ForEach(phase => phase.isBlocked = false);
    }
}