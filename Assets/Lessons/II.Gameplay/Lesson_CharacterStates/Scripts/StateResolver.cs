using System;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.Gameplay.CharacterStates
{
    public sealed class StateResolver : MonoBehaviour
    {
        [SerializeField]
        private StateMachine stateMachine;

        [Space]
        [SerializeField]
        private UMoveInDirectionMotor moveEngine;

        private void OnEnable()
        {
            this.moveEngine.OnStartMove += this.OnMoveStarted;
            this.moveEngine.OnStopMove += this.OnMoveFinished;
        }
        
        private void OnDisable()
        {
            this.moveEngine.OnStartMove -= this.OnMoveStarted;
            this.moveEngine.OnStopMove -= this.OnMoveFinished;
        }

        private void OnMoveStarted()
        {
            this.stateMachine.SwitchState(StateType.RUN);
        }

        private void OnMoveFinished()
        {
            if (this.stateMachine.CurrentState == StateType.RUN)
            {
                this.stateMachine.SwitchState(StateType.IDLE);
            }
        }
    }
}