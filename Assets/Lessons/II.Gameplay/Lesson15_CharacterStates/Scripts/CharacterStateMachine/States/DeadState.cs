using System;
using Declarative;
using Lessons.Character.Model;
using Lessons.Utils;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class DeadState : IState
    {
        private StateMachine _stateMachine;
        private AtomicVariable<bool> _isAlive;

        [Construct]
        public void Construct(CharacterCore core, CharacterStates states)
        {
            _stateMachine = states.stateMachine;
            _isAlive = core.life.isAlive;
        }

        void IState.Enter()
        {
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