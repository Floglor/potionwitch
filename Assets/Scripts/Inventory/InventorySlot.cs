using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler, ISnapBack
    {
        public InventoryItem _inventoryItem;

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");

            if (_inventoryItem != null)
            {
                return;
            }

            GameObject droppedItem = eventData.pointerDrag;

            droppedItem.transform.parent = transform;

            if (droppedItem == null)
            {
                return;
            }

            _inventoryItem = droppedItem.GetComponent<InventoryItem>();
            _inventoryItem.IsHeld = true;

            droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            droppedItem.GetComponent<RectTransform>().anchorMin = GetComponent<RectTransform>().anchorMin;
            droppedItem.GetComponent<RectTransform>().anchorMax = GetComponent<RectTransform>().anchorMax;
            droppedItem.transform.position = transform.position;
        }

        public void ClearItem()
        {
            Destroy(_inventoryItem.gameObject);
            _inventoryItem = null;
        }

        public IItem GetItem()
        {
            return _inventoryItem != null ? _inventoryItem.TargetItem : null;
        }


        public void SnapBack(InventoryItem item)
        {
            _inventoryItem = item;
            GameObject droppedItem = item.gameObject;
            droppedItem.transform.parent = transform;
            _inventoryItem.IsHeld = true;

            droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            droppedItem.GetComponent<RectTransform>().anchorMin = GetComponent<RectTransform>().anchorMin;
            droppedItem.GetComponent<RectTransform>().anchorMax = GetComponent<RectTransform>().anchorMax;
            droppedItem.transform.position = transform.position;
        }

        public void LoseItem(InventoryItem item)
        {
            _inventoryItem = null;
        }
    }
}