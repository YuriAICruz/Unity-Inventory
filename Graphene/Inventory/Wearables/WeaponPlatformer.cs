﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphene.Inventory.Wearables
{
    public abstract class WeaponPlatformer : MonoBehaviour, IWearable, IWeaponPlatformer
    {
        public int Damage;
        private bool _enabled;

        private GameObject _hitParticle;

        public float Height;
        public float Radius;
        public Vector3 Offset;
        private IDamageble _owner;
        private List<IDamageble> _lastHits;
        private Coroutine _routine, _using;

        private void Awake()
        {
            _hitParticle = Resources.Load<GameObject>("Particle/Hit0");
        }
        
        public void SetOwner(IDamageble owner)
        {
            _owner = owner;
        }
        
        public void Use(float delay = 0, float duration = 0.4f)
        {
            if (_using != null)
            {
                StopCoroutine(_using);
                _using = null;
            }

            _using = StartCoroutine(Using(delay, duration));
        }
        
        IEnumerator Using(float delay, float duration)
        {
            // _enabled = false;
            _lastHits = new List<IDamageble>();
            
            yield return new WaitForSeconds(delay);

            Hit();

            yield return new WaitForSeconds(duration);

            _enabled = false;
        }

        private void Hit()
        {
            //TODO: distance based hit
            UnityEngine.Physics.SphereCastAll(transform.TransformPoint(Offset), Height, transform.forward, Radius);
        }

        public void SetEnabled(float delay = 0, float duration = 0.4f)
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }

            _routine = StartCoroutine(Enable(delay, duration));
        }

        IEnumerator Enable(float delay, float duration)
        {
            // _enabled = false;
            _lastHits = new List<IDamageble>();
            
            yield return new WaitForSeconds(delay);

            _enabled = true;

            yield return new WaitForSeconds(duration);

            _enabled = false;
        }

        private void Update()
        {
            if (!_enabled) return;

            var rays = new Ray[]
            {
                new Ray(transform.TransformPoint(Offset), transform.forward),
                new Ray(transform.TransformPoint(Offset) + transform.forward * Height, -transform.forward),
            };

            foreach (var ray in rays)
            {
                Debug.DrawRay(ray.origin, ray.direction * Height, Color.red);
                CheckCollision(ray);
            }
        }

        private void CheckCollision(Ray ray)
        {
            var hits = UnityEngine.Physics.RaycastAll(ray, Height);

            foreach (var hit in hits)
            {
                var dmg = hit.transform.GetComponent<IDamageble>();

                if (dmg == null || (_owner != null && _owner == dmg) || _lastHits.Contains(dmg)) continue;

                //_lastHits.Add(dmg);
                Instantiate(_hitParticle, hit.point, Quaternion.identity);
                dmg.DoDamage(Damage, transform.position);
            }
        }
    }
}