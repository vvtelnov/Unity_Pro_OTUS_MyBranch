using System;
using System.Collections.Generic;
using Declarative;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public sealed class IdleState : CompositeState
    {
        public AnimatorState animatorState;

        [Construct]
        public void ConstructSelf()
        {
            states = new List<IState>
            {
                animatorState
            };
        }
    }
}