using System.Collections.Generic;
using Novel;
using UnityEngine;

namespace Director
{
    public class DialogueQueueController : MonoBehaviour
    {
        private Queue<Dialogue> _dialogueQueue;
        private NovelController _novelController;
        
        public void Start()
        {
            _dialogueQueue = new Queue<Dialogue>();
            _novelController = FindObjectOfType<NovelController>();
            _novelController.OnEndDialogue += EndDialogue;
        }
        
        public void AddDialogue(Dialogue dialogue)
        {
            _dialogueQueue.Enqueue(dialogue);
        }

        private void EndDialogue()
        {
            _dialogueQueue.Dequeue();
            
            if (_dialogueQueue.Count > 0)
            {
                _novelController.StartDialogue(_dialogueQueue.Peek());
            }
        }
        
        public Dialogue GetDialogue()
        {
            return _dialogueQueue.Peek();
        }
        
    }
}