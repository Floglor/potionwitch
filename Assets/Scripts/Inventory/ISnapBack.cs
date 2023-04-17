namespace Inventory
{
    public interface ISnapBack
    {
        public void SnapBack(InventoryItem item);
        public void LoseItem(InventoryItem item);
    }
}