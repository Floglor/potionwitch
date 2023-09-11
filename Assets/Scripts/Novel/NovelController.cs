using System;
using System.Collections.Generic;
using Alchemy;
using Inventory;
using Sirenix.OdinInspector;
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
        [SerializeField] private Button _potionSlotCancelButton;

        [SerializeField] private List<GameObject> _advanceDialogueButtons;


        public event System.Action OnPauseInteractions;
        public event System.Action OnResumeInteractions;


        private Dialogue _currentDialogue;


        [OnValueChanged("OnValueChanged")]
        public Dialogue CurrentDialogue
        {
            get => _currentDialogue;
            set => _currentDialogue = value;
        }

        private bool _isDialogueRunning;

        private bool _isPaused;

        public bool IsDialogueRunning
        {
            get => _isDialogueRunning;

            private set
            {
                if (_isDialogueRunning != value)
                {
                    _isDialogueRunning = value;
                    OnIsPausedChanged(value);
                }
            }
        }

        public virtual void OnIsPausedChanged(bool newValue)
        {
            IsPausedChanged?.Invoke(newValue);
        }

        public event System.Action<bool> IsPausedChanged;

        private int _currentDialogueIndex;

        //OnEndDialogue event
        public event System.Action OnEndDialogue;

        private void Awake()
        {
            _characterSpriteCenter.gameObject.SetActive(false);
            _characterSpriteLeft.gameObject.SetActive(false);
            _characterSpriteRight.gameObject.SetActive(false);
            novelText.text = "";
            novelName.text = "";
        }

        private void OnValueChanged()
        {
            Debug.Log("OnValueChanged");
        }

        public void ResetCurrentDialogue()
        {
            if (CurrentDialogue != null)
            {
                StartDialogue(CurrentDialogue);
            }

            ClosePotionRequest();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            UnpauseInteractions();

            if (dialogue is null)
            {
                Debug.Log("Dialogue is null");
                return;
            }

            if (novelText.gameObject.activeInHierarchy)
                IsDialogueRunning = true;

            _isPaused = false;
            CurrentDialogue = dialogue;
            _currentDialogueIndex = 0;

            UpdateTextAndSprite();
        }

        private void UpdateTextAndSprite()
        {
            Line currentLine = CurrentDialogue.Lines[_currentDialogueIndex];

            SetNovelText(currentLine.Text,
                currentLine.TargetCharacter != null
                    ? currentLine.TargetCharacter.Name
                    : "Witch");

            _novelHistory.SaveInHistory(currentLine.Text, currentLine.TargetCharacter != null
                ? currentLine.TargetCharacter.Name
                : "Witch");


            if (CurrentDialogue.Lines[_currentDialogueIndex].TargetCharacter != null)
                SetCharacterSprite(CurrentDialogue.Lines[_currentDialogueIndex].CharacterSprite,
                    CurrentDialogue.Lines[_currentDialogueIndex].Position);
        }

        public void AdvanceDialogue()
        {
            if (_isPaused)
            {
                return;
            }

            _currentDialogueIndex++;

            if (CurrentDialogue == null)
            {
                Debug.Log("No dialogue to advance");
                return;
            }

            if (_currentDialogueIndex >= CurrentDialogue.Lines.Count)
            {
                EndDialogue();
                return;
            }


            Line currentLine = CurrentDialogue.Lines[_currentDialogueIndex];

            if (currentLine.ChangeUsedSprite)
            {
                SetCharacterSprite(currentLine.RemoveCharacterSprite
                        ? null
                        : currentLine.CharacterSprite,
                    currentLine.UsedSpritePosition);

                SetCharacterSprite(currentLine.ChangeSpriteTo, currentLine.UsedSpritePosition);
            }

            if (_currentDialogueIndex >= CurrentDialogue.Lines.Count)
            {
                EndDialogue();
                return;
            }

            UpdateTextAndSprite();


            if (currentLine.IsInterrupted)
            {
                switch (currentLine.InterruptType)
                {
                    case DialogueInterruptType.Choice:
                        PauseInteractions();
                        ExecuteChoice(currentLine.InterruptionChoices);
                        break;
                    case DialogueInterruptType.Potion:
                        PauseInteractions();
                        OpenPotionRequest(currentLine.interruptionPotion);
                        break;
                    case DialogueInterruptType.Dialogue:
                        EndDialogue();
                        StartDialogue(currentLine.InterruptionDialogue);
                        break;
                    default:
                        break;
                }
            }
        }

        private void OpenPotionRequest(PotionEffect potionEffect)
        {
            Debug.Log("Open potion request");
            _potionSlot.gameObject.SetActive(true);

            IsDialogueRunning = false;

            _potionSlotButton.onClick.AddListener(() =>
            {
                IItem item = _potionSlot.GetItem();

                if (item == null)
                    return;

                if (!(item is Potion potion))
                {
                    Debug.Log("provided item is not a potion");
                }
                else if (potion.GetEffectId() != potionEffect.GetEffectId())
                {
                    Debug.Log("provided potion is not the correct effect");
                }
                else
                {
                    UnpauseInteractions();
                    AdvanceDialogue();
                    ClosePotionRequest();
                    _potionSlot.ClearItem();
                }
            });

            //_potionSlotCancelButton.onClick.AddListener(() =>
            //{
            //    PostponeDialogue();
            //});
        }

        private void PostponeDialogue()
        {
            throw new NotImplementedException();
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
            foreach (GameObject advanceDialogueButton in _advanceDialogueButtons)
            {
                advanceDialogueButton.SetActive(false);
            }
        }

        private void UnpauseInteractions()
        {
            _isPaused = false;
            Debug.Log("UnpauseInteractions");
            foreach (GameObject advanceDialogueButton in _advanceDialogueButtons)
            {
                advanceDialogueButton.SetActive(true);
            }
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
                    UnpauseInteractions();
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
            ClearSprites();
            Debug.Log("end dialogue");
            OnEndDialogue?.Invoke();
            _currentDialogue = null;
            IsDialogueRunning = false;
        }

        private void ClearSprites()
        {
            SetCharacterSprite(null, DialoguePosition.Center);
            SetCharacterSprite(null, DialoguePosition.Left);
            SetCharacterSprite(null, DialoguePosition.Right);
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