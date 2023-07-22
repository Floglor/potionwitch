using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Map
{
    [Serializable]
    public class MapObject : MonoBehaviour
    {
        [SerializeField]
        private Button _interactable;
        
        [SerializeField]
        private List<GameObject> _rooms;

        [SerializeField] private string _roomName;
        

        public void SetInteractable(UnityAction buttonEvent)
        {
            _interactable.onClick.AddListener(buttonEvent);
        }
        public string GetRoomName()
        {
            return _roomName;
        }

        public List<GameObject> GetRooms()
        {
            return _rooms;
        }
    }
}