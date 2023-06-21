using System;
using Declarative;
using Lessons.Character.Model;
using UnityEngine;

namespace Lessons.CharacterStateMachine.States
{
    [Serializable]
    public sealed class DeadState : CompositeState
    {
        [SerializeField]
        private AnimatorState animatorState;
        
        [Construct]
        public void Construct(CharacterVisual visual)
        {
            animatorState.Construct(visual.animator);
            
            SetStates(animatorState);
        }
    }
}