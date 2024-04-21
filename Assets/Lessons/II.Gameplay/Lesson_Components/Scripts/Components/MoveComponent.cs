using System;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private bool _canMove;

        private readonly CompositeCondition _condition = new();

        private void Update()
        {
            if (_condition.IsTrue() && _canMove)
            {
                _root.position += _moveDirection * _speed * Time.deltaTime;
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