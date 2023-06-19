using System;
using Lessons.Character;
using Lessons.Character.Variables;
using Lessons.Engine.Atomic.Values;
using UnityEngine;

namespace Lessons.States
{
    [Serializable]
    public sealed class IdleState : IState
    {
        [SerializeField]
        private Animator animator;
        
        private StateMachine _stateMachine;
        private AtomicVariable<bool> _isAlive;
        private MovementDirectionVariable _movementDirection;

        public void Construct(StateMachine stateMachine, AtomicVariable<bool> isAlive,
            MovementDirectionVariable movementDirection)
        {
            _stateMachine = stateMachine;
            _isAlive = isAlive;
            _movementDirection = movementDirection;
        }

        void IState.Enter()
        {
            animator.SetInteger("State", (int) AnimatorState.Idle);
            
            _isAlive.OnChanged += OnIsAliveChanged;
            _movementDirection.MovementStarted += OnMovementStarted;
        }

        void IState.Exit()
        {
            _isAlive.OnChanged -= OnIsAliveChanged;
            _movementDirection.MovementStarted -= OnMovementStarted;
        }

        private void OnIsAliveChanged(bool isAlive)
        {
            if (!isAlive)
            {
                _stateMachine.SwitchState(PlayerStateType.Dead);
            }
        }
        
        private void OnMovementStarted()
        {
            _stateMachine.SwitchState(PlayerStateType.Move);
        }
    }
}