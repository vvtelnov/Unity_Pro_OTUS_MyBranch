using System;
using Lessons.Gameplay.AnimationSystems;
using UnityEngine;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public sealed class AnimatorState : IState
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimatorStateType stateType;

        private readonly int _state = Animator.StringToHash("State");
        
        void IState.Enter()
        {
            animator.SetInteger(_state, (int) stateType);
        }

        void IState.Exit()
        {
            
        }
    }
}