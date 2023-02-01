using UnityEngine;

namespace Inventory
{
    public interface IItem
    {
        Sprite GetIcon();
        string GetName();
    }
}