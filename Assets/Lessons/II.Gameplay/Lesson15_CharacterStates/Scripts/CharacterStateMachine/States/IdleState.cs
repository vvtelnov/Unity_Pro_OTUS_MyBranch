using System;
using Declarative;
using Lessons.Character.Model;
using Lessons.Utils;
using UnityEngine;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class IdleState : IState
    {
        private StateMachine _stateMachine;
        private AtomicVariable<bool> _isAlive;
        private MovementDirectionVariable _movementDirection;
        
        private Animator _animator;

        [Construct]
        public void Construct(CharacterCore core, CharacterStates states)
        {
            _stateMachine = states.stateMachine;
            _isAlive = core.life.isAlive;
            _movementDirection = core.movement.movementDirection;
        }
        
        [Construct]
        public void Construct(CharacterVisual visual)
        {
            _animator = visual.animator;
        }
        
        void IState.Enter()
        {
            _animator.SetInteger("State", (int) AnimatorStateType.Idle);
            
            _isAlive.ValueChanged += OnIsAliveChanged;
            _movementDirection.MovementStarted += OnMovementStarted;
        }

        void IState.Exit()
        {
            _isAlive.ValueChanged -= OnIsAliveChanged;
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
            _stateMachine.SwitchState(PlayerStateType.Moving);
        }
    }
}