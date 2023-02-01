using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        private InventoryItem _inventoryItem;

        public void OnDrop(PointerEventData eventData)
        {
            GameObject droppedItem = eventData.pointerDrag;
            
            droppedItem.transform.parent = transform;
            if (droppedItem == null)
            {
                return;
            }
            
            _inventoryItem = droppedItem.GetComponent<InventoryItem>();
            
            droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            droppedItem.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            droppedItem.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }
        
        public void ClearItem()
        {
            Destroy(_inventoryItem.gameObject);
            _inventoryItem = null;
        }
        
        public IItem GetItem()
        {
            return _inventoryItem.TargetItem;
        }
    }
}