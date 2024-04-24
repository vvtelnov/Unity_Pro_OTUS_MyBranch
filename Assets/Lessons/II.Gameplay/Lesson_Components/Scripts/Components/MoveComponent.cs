using System;
using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class MoveComponent
    {
        public AtomicVariable<Vector3> MoveDirection;

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        public AtomicVariable<bool> CanMove;

        private readonly CompositeCondition _condition = new();

        public void Update(float deltaTime)
        {
            if (_condition.IsTrue() && CanMove.Value)
            {
                _root.position += MoveDirection.Value * _speed * deltaTime;
            }
        }
        
        public void SetDirection(Vector3 direction)
        {
            MoveDirection.Value = direction;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}