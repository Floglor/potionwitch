using System.Collections.Generic;
using Inventory;
using Misc;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeArea : MonoBehaviour, IDropHandler, IReceiveItems
{
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private DropArea _dropArea;
    [SerializeField] private TextMeshProUGUI _valueText;

    public List<InventoryItem> _inventoryItemsUI = new List<InventoryItem>();

    private void Start()
    {
        _dropArea._dropTarget = this;
    }

    public void AddItem(InventoryItem itemObject)
    {
        itemObject.transform.SetParent(_contentTransform);
        _inventoryItemsUI.Add(itemObject);
        itemObject.isDraggable = false;
        _valueText.text = "Price: " + CalculateValue();
    }


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop Trade area");

        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem == null)
        {
            return;
        }

        InventoryItem item = droppedItem.GetComponent<InventoryItem>();
        
        if (item == null)
        {
            return;
        }

        if (!item.NotOwned)
        {
            return;
        }

        if (item != null && !item.NotOwned)
        {
            AddItem(item);
            item.IsHeld = true;
        }
    }

    public void Buy()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        
        if (!player.TryBuying(CalculateValue()))
            return;
        
        Debug.Log("Buy");
        Inventory.Inventory inventory = GlobalAccess.Instance.Inventory;

        foreach (InventoryItem inventoryItem in _inventoryItemsUI)
        {
            inventory.AddItem(inventoryItem);
            inventoryItem.NotOwned = false;
            inventoryItem.isDraggable = true;
        }

        _valueText.text = "Price: " + CalculateValue();
    }

    private void ClearItems()
    {
        for (int index = 0; index < _inventoryItemsUI.Count; index++)
        {
            InventoryItem inventoryItem = _inventoryItemsUI[index];
            Destroy(inventoryItem);
        }
    }


    private int CalculateValue()
    {
        int price = 0;

        if (_inventoryItemsUI.IsNullOrEmpty())
            return price;

        foreach (InventoryItem inventoryItem in _inventoryItemsUI)
        {
            price += inventoryItem.TargetItem.GetPrice();
        }

        return price;
    }
}