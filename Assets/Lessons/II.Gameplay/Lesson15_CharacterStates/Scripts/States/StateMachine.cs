using System;
using System.Collections.Generic;
using Declarative;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.States
{
    [Serializable]
    public sealed class StateMachine : IState, IStartListener
    {
        [SerializeField]
        private bool enterOnStart = true;
        
        [SerializeField]
        private PlayerStateType currentStateType;
        
        [ShowInInspector, ReadOnly]
        private IState _currentState;

        [ShowInInspector, ReadOnly]
        private List<StateInfo> _states;

        public void Construct(params StateInfo[] states)
        {
            _states = new List<StateInfo>(states);
        }
        
        void IStartListener.Start()
        {
            if (enterOnStart)
            {
                Enter();
            }
        }

        public void Enter()
        {
            _currentState = FindState(currentStateType);
            _currentState?.Enter();
        }

        public void Exit()
        {
            _currentState?.Exit();
            _currentState = null;
        }

        public void SwitchState(PlayerStateType stateType)
        {
            Exit();

            currentStateType = stateType;

            Enter();
        }

        private IState FindState(PlayerStateType stateType)
        {
            foreach (var stateInfo in _states)
            {
                if (stateInfo.stateType == stateType)
                {
                    return stateInfo.state;
                }
            }

            return null;
        }
    }
}