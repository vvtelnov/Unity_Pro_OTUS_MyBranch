using System;
using Lessons.Character.Model;
using UnityEngine;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class AnimatorState : IState
    {
        [SerializeField]
        private AnimatorStateType stateType;

        private readonly int _state = Animator.StringToHash("State");

        private Animator _animator;

        public void Construct(Animator animator)
        {
            _animator = animator;
        }
        
        void IState.Enter()
        {
            _animator.SetInteger(_state, (int) stateType);
        }

        void IState.Exit()
        {
            
        }
    }
}