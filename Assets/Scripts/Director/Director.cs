using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace Director
{
    public class Director : MonoBehaviour, IObserver
    {
        public TimeController TimeController;
        private DialogueQueueController _dialogueQueueController;
        public List<QuestDate> Quests;

        public void Start()
        {
            TimeController = FindObjectOfType<TimeController>();
            _dialogueQueueController = FindObjectOfType<DialogueQueueController>();
        }

        public void UpdateObserver(ISubject subject)
        {
            CheckDate(subject as TimeController);
        }

        private void CheckDate(TimeController timeController)
        {
            foreach (QuestDate questDate in Quests)
            {
                if (questDate.date == timeController.date)
                {
                    StartQuest(questDate.quest);
                }
            }
        }

        private void StartQuest(Quest questDateQuest)
        {
            _dialogueQueueController.AddDialogue(questDateQuest.Phases[0].PhaseDialogue);
        }
    }


    [Serializable]
    public class QuestDate : MonoBehaviour
    {
        public int date;
        public Quest quest;
    }
}