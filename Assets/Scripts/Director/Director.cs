using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace Director
{
    public class Director : MonoBehaviour, IObserver
    {
        private TimeController _timeController;
        private DialogueQueueController _dialogueQueueController;
        public List<QuestDate> Quests;
        public List<PhaseDate> Phases;
        
        public List<Quest> CompletedQuests;
        public List<QuestPhase> CompletedPhases;

        public void Start()
        {
            _timeController = FindObjectOfType<TimeController>();
            _dialogueQueueController = FindObjectOfType<DialogueQueueController>();
            _timeController.Attach(this);
        }

        public void UpdateObserver(ISubject subject)
        {
            CheckDate(subject as TimeController);
            _dialogueQueueController.ResetDialogue();
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
            
            foreach (PhaseDate phaseDate in Phases)
            {
                if (phaseDate.date == timeController.date)
                {
                    StartPhase(phaseDate.phase);
                }
            }
        }

        private void StartQuest(Quest questDateQuest)
        {
            StartPhase(questDateQuest.Phases[0]);
        }
        
        private void StartPhase(QuestPhase questPhase)
        {
            _dialogueQueueController.AddDialogue(questPhase.PhaseDialogue);
        }
        
        public void EndQuest(Quest quest)
        {
            quest.IsCompleted = true;
        }
        
        public void EndPhase(QuestPhase questPhase, Quest quest, QuestPhase nextPhase = null)
        {
            questPhase.IsCompleted = true;

            Phases.Remove(Phases.Find(phaseDate => phaseDate.phase == questPhase));

            if (nextPhase == null)
            {
                quest.IsCompleted = true;
                return;
            }
            
            if (questPhase.NextPhaseTime != 0)
            {
                PhaseDate phaseDate = new PhaseDate
                {
                    date = _timeController.date + questPhase.NextPhaseTime,
                    phase = nextPhase
                };
                
                Phases.Add(phaseDate);
            }
            else
            {
                StartPhase(nextPhase);
            }
        }
    }


    [Serializable]
    public class QuestDate
    {
        public int date;
        public Quest quest;
    }
    
    [Serializable]
    public class PhaseDate
    {
        public int date;
        public QuestPhase phase;
    }
}