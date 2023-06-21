using System;
using Declarative;
using Lessons.Character.Engines;
using Lessons.Character.Model;
using Lessons.Utils;
using UnityEngine;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class MoveInDirectionState : IState
    {
        private MovementDirectionVariable _movementDirection;
        private MoveInDirectionEngine _moveInDirectionEngine;

        public void Construct(MovementDirectionVariable movementDirection, MoveInDirectionEngine moveInDirectionEngine)
        {
            _movementDirection = movementDirection;
            _moveInDirectionEngine = moveInDirectionEngine;
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
            _moveInDirectionEngine.SetDirection(direction);
        }
    }
}