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

        private MoveInDirectionEngine _moveInDirectionEngine = new();
        private RotateInDirectionEngine _rotateInDirectionEngine = new();
        
        private StateMachine _stateMachine;
        private AtomicVariable<bool> _isAlive;
        private MovementDirectionVariable _movementDirection;

        [Construct]
        public void Construct(CharacterCore core, CharacterStates states)
        {
            _moveInDirectionEngine.Construct(core.movement.transform, movementSpeed);
            _rotateInDirectionEngine.Construct(core.movement.transform, rotationSpeed);
            
            _stateMachine = states.stateMachine;
            _isAlive = core.life.isAlive;
            _movementDirection = core.movement.movementDirection;
        }

        void IState.Enter()
        {
            _isAlive.ValueChanged += OnIsAliveValueChanged;
            _movementDirection.MovementFinished += OnMovementFinished;

            _movementDirection.ValueChanged += UpdateMovement;
        }

        void IState.Exit()
        {
            _isAlive.ValueChanged -= OnIsAliveValueChanged;
            _movementDirection.MovementFinished -= OnMovementFinished;
            
            _movementDirection.ValueChanged -= UpdateMovement;
            UpdateMovement(Vector3.zero);
        }
        
        private void OnIsAliveValueChanged(bool isAlive)
        {
            _stateMachine.SwitchState(PlayerStateType.Dead);
        }
        
        private void OnMovementFinished()
        {
            _stateMachine.SwitchState(PlayerStateType.Idle);
        }
        
        private void UpdateMovement(Vector3 direction)
        {
            _moveInDirectionEngine.SetDirection(direction);
            _rotateInDirectionEngine.SetDirection(direction);   
        }
    }
}