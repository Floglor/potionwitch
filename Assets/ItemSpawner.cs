using Inventory;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject _itemPrefab;
    
    public InventoryItem SpawnItem(IItem item, Transform contentTransform)
    {
            
        GameObject itemObject = Instantiate(_itemPrefab, contentTransform);

        InventoryItem inventoryItem = itemObject.GetComponent<InventoryItem>();
        inventoryItem.SetItem(item);

        Button itemButton = itemObject.GetComponent<Button>();

        return inventoryItem;
    }
}