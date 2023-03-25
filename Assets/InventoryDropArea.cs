using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropArea : MonoBehaviour, IDropHandler
{
    private Inventory.Inventory _inventory;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory.Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        _inventory.OnDrop(eventData);
    }
}
