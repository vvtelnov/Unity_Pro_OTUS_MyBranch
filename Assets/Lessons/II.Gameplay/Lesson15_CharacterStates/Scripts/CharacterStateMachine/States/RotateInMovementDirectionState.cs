using System;
using Declarative;
using Lessons.Character.Engines;
using Lessons.Character.Model;
using Lessons.Utils;
using UnityEngine;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class RotateInMovementDirectionState : IState
    {
        private MovementDirectionVariable _movementDirection;
        private RotateInDirectionEngine _rotateInDirectionEngine;

        public void Construct(MovementDirectionVariable movementDirection, RotateInDirectionEngine rotateInDirectionEngine)
        {
            _movementDirection = movementDirection;
            _rotateInDirectionEngine = rotateInDirectionEngine;
        }
        
        void IState.Enter()
        {
            _movementDirection.ValueChanged += UpdateDirection;
            UpdateDirection(_movementDirection);
        }

        void IState.Exit()
        {
            _movementDirection.ValueChanged -= UpdateDirection;
            UpdateDirection(Vector3.zero);
        }

        private void UpdateDirection(Vector3 direction)
        {
            _rotateInDirectionEngine.SetDirection(direction);
        }
    }
}