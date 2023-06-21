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
        private Animator _animator;

        [Construct]
        public void Construct(CharacterVisual visual)
        {
            _animator = visual.animator;
        }
        
        void IState.Enter()
        {
            _animator.SetInteger("State", (int) AnimatorStateType.Idle);
        }

        void IState.Exit()
        {
            
        }
    }
}