using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private Vector3 _rotateDirection;
        
        [SerializeField] private int _hitPoints;
        [SerializeField] private bool _isDead;
        
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateRate;
        [SerializeField] private bool _canRotate;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private Bullet _bulletPrefab;

        [SerializeField] private MoveComponent _moveComponent;

        private void Awake()
        {
            _moveComponent.AppendCondition(()=> !_isDead);
            
        }

        private void Update()
        {
            Rotate();
        }

        [Button]
        public void TakeDamage(int damage)
        {
            if (_isDead)
            {
                return;
            }
            
            _hitPoints -= damage;
            Debug.Log($"Take damage = {damage}");
            
            if (_hitPoints <= 0)
            {
                _isDead = true;
            }
        }

        public void Shoot()
        {
            if (_isDead)
            {
                return;
            }

            var bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            
            if (bullet.TryGetComponent(out MoveComponent moveComponent))
            {
                moveComponent.Direction = _firePoint.forward;
            }

            Debug.Log($"Fire!");
        }

        private void Rotate()
        {
            if (!_canRotate && _isDead)
            {
                return;
            }

            _rotateDirection = _moveComponent.Direction;

            if (_rotateDirection == Vector3.zero)
            {
                return;
            }
            
            var targetRotation = Quaternion.LookRotation(_rotateDirection, Vector3.up);
            _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);
        }
    }
}
