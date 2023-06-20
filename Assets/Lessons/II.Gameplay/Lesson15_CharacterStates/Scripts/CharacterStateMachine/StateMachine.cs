using System;
using System.Collections.Generic;
using Declarative;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.CharacterStateMachine
{
    [Serializable]
    public sealed class StateMachine : IState, IStartListener
    {
        [SerializeField]
        private PlayerStateType currentStateType;

        [ShowInInspector, ReadOnly]
        private IState _currentState;
        
        [ShowInInspector, ReadOnly]
        private List<(PlayerStateType, IState)> _states;
        
        public void Construct(params (PlayerStateType, IState)[] states)
        {
            _states = new List<(PlayerStateType, IState)>(states);
        }

        void IStartListener.Start()
        {
            Enter();
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
            foreach (var state in _states)
            {
                if (state.Item1 == stateType)
                {
                    return state.Item2;
                }
            }

            return null;
        }
    }
}