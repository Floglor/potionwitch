using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InventorySlot))]
[RequireComponent(typeof(Image))]
public class ItemHolder : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public IItem HeldItem;
    public InventorySlot InventorySlot;

    [SerializeField] private Canvas canvas;

    private Camera _mainCamera;
    private bool _isHoldingItem;

    private void Start()
    {
        InventorySlot = GetComponent<InventorySlot>();
        _mainCamera = Camera.main;
    }
    private void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        transform.position = Input.mousePosition / canvas.scaleFactor;
    }

    private void Update()
    {
        if (_isHoldingItem)
            FollowMouse();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}