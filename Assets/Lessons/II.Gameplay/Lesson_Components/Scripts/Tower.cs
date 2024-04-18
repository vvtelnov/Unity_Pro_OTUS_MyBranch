using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private RotateComponent _rotateComponent;
        [SerializeField] private FireComponent _fireComponent;

        private void Awake()
        {
            _fireComponent.AppendCondition(()=> !_lifeComponent.IsDead);
            _rotateComponent.AppendCondition(()=> !_lifeComponent.IsDead);
        }

        private void Update()
        {
            var forwardDirection = _target.position - transform.position;
            _rotateComponent.Rotate(forwardDirection);

            _fireComponent.FireRequest = true;
        }
    }
}