using System;
using System.Collections.Generic;
using Garden;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

public class StoreInventory : MonoBehaviour, IReceiveItems
{
    [SerializeField] private Transform _contentTransform;
    
    public List<InventoryItem> InventoryItems;
    public TradeArea TradeArea;

    public GardenSeed TestItem;
    private void Start()
    {
        AddItem(FindObjectOfType<ItemSpawner>().SpawnItem(TestItem, transform));
    }

    public void AddItem(InventoryItem item)
    {
        item.transform.SetParent(_contentTransform);
        Button itemButton = item.GetComponent<Button>();
        item.isDraggable = false;

        itemButton.onClick.AddListener(() => MoveItemToTradeArea(item));
        InventoryItems.Add(item);
        item.NotOwned = true;
    }
    
    private void MoveItemToTradeArea(InventoryItem item)
    {
        TradeArea.AddItem(item);
        InventoryItems.Remove(item);
        
        Button itemButton = item.GetComponent<Button>();

        itemButton.onClick.RemoveAllListeners();
    }
}