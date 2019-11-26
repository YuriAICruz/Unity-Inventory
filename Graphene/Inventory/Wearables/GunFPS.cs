using System.Security.Cryptography.X509Certificates;
using Graphene.Inventory;
using Graphene.Inventory.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Graphene.Inventory.Wearables
{
    public abstract class GunFps : MonoBehaviour, IWearable, IGun
    {
        private IDamageble _owner;

        public GunData gunData;

        private int _bullets = 0;

        public LayerMask mask;

        public Transform tip;

        public void SetOwner(IDamageble owner)
        {
            _owner = owner;
            Reload();
        }

        public void Shoot(Ray ray)
        {
            if (_bullets <= 0) return;

            _bullets--;
            
            RaycastHit hit;
            var hitten = UnityEngine.Physics.Raycast(ray, out hit, gunData.maxDistance, mask);

            if (hitten)
            {
                var dmg = hit.collider.GetComponent<IDamageble>();
                if (dmg != null)
                {
                    PresentShoot(ray, hit, dmg);
                }
                else
                {
                    PresentShoot(ray, hit);
                }
            }
            else
            {
                PresentShoot(ray);
            }
        }

        protected virtual void PresentShoot(Ray ray)
        {
            Debug.DrawRay(ray.origin, ray.direction * gunData.maxDistance, Color.yellow, 10);
            Debug.DrawLine(tip.position, ray.GetPoint(gunData.maxDistance), Color.yellow, 10);
        }
        protected virtual void PresentShoot(Ray ray, RaycastHit hit)
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 10);
            Debug.DrawLine(tip.position, hit.point, Color.green, 10);
        }
        protected virtual void PresentShoot(Ray ray, RaycastHit hit, IDamageble dmg)
        {
            dmg.DoDamage(gunData.damage, ray.origin);
            
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.magenta, 10);
            Debug.DrawLine(tip.position, hit.point, Color.magenta, 10);
        }

        public void Reload()
        {
            _bullets = gunData.maxBullets;
        }
    }
}