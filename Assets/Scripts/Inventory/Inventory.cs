using System;
using System.Collections.Generic;
using Alchemy;
using Garden;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class Inventory : MonoBehaviour, IDropHandler, ISnapBack, IReceiveItems
    {
        public List<IItem> items = new List<IItem>();
        [SerializeField] private Cauldron _cauldron;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Transform _contentTransform;
        [SerializeField] private DropArea _dropArea;
        
        public List<InventoryItem> _inventoryItemsUI = new List<InventoryItem>();

        public GardenPlot SelectedGardenPlot;
        

        private void Start()
        {
            _dropArea._dropTarget = this;
        }

        public void ItemOnClick(IItem item)
        {
            if (StateController.Instance.areIngredientsSelectable)
            {
                if (item is Ingredient ingredient && _cauldron is not null) 
                {
                    _cauldron.AddIngredient(ingredient);
                    DestroyItem(item); 
                }
            }

            if (item is GardenSeed gardenSeed && SelectedGardenPlot is not null)
            {
                if (SelectedGardenPlot.IsPlanted == true)
                {
                    return;
                }

                Debug.Log("Succesfully planted Seed on Garden Plot:");
                Debug.Log(SelectedGardenPlot.name);
                gardenSeed.PlantSeed(SelectedGardenPlot);
                DestroyItem(item);
            }
        }

        public void SpawnItem(IItem item)
        {
            items.Add(item);
            
            GameObject itemObject = Instantiate(_itemPrefab, _contentTransform);
            _inventoryItemsUI.Add(itemObject.GetComponent<InventoryItem>());

            InventoryItem inventoryItem = itemObject.GetComponent<InventoryItem>();
            inventoryItem.SetItem(item);
            inventoryItem.isDraggable = true;

            inventoryItem.RemoveItemEvent += RemoveItem;

            Button itemButton = itemObject.GetComponent<Button>();

            itemButton.onClick.AddListener(() => ItemOnClick(item));
        }
        
        public void AddItem(InventoryItem itemObject)
        {
            itemObject.transform.SetParent(_contentTransform);
            
            InventoryItem inventoryItem = itemObject.GetComponent<InventoryItem>();
            _inventoryItemsUI.Add(inventoryItem);
            inventoryItem.isDraggable = true;

            inventoryItem.RemoveItemEvent += RemoveItem;

            Button itemButton = itemObject.GetComponent<Button>();

            itemButton.onClick.AddListener(() => ItemOnClick(inventoryItem.TargetItem));
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
                    if (_inventoryItemsUI[i].TargetItem is null) return;
                    
                    Destroy(_inventoryItemsUI[i].gameObject);
                    _inventoryItemsUI.Remove(_inventoryItemsUI[i]);
                    break;
                }
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            
            GameObject droppedItem = eventData.pointerDrag;

            if (droppedItem == null)
            {
                return;
            }

            InventoryItem item = droppedItem.GetComponent<InventoryItem>();

            if (item != null && !item.NotOwned) 
            {
                AddItem(item);
                item.IsHeld = true;
            }
            
        }

        public void SnapBack(InventoryItem item)
        {
            item.transform.SetParent(transform);
        }

        public void LoseItem(InventoryItem item)
        {
            _inventoryItemsUI.Remove(item);
        }
    }
}