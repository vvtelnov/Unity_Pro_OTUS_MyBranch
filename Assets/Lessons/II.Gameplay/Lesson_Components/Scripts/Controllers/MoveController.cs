using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private MoveComponent _moveComponent;
        private RotateComponent _rotateComponent;
        
        private void Awake()
        {
            _moveComponent = _character.GetComponent<MoveComponent>();
            _rotateComponent = _character.GetComponent<RotateComponent>();
        }

        private void Update()
        {
            this.HandleKeyboard();
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
            _moveComponent.Direction = direction;
            _rotateComponent.Rotate(direction);
        }
    }
}