using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Novel
{
    public class NovelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI novelText;
        [SerializeField] private Image _characterSprite;
        
        [SerializeField] private GameObject _choiceList;
        [SerializeField] private GameObject _choicePrefab;

        [SerializeField] private NovelHistory _novelHistory;
        
        
        
        private bool _isPaused;
        private Dialogue _currentDialogue;
        private int _currentDialogueIndex;
        
        
        public void StartDialogue(Dialogue dialogue)
        {
            _isPaused = false;
            _currentDialogue = dialogue;
            _currentDialogueIndex = 0;
            
            UpdateTextAndSprite();
        }

        private void UpdateTextAndSprite()
        {
            SetNovelText(_currentDialogue.Lines[_currentDialogueIndex].Text);
            SetCharacterSprite(_currentDialogue.Lines[_currentDialogueIndex].CharacterSprite);
        }

        public void AdvanceDialogue()
        {
            if (_isPaused)
            {
                return;
            }
            
            _currentDialogueIndex++;
            
            if (_currentDialogueIndex >= _currentDialogue.Lines.Count)
            {
                EndDialogue();
                return;
            }
            
            Line currentLine = _currentDialogue.Lines[_currentDialogueIndex];
            
            if (_currentDialogueIndex >= _currentDialogue.Lines.Count)
            {
                EndDialogue();
                return;
            }
            
            UpdateTextAndSprite();
            //_novelHistory.AddLine(currentLine);

            
            if (currentLine.IsInterrupted)
            {
                if (currentLine.InterruptType == DialogueInterruptType.Choice)
                {
                    PauseInteractions();
                    ExecuteChoice(currentLine.InterruptionChoices);
                }
                else
                {
                    PauseInteractions();
                    OpenPotionRequest();
                }
            }
        }

        private void OpenPotionRequest()
        {
            Debug.Log("Open potion request");
        }

        private void PauseInteractions()
        {
            _isPaused = true;
            Debug.Log("PauseInteractions");
        }

        private void ExecuteChoice(List<Choice> currentLineInterruptionChoices)
        {
            foreach (Choice currentLineInterruptionChoice in currentLineInterruptionChoices)
            {
                GameObject choice = Instantiate(_choicePrefab, _choiceList.transform);
                choice.GetComponent<TextMeshProUGUI>().text = currentLineInterruptionChoice.Text;
                choice.GetComponent<Button>().onClick.AddListener(() =>
                {
                    StartDialogue(currentLineInterruptionChoice.NextDialogue);
                    ClearChoices();
                });
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_choiceList.GetComponent<RectTransform>());
        }
        

        private void ClearChoices()
        {
            foreach (Transform child in _choiceList.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void EndDialogue()
        {
            PauseInteractions();
            ClearText();
            Debug.Log("end dialogue");
        }

        private void ClearText()
        {
            SetNovelText("");
        }

        private void SetNovelText(string text)
        {
            novelText.text = text;
        }
        
        private void SetCharacterSprite(Sprite sprite)
        {
            _characterSprite.sprite = sprite;
        }
    }
    

}
