using Sirenix.OdinInspector;

namespace Lessons.CharacterStateMachine.States
{
    public abstract class CompositeState : IState
    {
        [ShowInInspector, ReadOnly]
        private IState[] _states;
        
        void IState.Enter()
        {
            foreach (var state in _states)
            {
                state.Enter();
            }
        }

        void IState.Exit()
        {
            foreach (var state in _states)
            {
                state.Exit();
            }
        }

        protected void SetStates(params IState[] states)
        {
            _states = states;
        }
    }
}