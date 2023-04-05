using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public IDropHandler _dropTarget;


    public void OnDrop(PointerEventData eventData)
    {
        _dropTarget?.OnDrop(eventData);
    }
}