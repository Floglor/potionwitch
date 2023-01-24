using TMPro;
using UnityEngine;

namespace Novel
{
    public class HistoryLog : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _characterName;
        
        public void SetText(string text, string characterName)
        {
            _text.text = text;
            _characterName.text = characterName;
        }
    }
}