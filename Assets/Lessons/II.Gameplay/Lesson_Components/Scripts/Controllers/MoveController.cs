using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Lessons.Lesson_Components.Components;
using Lessons.Lesson_SectionAndVisuals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private AtomicEntity _entity;

        [ShowInInspector]
        private IAtomicVariable<Vector3> _moveDirection;

        private void Awake()
        {
            _moveDirection = _entity.GetVariable<Vector3>(MoveAPI.MOVE_DIRECTION);
        }

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            Move(Vector3.zero);
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Move(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Move(Vector3.back);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(Vector3.right);
            }
        }
        
        private void Move(Vector3 direction)
        {
            if (_moveDirection == null)
            {
                return;
            }
            
            _moveDirection.Value = direction;
        }
    }
}