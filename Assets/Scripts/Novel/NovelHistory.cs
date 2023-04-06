using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Novel
{
    public class NovelHistory : MonoBehaviour
    {
        [SerializeField] private GameObject _historyParent;
        [SerializeField] private GameObject _historyPrefab;
        [SerializeField] private GameObject _historyScrollView;
        
        private List<HistoryLog> _historyLog;

        private void Awake()
        {
            _historyLog = new List<HistoryLog>();
        }

        public void ToggleHistory()
        {
            _historyScrollView.SetActive(!_historyScrollView.activeSelf);
        }

        private void OpenLog()
        {
            _historyScrollView.SetActive(true);
        }
        
        private void CloseLog()
        {
            _historyScrollView.SetActive(false);
        }

        public void SaveInHistory(string text, string characterName)
        {
            if (string.IsNullOrEmpty(text))
                return;
            
            HistoryLog log = Instantiate(_historyPrefab, _historyParent.transform).GetComponent<HistoryLog>();
            
            _historyLog.Add(log);
            
            log.SetText(text, characterName);
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_historyParent.transform.GetComponentInParent<RectTransform>());
        }
    }
}