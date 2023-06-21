using System;
using Declarative;
using Lessons.Character.Model;
using UnityEngine;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class MovingState : CompositeState
    {
        [SerializeField]
        private AnimatorState animatorState;

        [SerializeField]
        private MoveInDirectionState moveInDirectionState;

        [SerializeField]
        private RotateInMovementDirectionState rotateInMovementDirectionState;
        
        [Construct]
        public void Construct(CharacterCore core, CharacterVisual visual)
        {
            animatorState.Construct(visual.animator);
            moveInDirectionState.Construct(core.movement.movementDirection, core.movement.moveInDirectionEngine);
            rotateInMovementDirectionState.Construct(core.movement.movementDirection, core.movement.rotateInDirectionEngine);
            
            SetStates(animatorState, moveInDirectionState, rotateInMovementDirectionState);
        }
    }
}