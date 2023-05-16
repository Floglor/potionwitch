using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScreenChanger : MonoBehaviour
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        [InfoBox("All indexes must be unique; There should be no numbers missing")] [SerializeField]
        private List<ScreenPage> _screenPages;

        [ShowInInspector]
        private ScreenPage _currentPage;

        [ButtonGroup]
        private void EditorChangeLeft()
        {
            ChangeScreen(ScreenMove.Left);
        }
        [ButtonGroup]
        private void EditorChangeRight()
        {
            ChangeScreen(ScreenMove.Right);
        }

        private void Start()
        {
            _currentPage = FindPage(0);
            _leftButton.onClick.AddListener(() => ChangeScreen(ScreenMove.Left));
            _rightButton.onClick.AddListener(() => ChangeScreen(ScreenMove.Right));
        }

        private void ChangeScreen(ScreenMove screenMove)
        {
            ScreenPage targetPage = FindPage(screenMove);

            if (targetPage == null) return;

            TogglePage(_currentPage, false);
            TogglePage(targetPage, true);

            _currentPage = targetPage;

            ResetButtonStates();
            if (FindPage(screenMove) == null)
            {
                if (screenMove == ScreenMove.Right)
                {
                    _rightButton.interactable = false;
                }
                else
                {
                    _leftButton.interactable = false;
                }
            }
            else
            {
                if (screenMove == ScreenMove.Right)
                {
                    _rightButton.interactable = true;
                }
                else
                {
                    _leftButton.interactable = true;
                }
            }
        }

        private void ResetButtonStates()
        {
            _leftButton.interactable = true;
            _rightButton.interactable = true;
        }

        private ScreenPage FindPage(ScreenMove move)
        {
            int moveVector = move == ScreenMove.Right ? 1 : -1;

            ScreenPage targetPage = null;

            foreach (ScreenPage screenPage in _screenPages.Where(screenPage =>
                         screenPage.Index == _currentPage.Index + moveVector))
            {
                targetPage = screenPage;
            }

            return targetPage;
        }

        private ScreenPage FindPage(int index)
        {
            ScreenPage targetPage = null;

            foreach (ScreenPage screenPage in _screenPages.Where(screenPage =>
                         screenPage.Index == index))
            {
                targetPage = screenPage;
            }

            return targetPage;
        }

        private static void TogglePage(ScreenPage page, bool state)
        {
            foreach (GameObject activatedObject in page.ActivatedObjects)
            {
                activatedObject.SetActive(state);
            }
        }
    }

    [Serializable]
    public class ScreenPage
    {
        public List<GameObject> ActivatedObjects;
        public int Index;
        public string Name;
    }

    public enum ScreenMove
    {
        Left,
        Right
    }
}