using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Gameplay.CharacterStates
{
    public class StateMachine : State
    {
        public event Action<StateType> OnStateChanged;
    
        public StateType CurrentState
        {
            get { return this.currentStateType; }
        }

        [SerializeField]
        private bool enterOnEnable;

        [SerializeField]
        private bool exitOnDisable;

        [Space]
        [LabelText("Current State")]
        [SerializeField]
        private StateType currentStateType;

        [Space]
        [SerializeField]
        private List<StateHolder> states;

        private State currentState;

        private void OnEnable()
        {
            if (this.enterOnEnable)
            {
                this.Enter();
            }
        }

        private void OnDisable()
        {
            if (this.exitOnDisable)
            {
                this.Exit();
            }
        }

        public override void Enter()
        {
            if (this.currentState == null)
            {
                this.currentState = this.FindState(this.currentStateType);
                this.currentState.Enter();
            }
        }

        public override void Exit()
        {
            if (this.currentState != null)
            {
                this.currentState.Exit();
                this.currentState = null;
            }
        }

        public void SwitchState(StateType stateType)
        {
            this.Exit();
            this.currentStateType = stateType;
            this.Enter();
            this.OnStateChanged?.Invoke(stateType);
        }

        private State FindState(StateType type)
        {
            for (int i = 0, count = this.states.Count; i < count; i++)
            {
                StateHolder holder = this.states[i];
                if (holder.type == type)
                {
                    return holder.state;
                }
            }

            throw new Exception($"State {type} is not found!");
        }

        [Serializable]
        private struct StateHolder
        {
            [SerializeField]
            public StateType type;

            [SerializeField]
            public State state;
        }
    }
}