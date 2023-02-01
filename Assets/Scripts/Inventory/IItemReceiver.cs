namespace Inventory
{
    public interface IItemReceiver
    {
        bool ReceiveItem(IItem item);
    }
}