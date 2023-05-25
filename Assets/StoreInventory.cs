using System;
using System.Collections.Generic;
using System.Linq;
using Garden;
using Inventory;
using Misc;
using UnityEngine;
using UnityEngine.UI;

public class StoreInventory : MonoBehaviour, IReceiveItems, IObserver
{
    [SerializeField] private Transform _contentTransform;

    public List<InventoryItem> InventoryItems;
    public TradeArea TradeArea;

    public GardenSeed TestItem;
    public ShopItemRandomizer ShopSeedRandomizer;
    public int MaxSlots;
    public int MaxSimilarItems;


    private void Start()
    {
        AddItem(FindObjectOfType<ItemSpawner>().SpawnItem(TestItem, transform));
        TimeController timeController = FindObjectOfType<TimeController>();
        timeController.Attach(this);
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

    public void UpdateObserver(ISubject subject)
    {
        if (InventoryItems.Count >= MaxSlots) return;

        TimeController timeController = subject as TimeController;

        if (timeController != null)
        {
            List<IItem> itemsToAdd = ShopSeedRandomizer.UpdateShop(timeController.date);
            AddItems(itemsToAdd);
        }
    }

    private void AddItems(List<IItem> itemsToAdd)
    {
        foreach (IItem item in itemsToAdd)
        {
            //Change logic to stacks
            int countInInventory = ReturnCount(item, InventoryItems);

            int difference = MaxSimilarItems - countInInventory;

            if (difference == 0)
                return;

            AddItem(FindObjectOfType<ItemSpawner>().SpawnItem(item, this.transform));
            
            if (InventoryItems.Count >= MaxSlots)
            {
                Debug.Assert(InventoryItems.Count > MaxSlots, "Exceeded max items limit in Shop");

                return;
            }
        }
    }

    private static int ReturnCount(IItem item, IEnumerable<IItem> listToCount)
    {
        return listToCount.Count(inventoryItem => inventoryItem.GetName().Equals(item.GetName()));
    }

    private static int ReturnCount(IItem item, IEnumerable<InventoryItem> listToCount)
    {
        return listToCount.Count(inventoryItem => inventoryItem.GetItem().GetName().Equals(item.GetName()));
    }
}