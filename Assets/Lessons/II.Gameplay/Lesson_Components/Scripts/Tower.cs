using System;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private Transform _targetPoint;
        [SerializeField] private float _radius;

        private void Awake()
        {
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
            _shootComponent.AppendCondition(_lifeComponent.IsAlive);
        }

        private void Update()
        {
            var direction = _targetPoint.position - transform.position;
            _rotationComponent.Rotate(direction);
            
            if (direction.magnitude < _radius)
            {
                _shootComponent.Shoot();
            }
        }
    }
}