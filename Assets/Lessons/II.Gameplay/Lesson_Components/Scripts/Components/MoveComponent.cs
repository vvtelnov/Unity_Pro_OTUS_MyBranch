using System;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class MoveComponent
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private bool _canMove;

        private readonly CompositeCondition _condition = new();

        public void Update(float deltaTime)
        {
            if (_condition.IsTrue() && _canMove)
            {
                _root.position += _moveDirection * _speed * deltaTime;
            }
        }
        
        public void SetDirection(Vector3 direction)
        {
            _moveDirection = direction;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}