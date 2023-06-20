using System;
using Declarative;
using Lessons.CharacterStateMachine;
using Lessons.CharacterStateMachine.States;

namespace Lessons.Character.Model
{
    [Serializable]
    public sealed class CharacterStates
    {
        public StateMachine stateMachine;

        [Section]
        public IdleState idleState;

        [Section]
        public MovingState movingState;

        [Section]
        public DeadState deadState;
        
        [Construct]
        public void Construct()
        {
            stateMachine.Construct((PlayerStateType.Idle, idleState), (PlayerStateType.Moving, movingState),
                (PlayerStateType.Dead, deadState));
        }
    }
}