using Lessons.StateMachines.States;

namespace Lessons.StateMachines
{
    public sealed class StateInfo
    {
        public StateInfo(PlayerStateType stateType, IState state)
        {
            this.stateType = stateType;
            this.state = state;
        }
        
        public readonly PlayerStateType stateType;
        public readonly IState state;
    }
}