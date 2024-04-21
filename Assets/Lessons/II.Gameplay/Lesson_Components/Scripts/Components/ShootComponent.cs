using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    public class ShootComponent : MonoBehaviour
    {
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private bool _isReloading;
        [SerializeField] private bool _canFire;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        
        [ShowInInspector, ReadOnly]
        private float _reloadTimer;
        
        private readonly CompositeCondition _condition = new();

        private void Update()
        {
            if (_isReloading)
            {
                _reloadTimer -= Time.deltaTime;
                
                if (_reloadTimer <= 0)
                {
                    _isReloading = false;
                }
            }
        }

        public bool CanFire()
        {
            return _canFire;
        }
        
        public void Shoot()
        {
            if (!_canFire)
            {
                return;
            }

            if (_isReloading)
            {
                return;
            }

            var bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

            if (bullet.TryGetComponent(out MoveComponent component))
            {
                component.SetDirection(_firePoint.forward);
            }
            
            _reloadTimer = _reloadTime;
            _isReloading = true;
            
            Debug.Log("Fire!");
        }
        
        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}