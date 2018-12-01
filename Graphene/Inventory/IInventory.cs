using System;

namespace Graphene.Inventory
{
    public enum AddError
    {
        Exception = 0,
        Duplicated = 1,
        NoMoreRoom = 2,
        Success = 3
    }
    public interface IInventory
    {
        AddError AddItem(IItem item);
        AddError AddWearable(IWearable wearable);
        IWearable GetByType(Type type);
    }
}