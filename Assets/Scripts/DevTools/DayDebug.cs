using Misc;
using TMPro;
using UnityEngine;

namespace DevTools
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DayDebug : MonoBehaviour, IObserver
    {
        
        TextMeshProUGUI text;
        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            TimeController timeController = FindObjectOfType<TimeController>();
            timeController.Attach(this);
        }

        public void UpdateObserver(ISubject subject)
        {
            if (subject as TimeController == null) return;
            Debug.Log("Day: " + ((TimeController) subject).date);
            
            text.text = "Day: " + ((TimeController) subject).date;
        }
    }
}