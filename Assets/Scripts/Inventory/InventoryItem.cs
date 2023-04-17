using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] protected Image _itemImage;
        [SerializeField] protected TextMeshProUGUI _itemText;
        public IItem TargetItem { get; set; }

        public event System.Action<IItem> RemoveItemEvent;

        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private Transform _originalParentTransform;

        public bool NotOwned;

        [HideInInspector]
        public bool IsHeld;

        public bool isDraggable;

        private void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetItem(IItem item)
        {
            _itemImage.sprite = item.GetIcon();
            _itemText.text = item.GetName();
            TargetItem = item;
        }

        public IItem GetItem()
        {
            return TargetItem;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //RemoveItemEvent?.Invoke(TargetItem);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isDraggable) return;
            
            IsHeld = false;
            _originalParentTransform = transform.parent;
            Debug.Log("OnBeginDrag");
            _canvasGroup.blocksRaycasts = false;
            transform.SetParent(_canvas.transform);
            ISnapBack snapTarget = _originalParentTransform.gameObject.GetComponent<ISnapBack>();
            snapTarget?.LoseItem(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isDraggable) return;
            
            Debug.Log("OnEndDrag");
            _canvasGroup.blocksRaycasts = true;
            ISnapBack snapTarget = _originalParentTransform.gameObject.GetComponent<ISnapBack>();

            if (IsHeld) return;
            
            snapTarget?.SnapBack(this);
            IsHeld = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDraggable)
                return;
            
            FollowMouse();
        }

        private void FollowMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            transform.position = Input.mousePosition * _canvas.scaleFactor;
        }
    }
}