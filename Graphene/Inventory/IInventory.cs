using System;

namespace Graphene.Inventory
{
    public enum InventoryResponse
    {
        Exception = 0,
        Duplicated = 1,
        NoMoreRoom = 2,
        Success = 3
    }
    public interface IInventory
    {
        InventoryResponse AddItem(IItem item);
        InventoryResponse AddWearable(IWearable wearable);
        IWearable GetByType(Type type);
    }
}