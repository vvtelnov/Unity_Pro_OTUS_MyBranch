using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class FireComponent : MonoBehaviour
    {
        public bool FireRequest;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private bool _canFire;
        [SerializeField] private float _reloadTime;
        [SerializeField] private bool _isReloading;

        [ShowInInspector,ReadOnly]
        private float _timer;
        
        private readonly CompositeCondition _condition = new CompositeCondition();
        
        private void Update()
        {
            if (_isReloading)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _timer = 0;
                    _isReloading = false;
                }
            }
            
            if (CanFire())
            {
                FireRequest = false;
                Fire();
            }
        }
        
        public void AppendCondition(Func<bool> condition)
        {
            _condition.Add(condition);
        }

        public bool CanFire()
        {
            return _condition.IsTrue() && _canFire && !_isReloading && FireRequest;
        }
        
        public void Fire()
        {
            var bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            
            if (bullet.TryGetComponent(out MoveComponent moveComponent))
            {
                moveComponent.Direction = _firePoint.forward;
            }

            Debug.Log($"Fire!");
            _isReloading = true;
            _timer = _reloadTime;
        }
    }
}