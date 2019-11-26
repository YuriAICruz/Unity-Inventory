using UnityEngine;

namespace Graphene.Inventory
{
    public interface IDamageble
    {
        void DoDamage(int damage, Vector3 from);
    }
}