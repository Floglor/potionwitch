using UnityEngine;

namespace UI
{
    public interface IItem
    {
        Sprite GetIcon();
        string GetName();
    }
}