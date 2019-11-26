using UnityEngine;

namespace Graphene.Inventory
{
    public interface IWearable
    {
        void SetOwner(IDamageble owner);
    }
}