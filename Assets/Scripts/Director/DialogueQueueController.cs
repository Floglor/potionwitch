using System;
using System.Collections.Generic;
using Novel;
using QFSW.QC;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Director
{
    public class DialogueQueueController : MonoBehaviour
    {
        [ShowInInspector] private Queue<PhaseDialogue> _dialogueQueue;
        private NovelController _novelController;

        public bool _hasDialogue;

        public event Action<bool> HasDialogueInQueue;
        
        public bool HasDialogue
        {
            get => _hasDialogue;
            set
            {
                _hasDialogue = value;
                HasDialogueInQueue.Invoke(_hasDialogue);
                
            }
        }
        
        

        public void Start()
        {
            _dialogueQueue = new Queue<PhaseDialogue>();
            _novelController = FindObjectOfType<NovelController>();
            _novelController.OnEndDialogue += EndDialogue;
        }

        public void AddDialogue(Dialogue dialogue, QuestPhase questPhase = null)
        {
            Debug.Log($"Adding dialogue {dialogue.name}");
            _dialogueQueue.Enqueue(new PhaseDialogue(dialogue, questPhase));
            HasDialogue = true;
        }

        [Command("AdvanceDialogue")]
        public void AdvanceQueue()
        {
            Debug.Log(_dialogueQueue.Count);
            if (_dialogueQueue.Count == 0)
            {
                HasDialogue = false;
                return;
            }

            _novelController.StartDialogue(_dialogueQueue.Peek().Dialogue);
        }

        public void ResetDialogue()
        {
            _novelController.ResetCurrentDialogue();
        }

        private void EndDialogue()
        {
            PhaseDialogue phaseDialogue = _dialogueQueue.Peek();

            if (phaseDialogue.Phase != null)
            {
                phaseDialogue.Phase.IsCompleted = true;
                FindObjectOfType<Director>().EndPhase(phaseDialogue.Phase, phaseDialogue.Phase.LinkedQuest);
            }

            _dialogueQueue.Dequeue();

            if (_dialogueQueue.Count > 0)
            {
                _novelController.StartDialogue(_dialogueQueue.Peek().Dialogue);
            }
        }

        public Dialogue GetDialogue()
        {
            return _dialogueQueue.Peek().Dialogue;
        }
    }
}