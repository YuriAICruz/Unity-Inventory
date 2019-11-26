using UnityEngine;

namespace Graphene.Inventory.Wearables
{
    public interface IGun
    {
        void Shoot(Ray ray);
        void Reload();
    }
}