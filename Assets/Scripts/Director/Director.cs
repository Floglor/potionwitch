using System;
using System.Collections.Generic;
using Misc;
using Sirenix.Utilities;
using UnityEngine;

namespace Director
{
    public class Director : MonoBehaviour, IObserver
    {
        private TimeController _timeController;
        private DialogueQueueController _dialogueQueueController;
        private DialogueVariableManager _dialogueVariableManager;
        public List<Quest> Quests;
        public List<PhaseAndDate> Phases;

        public List<Quest> CompletedQuests;
        public List<QuestPhase> CompletedPhases;

        public void Start()
        {
            _timeController = FindObjectOfType<TimeController>();
            _dialogueQueueController = FindObjectOfType<DialogueQueueController>();
            _dialogueVariableManager = FindObjectOfType<DialogueVariableManager>();
            _timeController.Attach(this);
        }

        public void UpdateObserver(ISubject subject)
        {
            CheckDate(subject as TimeController);
            _dialogueQueueController.ResetDialogue();
        }

        private void CheckDate(TimeController timeController)
        {
            foreach (Quest questDate in Quests)
            {
                if (timeController.date < questDate.date || questDate.IsStarted) continue;

                Debug.Log("Starting quest " + questDate.QuestName + " at date " + timeController.date + "");

                //No Quest Condition
                if (questDate.Condition.FirstVariableName.Equals(""))
                {
                    StartQuest(questDate);
                    continue;
                }

                if (_dialogueVariableManager.CheckVariable(
                        questDate.Condition.FirstVariableName,
                        questDate.Condition.SecondVariableName,
                        questDate.Condition.Operation))
                {
                    StartQuest(questDate);
                }
            }

            for (int index = 0; index < Phases.Count; index++)
            {
                PhaseAndDate phaseDate = Phases[index];
                if (timeController.date >= phaseDate.date)
                {
                    Debug.Log("Starting phase " + phaseDate.phase.name + " at date " + timeController.date + "");
                    StartPhase(phaseDate.phase);
                    Phases.Remove(phaseDate);
                }
            }
        }

        private void StartQuest(Quest quest)
        {
            if (quest.Phases.IsNullOrEmpty())
            {
                Debug.Log($"Quest {quest.name} has no phases!");
                return;
            }

            if (quest.Phases[0].isBlocked)
            {
                Debug.Log($"Quest {quest.name} is blocked. Not starting it.");
                return;
            }

            quest.IsStarted = true;
            StartPhase(quest.Phases[0]);
        }

        private void StartPhase(QuestPhase questPhase)
        {
            if (questPhase.isBlocked)
            {
                Debug.Log($"QuestPhase {questPhase.name} is blocked. Not starting it.");
                return;
            }

            _dialogueQueueController.AddDialogue(questPhase.PhaseDialogue, questPhase);
        }

        private void EndQuest(Quest quest)
        {
            quest.IsCompleted = true;
            CompletedQuests.Add(quest);
            Debug.Log("Quest " + quest.QuestName + " completed");

            if (quest.Reward != null)
            {
                quest.Reward.TriggerBehaviour();
            }

            Quests.Remove(quest);

            if (quest.ChangingVariables)
            {
                if (quest.IsChangingBool)
                    _dialogueVariableManager.ChangeVariable(quest.CompletionVariableName,
                        quest.CompletionVariableBool);
                else 
                    _dialogueVariableManager.ChangeVariable(quest.CompletionVariableName,
                        quest.CompletionVariableInt);
            }
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

            CheckIfThereIsNextPhase(questPhase, nextPhase);
        }

        private void CheckIfThereIsNextPhase(QuestPhase questPhase, QuestPhase nextPhase)
        {
            if (nextPhase == null) return;

            if (questPhase.NextPhaseTime != 0)
            {
                PhaseAndDate phaseAndDate = new PhaseAndDate
                {
                    date = _timeController.date + questPhase.NextPhaseTime,
                    phase = nextPhase
                };

                Phases.Add(phaseAndDate);
                Debug.Log("Phase " + nextPhase.name + " will start at date " + phaseAndDate.date + "");
            }
            else
            {
                StartPhase(nextPhase);
            }
        }

        private static QuestPhase FindNextPhase(QuestPhase questPhase, Quest quest)
        {
            QuestPhase nextPhase = null;

            for (int index = 0; index < quest.Phases.Count; index++)
            {
                QuestPhase phase = quest.Phases[index];

                if (questPhase == phase && index + 1 < quest.Phases.Count)
                {
                    nextPhase = quest.Phases[index + 1];
                    break;
                }
            }

            return nextPhase;
        }
    }


    [Serializable]
    public class QuestAndDate
    {
        public int date;
        public Quest quest;
    }

    [Serializable]
    public class PhaseAndDate
    {
        public int date;
        public QuestPhase phase;
    }
}