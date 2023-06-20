using System;
using Declarative;
using Lessons.Character.Model;
using Lessons.Utils;
using UnityEngine;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class DeadState : IState
    {
        private StateMachine _stateMachine;
        private AtomicVariable<bool> _isAlive;

        private Animator _animator;

        [Construct]
        public void Construct(CharacterCore core, CharacterStates states)
        {
            _stateMachine = states.stateMachine;
            _isAlive = core.life.isAlive;
        }

        [Construct]
        public void Construct(CharacterVisual visual)
        {
            _animator = visual.animator;
        }

        void IState.Enter()
        {
            _animator.SetInteger("State", (int) AnimatorStateType.Dead);
            
            _isAlive.ValueChanged += OnIsAliveChanged;
        }

        void IState.Exit()
        {
            _isAlive.ValueChanged -= OnIsAliveChanged;
        }

        private void OnIsAliveChanged(bool isAlive)
        {
            _stateMachine.SwitchState(PlayerStateType.Idle);
        }
    }
}