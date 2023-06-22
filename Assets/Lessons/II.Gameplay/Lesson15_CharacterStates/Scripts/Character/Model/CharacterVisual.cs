using System;
using Declarative;
using UnityEngine;

namespace Lessons.Character.Model
{
    [Serializable]
    public sealed class CharacterVisual
    {
        public Animator animator;

        [Construct]
        public void Construct(CharacterCore core)
        {
            var state = Animator.StringToHash("State");
            
            core.movement.movementDirection.MovementStarted +=
                () => animator.SetInteger(state, (int) AnimatorStateType.Run);

            core.movement.movementDirection.MovementFinished +=
                () => animator.SetInteger(state, (int) AnimatorStateType.Idle);
        }
    }
}