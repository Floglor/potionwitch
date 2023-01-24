using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Garden
{
    public class GardenPlot : MonoBehaviour, IPointerDownHandler
    {
        private Sprite _sprite;
        public bool _isClosed; 
        
        private GardenSeed _seed;

        public bool _isPlanted; 

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isClosed)
            {
                return;
            }

            if (!_isPlanted)
            {
                Debug.Log("Gay 2");
                GlobalAccess.Instance.Inventory.gameObject.SetActive(true);
                GlobalAccess.Instance.Inventory.SelectedGardenPlot = this;
            }
        }

        public void Plant(GardenSeed seed)
        {
            _seed = seed;
            _isPlanted = true;
        }
    }
       
}