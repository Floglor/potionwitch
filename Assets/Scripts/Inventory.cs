using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   List<IItem> items = new List<IItem>();
   [SerializeField] private GameObject _itemPrefab;
   [SerializeField] private Transform _contentTransform;
   
   List<InventoryItemUI> _inventoryItemsUI = new List<InventoryItemUI>();
   
   public void AddItem(IItem item)
   {
       items.Add(item);
       GameObject itemObject = Instantiate(_itemPrefab, _contentTransform);
       _inventoryItemsUI.Add(itemObject.GetComponent<InventoryItemUI>());
       itemObject.GetComponent<InventoryItemUI>().SetItem(item);
   }

   public void HideItem(IItem item)
   {
       for (int i = 0; i < _inventoryItemsUI.Count; i++)
       {
           if (item.Equals(_inventoryItemsUI[i].TargetItem))
           {
               _inventoryItemsUI[i].Hide();
               break;
           }
       }
   }
   
   public void ActivateItem(IItem item)
   {
       for (int i = 0; i < _inventoryItemsUI.Count; i++)
       {
           if (item.Equals(_inventoryItemsUI[i].TargetItem))
           {
               _inventoryItemsUI[i].Activate();
               break;
           }
       }
   }
   
   public void RemoveItem(IItem item)
   {
       items.Remove(item);
       for (int i = 0; i < _inventoryItemsUI.Count; i++)
       {
           if (item.Equals(_inventoryItemsUI[i].TargetItem))
           {
               _inventoryItemsUI.Remove(_inventoryItemsUI[i]);
               break;
           }
       }
   }
}