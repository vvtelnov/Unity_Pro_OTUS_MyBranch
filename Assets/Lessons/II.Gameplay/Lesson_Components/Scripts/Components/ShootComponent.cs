using System;
using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_Components.Scripts;
using Lessons.Lesson_SectionAndVisuals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class ShootComponent
    {
        public Transform FirePoint;
        public AtomicEvent ShootRequest;
        public AtomicEvent ShootAction;
        public AtomicEvent ShootEvent;
        
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private bool _isReloading;
        [SerializeField] private bool _canShoot;
        [SerializeField] private Bullet _bulletPrefab;
        
        [ShowInInspector, ReadOnly]
        private float _reloadTimer;
        
        private readonly CompositeCondition _condition = new();

        public void OnEnable()
        {
            ShootAction.Subscribe(Shoot);
        }

        public void OnDisable()
        {
            ShootAction.Unsubscribe(Shoot);
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
            return _canShoot && !_isReloading;
        }
        
        private void Shoot()
        {
            if (!CanFire())
            {
                return;
            }

            var bullet = GameObject.Instantiate(_bulletPrefab, FirePoint.position, FirePoint.rotation);
            bullet.MoveComponent.SetDirection(FirePoint.forward);
            
            _reloadTimer = _reloadTime;
            _isReloading = true;
            
            ShootEvent.Invoke();
            Debug.Log("Fire!");
        }
        
        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}