using UnityEngine;

namespace Graphene.Inventory.Wearables
{
    public interface IWeaponTopDown
    {
        void Use(Vector3 dir);

        void SetTip(Vector3 tip);
    }
}