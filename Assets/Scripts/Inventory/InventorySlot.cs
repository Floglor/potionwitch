using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler, ISnapBack
    {
        private InventoryItem _inventoryItem;

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            GameObject droppedItem = eventData.pointerDrag;
            
            droppedItem.transform.parent = transform;
            
            if (droppedItem == null)
            {
                return;
            }
            
            _inventoryItem = droppedItem.GetComponent<InventoryItem>();
            _inventoryItem.IsHeld = true;
            
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
        

        public void SnapBack(InventoryItem item)
        {
            _inventoryItem = item;
            GameObject droppedItem = item.gameObject;
            droppedItem.transform.parent = transform;
            _inventoryItem.IsHeld = true;
            
            droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            droppedItem.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            droppedItem.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }
    }
}