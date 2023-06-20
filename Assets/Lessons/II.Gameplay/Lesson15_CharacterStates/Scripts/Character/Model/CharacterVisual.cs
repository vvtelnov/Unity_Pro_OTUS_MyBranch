using System;
using Declarative;
using UnityEngine;

namespace Lessons.Character.Model
{
    [Serializable]
    public sealed class CharacterVisual
    {
        [SerializeField]
        private Animator animator;

        private readonly int _state = Animator.StringToHash("State");
        
        [Construct]
        public void Construct(CharacterCore core)
        {
            core.life.isAlive.ValueChanged += isAlive =>
            {
                SetAnimatorState(isAlive ? AnimatorStateType.Idle : AnimatorStateType.Dead);
            };

            core.movement.movementDirection.MovementStarted += () =>
            {
                if (core.life.isAlive)
                {
                    SetAnimatorState(AnimatorStateType.Moving);
                }
            };
            
            core.movement.movementDirection.MovementFinished += () =>
            {
                if (core.life.isAlive)
                {
                    SetAnimatorState(AnimatorStateType.Idle);
                }
            };
        }

        private void SetAnimatorState(AnimatorStateType stateType)
        {
            animator.SetInteger(_state, (int) stateType);
        }
    }
}