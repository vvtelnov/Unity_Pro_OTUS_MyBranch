using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private bool _canMove;
        
        [SerializeField] private int _hitPoints;
        [SerializeField] private bool _isDead;
        
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateRate;
        [SerializeField] private bool _canRotate;

        [SerializeField] private Transform _firePoint;

        public void Move(Vector3 direction)
        {
            _moveDirection = direction;
            
            Move();
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
            
            Debug.Log($"Fire! = {_firePoint}");
        }

        private void Move()
        {
            if (!_canMove && _isDead)
            {
                return;
            }
            
            _root.position += _moveDirection * _speed * Time.deltaTime;
        }

        private void Rotate()
        {
            if (!_canRotate && _isDead)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
            _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);
        }
    }
}
