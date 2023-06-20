using System;
using System.Collections.Generic;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public class CompositeState : IState
    {
        protected List<IState> states;

        void IState.Enter()
        {
            foreach (var state in states)
            {
                state.Enter();
            }
        }

        void IState.Exit()
        {
            foreach (var state in states)
            {
                state.Exit();
            }
        }
    }
}