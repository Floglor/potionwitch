using System.Collections.Generic;
using Novel;
using UnityEngine;

namespace Director
{
    public class PhaseDialogue
    {
        public Dialogue Dialogue;
        public QuestPhase Phase;

        public PhaseDialogue(Dialogue dialogue, QuestPhase phase)
        {
            Dialogue = dialogue;
            Phase = phase;
        }
    }

    public class DialogueQueueController : MonoBehaviour
    {
        private Queue<PhaseDialogue> _dialogueQueue;
        private NovelController _novelController;

        public void Start()
        {
            _dialogueQueue = new Queue<PhaseDialogue>();
            _novelController = FindObjectOfType<NovelController>();
            _novelController.OnEndDialogue += EndDialogue;
        }

        public void AddDialogue(Dialogue dialogue, QuestPhase questPhase = null)
        {
            _dialogueQueue.Enqueue(new PhaseDialogue(dialogue, questPhase));

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