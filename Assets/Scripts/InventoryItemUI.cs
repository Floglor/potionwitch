using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemText;
    public IItem TargetItem { get; set; }

    public void SetItem(IItem item)
    {
        _itemImage.sprite = item.GetIcon();
        _itemText.text = item.GetName();
        TargetItem = item;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}