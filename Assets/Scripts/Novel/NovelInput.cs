using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Novel
{
    public class NovelInput : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private NovelController _novelController;
        
        
        private void Start()
        {
            foreach (var button in _buttons)
            {
                button.onClick.AddListener(() => _novelController.AdvanceDialogue());
            }
        }
    }
}
