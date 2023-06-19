using System;
using Declarative;
using Lessons.Engine.Atomic.Values;
using UnityEngine;

namespace Lessons.Character.Engines
{
    [Serializable]
    public sealed class MovementEngine : IUpdateListener
    {
        private Transform _targetTransform;
        private AtomicVariable<float> _speed;

        private Vector3 _direction;

        public void Construct(Transform targetTransform, AtomicVariable<float> speed)
        {
            _targetTransform = targetTransform;
            _speed = speed;
        }
        
        void IUpdateListener.Update(float deltaTime)
        {
            _targetTransform.position += _direction * (_speed * deltaTime);
        }
        
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
    }
}