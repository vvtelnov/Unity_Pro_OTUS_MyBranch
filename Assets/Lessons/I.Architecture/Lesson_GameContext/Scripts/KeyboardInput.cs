using System;
using UnityEngine;

namespace Lessons.Architecture.GameContexts
{
    public sealed class KeyboardInput : MonoBehaviour,
        IStartGameListener,
        IFinishGameListener
    {
        public event Action<Vector3> OnMove;

        private void Awake()
        {
            this.enabled = false;
        }

        private void Update()
        {
            this.HandleKeyboard();
        }

        void IStartGameListener.OnStartGame()
        {
            this.enabled = true;
        }

        void IFinishGameListener.OnFinishGame()
        {
            this.enabled = false;
        }

        private void HandleKeyboard()
        {
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
            this.OnMove?.Invoke(direction);
        }
    }
}