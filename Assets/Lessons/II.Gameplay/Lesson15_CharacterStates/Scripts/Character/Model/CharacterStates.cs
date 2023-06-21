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
        public void Construct(CharacterCore core)
        {
            stateMachine.Construct((PlayerStateType.Idle, idleState), (PlayerStateType.Moving, movingState),
                (PlayerStateType.Dead, deadState));

            core.life.isAlive.ValueChanged += isAlive =>
            {
                stateMachine.SwitchState(isAlive ? PlayerStateType.Idle : PlayerStateType.Dead);
            };

            core.movement.movementDirection.MovementStarted += () =>
            {
                if (core.life.isAlive)
                {
                    stateMachine.SwitchState(PlayerStateType.Moving);
                }
            };

            core.movement.movementDirection.MovementFinished += () =>
            {
                if (core.life.isAlive)
                {
                    stateMachine.SwitchState(PlayerStateType.Idle);
                }
            };
        }
    }
}