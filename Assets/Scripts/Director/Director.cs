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
                    Debug.Log("Starting quest " + questDate.quest.QuestName + " at date " + timeController.date + "");
                }
            }
            
            foreach (PhaseDate phaseDate in Phases)
            {
                if (phaseDate.date == timeController.date)
                {
                    StartPhase(phaseDate.phase);
                    Debug.Log("Starting phase " + phaseDate.phase.name + " at date " + timeController.date + "");
                }
            }
        }

        private void StartQuest(Quest questDateQuest)
        {
            StartPhase(questDateQuest.Phases[0]);
        }
        
        private void StartPhase(QuestPhase questPhase)
        {
            _dialogueQueueController.AddDialogue(questPhase.PhaseDialogue, questPhase);
        }

        private void EndQuest(Quest quest)
        {
            quest.IsCompleted = true;
            CompletedQuests.Add(quest);
            Debug.Log("Quest " + quest.QuestName + " completed");
        }
        
        public void EndPhase(QuestPhase questPhase, Quest quest)
        {
            questPhase.IsCompleted = true;
            CompletedPhases.Add(questPhase);
            Debug.Log("Phase " + questPhase.name + " completed");
            
            Phases.Remove(Phases.Find(phaseDate => phaseDate.phase == questPhase));

            QuestPhase nextPhase = FindNextPhase(questPhase, quest);
            
            if (nextPhase == null)
            {
                EndQuest(quest);
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
                Debug.Log("Phase " + nextPhase.name + " will start at date " + phaseDate.date + "");
            }
            else
            {
                StartPhase(nextPhase);
            }
        }

        private static QuestPhase FindNextPhase(QuestPhase questPhase, Quest quest)
        {
            QuestPhase nextPhase = null;
            foreach (QuestPhase phase in quest.Phases)
            {
                if (questPhase == phase)
                {
                    nextPhase = phase;
                    break;
                }
            }

            return nextPhase;
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