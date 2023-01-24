using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}