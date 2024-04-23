using System;
using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class MoveComponent
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private bool _canMove;

        private IAtomicVariable<Vector3> _moveDirection;
        private readonly CompositeCondition _condition = new();

        public void Construct(IAtomicVariable<Vector3> moveDirection)
        {
            _moveDirection = moveDirection;
        }
        
        public void Update(float deltaTime)
        {
            if (_condition.IsTrue() && _canMove)
            {
                _root.position += _moveDirection.Value * _speed * deltaTime;
            }
        }
        
        public void SetDirection(Vector3 direction)
        {
            _moveDirection.Value = direction;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}