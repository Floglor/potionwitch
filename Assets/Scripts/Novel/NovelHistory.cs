using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Novel
{
    public class NovelHistory : MonoBehaviour
    {
        [SerializeField] private GameObject _historyParent;
        [SerializeField] private GameObject _historyPrefab;
        
        private List<HistoryLog> _historyLog;

        public void OpenLog()
        {
            _historyParent.SetActive(true);
        }
        
        public void CloseLog()
        {
            _historyParent.SetActive(false);
        }

        public void SaveInHistory(string text, string characterName)
        {
            if (string.IsNullOrEmpty(text))
                return;
            
            HistoryLog log = Instantiate(_historyPrefab, _historyParent.transform).GetComponent<HistoryLog>();
            
            log.SetText(text, characterName);
        }
    }

    internal class HistoryLog : MonoBehaviour
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