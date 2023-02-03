using System.Collections.Generic;
using Alchemy;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class Inventory : MonoBehaviour, IDropHandler
    {
        public List<IItem> items = new List<IItem>();
        [SerializeField] private Cauldron _cauldron;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Transform _contentTransform;

        public List<InventoryItem> _inventoryItemsUI = new List<InventoryItem>();

        [HideInInspector] public InventorySlot _requestedSlot;

        public void RequestItemInSlot(InventorySlot slot)
        {
            _requestedSlot = slot;
        }


        public void AddItem(IItem item)
        {
            items.Add(item);
            GameObject itemObject = Instantiate(_itemPrefab, _contentTransform);
            _inventoryItemsUI.Add(itemObject.GetComponent<InventoryItem>());

            InventoryItem inventoryItem = itemObject.GetComponent<InventoryItem>();
            inventoryItem.SetItem(item);

            inventoryItem.RemoveItemEvent += RemoveItem;

            Button itemButton = itemObject.GetComponent<Button>();

            itemButton.onClick.AddListener(() => ItemOnClick(item));
        }

        public void ItemOnClick(IItem item)
        {
            if (StateController.Instance.areIngredientsSelectable)
            {
                if (item is Ingredient ingredient)
                {
                    _cauldron.AddIngredient(ingredient);
                    DestroyItem(item);
                }
            }

            if (item is GardenSeed gardenSeed)
            {
                Debug.Log("Succesfully planted Seed on Garden Plot:");
                Debug.Log(SelectedGardenPlot.name);
                gardenSeed.SeedPlanting(SelectedGardenPlot);
                RemoveItem(item);
            }
        }

        public void SelectGardenPlot(GardenPlot gardenPlot)
        {
            if (SelectedGardenPlot != null)
            {
                SelectedGardenPlot.Deselect();
            }
            gardenPlot.Select();
            SelectedGardenPlot = gardenPlot;
        }
        
        public void DeselectGardenPlot(GardenPlot gardenPlot)
        {
            if (SelectedGardenPlot != null)
            {
                SelectedGardenPlot.Deselect();
            }
            SelectedGardenPlot = null;
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
            for (int i = 0; i < _inventoryItemsUI.Count; i++)
            {
                if (item.Equals(_inventoryItemsUI[i].TargetItem))
                {
                    _inventoryItemsUI.Remove(_inventoryItemsUI[i]);
                    break;
                }
            }

            items.Remove(item);
        }

        public void DestroyItem(IItem item)
        {
            for (int i = 0; i < _inventoryItemsUI.Count; i++)
            {
                if (item.Equals(_inventoryItemsUI[i].TargetItem))
                {
                    Destroy(_inventoryItemsUI[i].gameObject);
                    _inventoryItemsUI.Remove(_inventoryItemsUI[i]);
                    break;
                }
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            
            GameObject droppedItem = eventData.pointerDrag;

            if (droppedItem == null)
            {
                return;
            }
            
            if (droppedItem.GetComponent<InventoryItem>().TargetItem is IItem item)
            {
                AddItem(item);
            }
            
            Destroy(droppedItem);
        }
    }
}