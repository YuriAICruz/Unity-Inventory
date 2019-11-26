using UnityEngine;
using UnityEngine.Serialization;

namespace Graphene.Inventory.Data
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Graphene/FPS/Weapon Data")]
    public class GunData : ScriptableObject
    {
        public int maxBullets = 6;
        public int damage = 1;
        
        [Space]
        public float maxDistance = 100;
        public float spread = 10;
        public float bulletSpeed = 20;
    }
}