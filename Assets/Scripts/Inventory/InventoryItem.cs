﻿using System;
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
            RemoveItemEvent?.Invoke(TargetItem);
            transform.parent = _canvas.transform;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            FollowMouse();
        }
        
        private void FollowMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            transform.position = Input.mousePosition / _canvas.scaleFactor;
        }
    }
}