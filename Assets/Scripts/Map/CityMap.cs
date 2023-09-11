using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

namespace Map
{
    public class CityMap : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _defaultScreens;
        [SerializeField] private List<MapObject> _mapObjects;

        [SerializeField] private List<GameObject> _map;


        private List<GameObject> CurrentRooms;

        private void Start()
        {
            //_currentRoom = _defaultScreens;
            SetButtons();
        }

        [Command]
        private void OpenMap()
        {
            ChangeRoom(_map);
        }
        

        private void SetButtons()
        {
            foreach (MapObject mapObject in _mapObjects)
            {
                mapObject.SetInteractable(() => ChangeRoom(mapObject.GetRooms()));
            }
        }

        [Command]
        private void ReturnToHouse()
        {
            foreach (GameObject defaultScreen in _defaultScreens)
            {
                defaultScreen.SetActive(true);
            }

            foreach (MapObject mapObject in _mapObjects)
            {
                mapObject.gameObject.SetActive(false);
            }
        }

        private void CloseHouse()
        {
            foreach (GameObject defaultScreen in _defaultScreens)
            {
                defaultScreen.SetActive(false);
            }
        }

        private void ChangeRoom(List<GameObject> rooms)
        {
            CloseHouse();
            
            if (CurrentRooms != null)
            {
                SetGOs(CurrentRooms, false);
            }

            SetGOs(rooms, true);

            CurrentRooms = rooms;
        }

        private static void SetGOs(List<GameObject> rooms, bool enabled)
        {
            foreach (GameObject room in rooms)
            {
                room.SetActive(enabled);
            }
        }

        [Command]
        private void ChangeRoom(string room)
        {
            foreach (MapObject mapObject in _mapObjects)
            {
                string roomName = mapObject.GetRoomName();
                if (roomName.Equals(room))
                {
                    ChangeRoom(mapObject.GetRooms());
                    return;
                }

                Debug.Log($"There is no room with name {room}");
            }
        }
    }
}