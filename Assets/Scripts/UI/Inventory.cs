using System.Collections.Generic;
using Alchemy;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Inventory : MonoBehaviour
    {
        public List<IItem> items = new List<IItem>();
        [SerializeField] private Cauldron _cauldron;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Transform _contentTransform;

        public List<InventoryItemUI> _inventoryItemsUI = new List<InventoryItemUI>();

        public void AddItem(IItem item)
        {
            items.Add(item);
            GameObject itemObject = Instantiate(_itemPrefab, _contentTransform);
            _inventoryItemsUI.Add(itemObject.GetComponent<InventoryItemUI>());
            itemObject.GetComponent<InventoryItemUI>().SetItem(item);
            Button itemButton = itemObject.GetComponent<Button>();

            itemButton.onClick.AddListener(() => ItemOnClick(item));
        }

        public void ItemOnClick(IItem item)
        {
            if (item is Ingredient ingredient)
            {
                _cauldron.AddIngredient(ingredient);
                RemoveItem(item);
            }
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
                    Destroy(_inventoryItemsUI[i].gameObject);
                    _inventoryItemsUI.Remove(_inventoryItemsUI[i]);
                    break;
                }
            }
            items.Remove(item);
        }
    }
}