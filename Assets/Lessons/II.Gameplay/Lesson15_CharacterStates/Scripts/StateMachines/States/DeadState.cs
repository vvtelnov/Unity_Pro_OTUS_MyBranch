using System;
using System.Collections.Generic;
using Declarative;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public sealed class DeadState : CompositeState
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