using System.Collections.Generic;
using Novel;
using UnityEngine;

namespace Director
{
    public class DialogueQueueController : MonoBehaviour
    {
        private Queue<Dialogue> _dialogueQueue;
        private NovelController _novelController;
        
        //on end dialogue event
        public event System.Action OnEndDialogue;
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

        public void ResetDialogue()
        {
            _novelController.ResetCurrentDialogue();
        }
        
        private void EndDialogue()
        {
            _dialogueQueue.Dequeue();
            
            if (_dialogueQueue.Count > 0)
            {
                _novelController.StartDialogue(_dialogueQueue.Peek());
            }
            
            OnEndDialogue?.Invoke();
        }
        
        public Dialogue GetDialogue()
        {
            return _dialogueQueue.Peek();
        }
        
    }
}