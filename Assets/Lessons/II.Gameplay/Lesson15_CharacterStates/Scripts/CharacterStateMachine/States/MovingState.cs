using System;
using Declarative;
using Lessons.Character.Engines;
using Lessons.Character.Model;
using Lessons.Utils;
using UnityEngine;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class MovingState : IState
    {
        public AtomicVariable<float> movementSpeed = 6f;
        public AtomicVariable<float> rotationSpeed = 10f;

        private MovementDirectionVariable _movementDirection;
        private MoveInDirectionEngine _moveInDirectionEngine = new();
        private RotateInDirectionEngine _rotateInDirectionEngine = new();
        

        private Animator _animator;

        [Construct]
        public void Construct(CharacterCore core, CharacterStates states)
        {
            _movementDirection = core.movement.movementDirection;
            _moveInDirectionEngine.Construct(core.movement.transform, movementSpeed);
            _rotateInDirectionEngine.Construct(core.movement.transform, rotationSpeed);
        }
        
        [Construct]
        public void Construct(CharacterVisual visual)
        {
            _animator = visual.animator;
        }

        void IState.Enter()
        {
            _animator.SetInteger("State", (int) AnimatorStateType.Moving);
            
            _movementDirection.ValueChanged += UpdateMovement;
            UpdateMovement(_movementDirection);
        }

        void IState.Exit()
        {
            _movementDirection.ValueChanged -= UpdateMovement;
            UpdateMovement(Vector3.zero);
        }

        private void UpdateMovement(Vector3 direction)
        {
            _moveInDirectionEngine.SetDirection(direction);
            _rotateInDirectionEngine.SetDirection(direction);   
        }
    }
}