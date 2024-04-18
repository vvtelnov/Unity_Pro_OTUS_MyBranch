using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveComponent : MonoBehaviour
    {
        public Vector3 Direction;
        
        [SerializeField] private float _speed;
        [SerializeField] private bool _canMove;

        private readonly ComponentCondition _condition = new();

        public void Update()
        {
            if (!CanMove())
            {
                return;
            }
            
            transform.position += Direction * _speed * Time.deltaTime;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.Add(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.Remove(condition);
        }

        private bool CanMove()
        {
            return _condition.IsTrue() && _canMove;
        }
    }
}