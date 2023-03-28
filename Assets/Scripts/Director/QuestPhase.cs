using System;
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
        public Quest LinkedQuest;

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                
                if (value)
                {
                    UnblockPhases();
                }
            }
        }

        private void Start()
        {
            BlockedPhases.ForEach(phase => phase.isBlocked = true);
        }

        private void UnblockPhases() => BlockedPhases.ForEach(phase => phase.isBlocked = false);
    }
}