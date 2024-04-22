using System;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private MoveComponent _moveComponent;
        private RotationComponent _rotationComponent;
        
        private void Awake()
        {
            _moveComponent = _character.GetComponent<MoveComponent>();
            _rotationComponent = _character.GetComponent<RotationComponent>();
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
                this.Move(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.Move(Vector3.back);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.Move(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.Move(Vector3.right);
            }
        }
        
        private void Move(Vector3 direction)
        {
            _moveComponent.SetDirection(direction);
            _rotationComponent.Rotate(direction);
        }
    }
}