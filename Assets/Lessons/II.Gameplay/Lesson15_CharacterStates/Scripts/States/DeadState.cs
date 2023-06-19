using System;
using Lessons.Character;
using Lessons.Character.Variables;
using Lessons.Engine.Atomic.Values;
using UnityEngine;

namespace Lessons.States
{
    [Serializable]
    public sealed class DeadState : IState
    {
        [SerializeField]
        private Animator animator;

        private StateMachine _stateMachine;
        private AtomicVariable<bool> _isAlive;

        public void Construct(StateMachine stateMachine, AtomicVariable<bool> isAlive)
        {
            _stateMachine = stateMachine;
            _isAlive = isAlive;
        }
        
        void IState.Enter()
        {
            animator.SetInteger("State", (int) AnimatorState.Dead);

            _isAlive.OnChanged += OnIsAliveChanged;
        }

        void IState.Exit()
        {
            _isAlive.OnChanged -= OnIsAliveChanged;
        }

        private void OnIsAliveChanged(bool isAlive)
        {
            if (isAlive)
            {
                _stateMachine.SwitchState(PlayerStateType.Idle);
            }
        }
    }
}