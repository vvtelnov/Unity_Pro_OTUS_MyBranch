using System;
using Atomic.Elements;
using Lessons.Lesson_Components.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class ShootComponent
    {
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private bool _isReloading;
        [SerializeField] private bool _canFire;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        
        [ShowInInspector, ReadOnly]
        private float _reloadTimer;
        
        private readonly CompositeCondition _condition = new();

        public AtomicAction ShootAction;

        public void Compose()
        {
            ShootAction.Compose(Shoot);
        }
        
        public void Update(float deltaTime)
        {
            if (_isReloading)
            {
                _reloadTimer -= deltaTime;
                
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

            //Зависимость через фактори
            var bullet = Object.Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.MoveComponent.SetDirection(_firePoint.forward);
            
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