using System.Collections.Generic;
using Alchemy;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Novel
{
    public class NovelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI novelText;
        [SerializeField] private TextMeshProUGUI novelName;

        [SerializeField] private Image _characterSpriteCenter;
        [SerializeField] private Image _characterSpriteLeft;
        [SerializeField] private Image _characterSpriteRight;

        [SerializeField] private GameObject _choiceList;
        [SerializeField] private GameObject _choicePrefab;

        [SerializeField] private NovelHistory _novelHistory;
        [SerializeField] private InventorySlot _potionSlot;
        [SerializeField] private Button _potionSlotButton;
        
        public event System.Action OnPauseInteractions;
        public event System.Action OnResumeInteractions;


        private bool _isPaused;
        private Dialogue _currentDialogue;
        private int _currentDialogueIndex;
        
        //OnEndDialogue event
        public event System.Action OnEndDialogue;


        public void ResetCurrentDialogue()
        {
            StartDialogue(_currentDialogue);
        }

        public void StartDialogue(Dialogue dialogue)
        {
            _isPaused = false;
            _currentDialogue = dialogue;
            _currentDialogueIndex = 0;

            UpdateTextAndSprite();
        }

        private void UpdateTextAndSprite()
        {
            Line currentLine = _currentDialogue.Lines[_currentDialogueIndex];

            SetNovelText(currentLine.Text,
                currentLine.TargetCharacter != null
                    ? currentLine.TargetCharacter.Name
                    : "Witch");

            _novelHistory.SaveInHistory(currentLine.Text, currentLine.TargetCharacter != null
                ? currentLine.TargetCharacter.Name
                : "Witch");


            SetCharacterSprite(_currentDialogue.Lines[_currentDialogueIndex].CharacterSprite,
                _currentDialogue.Lines[_currentDialogueIndex].Position);
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

            if (currentLine.ChangeUsedSprite)
            {
                SetCharacterSprite(currentLine.RemoveCharacterSprite
                        ? null
                        : currentLine.CharacterSprite,
                    currentLine.UsedSpritePosition);

                SetCharacterSprite(currentLine.ChangeSpriteTo, currentLine.UsedSpritePosition);
            }

            if (_currentDialogueIndex >= _currentDialogue.Lines.Count)
            {
                EndDialogue();
                return;
            }

            UpdateTextAndSprite();


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
                    OpenPotionRequest(currentLine.interruptionPotion);
                }
            }
        }

        private void OpenPotionRequest(PotionEffect potionEffect)
        {
            Debug.Log("Open potion request");
            _potionSlot.gameObject.SetActive(true);
            
            _potionSlotButton.onClick.AddListener(() =>
            {
                IItem item = _potionSlot.GetItem();
                
                if (!(item is Potion potion))
                {
                    Debug.Log("provided item is not a potion");
                }
                else 
                if (potion.GetEffectId() != potionEffect.GetEffectId())
                {
                    Debug.Log("provided potion is not the correct effect");
                }
                else
                {
                    UnpauseInteractions();
                    AdvanceDialogue();
                    ClosePotionRequest();
                }
            });
            
        }

        private void ClosePotionRequest()
        {
            Debug.Log("Close potion request");
            _potionSlot.gameObject.SetActive(false);
            _potionSlotButton.onClick.RemoveAllListeners();
        }
        
        private void HidePotionRequest()
        {
            Debug.Log("Hide potion request");
            _potionSlot.gameObject.SetActive(false);
        }

        private void PauseInteractions()
        {
            _isPaused = true;
            Debug.Log("PauseInteractions");
            
        }
        
        private void UnpauseInteractions()
        {
            _isPaused = false;
            Debug.Log("UnpauseInteractions");
        }

        private void ExecuteChoice(List<Choice> currentLineInterruptionChoices)
        {
            foreach (Choice currentLineInterruptionChoice in currentLineInterruptionChoices)
            {
                GameObject choice = Instantiate(_choicePrefab, _choiceList.transform);
                choice.GetComponent<TextMeshProUGUI>().text = currentLineInterruptionChoice.Text;

                choice.GetComponent<Button>().onClick.AddListener(() =>
                {
                    _novelHistory.SaveInHistory(choice.GetComponent<TextMeshProUGUI>().text, "Witch");
                    StartDialogue(currentLineInterruptionChoice.NextDialogue);
                    ClearChoices();
                });
            }
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
            OnEndDialogue?.Invoke();
        }

        private void ClearText()
        {
            SetNovelText("", "");
        }

        private void SetNovelText(string text, string characterName)
        {
            novelName.text = characterName;
            novelText.text = text;
        }

        private void SetCharacterSprite(Sprite sprite, DialoguePosition position)
        {
            switch (position)
            {
                case DialoguePosition.Center:
                    SetSprite(sprite, _characterSpriteCenter);
                    break;
                case DialoguePosition.Left:
                    SetSprite(sprite, _characterSpriteLeft);
                    break;
                case DialoguePosition.Right:
                    SetSprite(sprite, _characterSpriteRight);
                    break;
            }
        }

        private void SetSprite(Sprite sprite, Image image)
        {
            if (sprite == null)
                image.gameObject.SetActive(false);
            else
            {
                image.gameObject.SetActive(true);
                image.sprite = sprite;
            }
        }
    }
}