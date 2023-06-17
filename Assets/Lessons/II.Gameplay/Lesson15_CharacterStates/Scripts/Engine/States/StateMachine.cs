using System;
using System.Collections.Generic;
using System.Linq;
using Declarative;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Gameplay.States
{
    [Serializable]
    public class StateMachine<TKey> : IState, IStartListener where TKey : Enum
    {
        [SerializeField]
        private TKey currentKey;

        [SerializeField]
        private bool enterOnStart;

        [ShowInInspector, ReadOnly]
        private List<(TKey, IState)> states = new();

        [ShowInInspector, ReadOnly]
        private IState currentState;

        void IStartListener.Start()
        {
            if (this.enterOnStart)
            {
                this.Enter();
            }
        }

        public void Enter()
        {
            this.currentState = this.states.First(it => it.Item1.Equals(this.currentKey)).Item2;
            this.currentState.Enter();
        }

        public void Exit()
        {
            if (this.currentState != null)
            {
                this.currentState.Exit();
                this.currentState = null;
            }
        }

        public void SwitchState(TKey key)
        {
            this.Exit();
            this.currentKey = key;
            this.Enter();
        }

        public void SetupStates(params (TKey, IState)[] states)
        {
            this.states = new List<(TKey, IState)>(states);
        }
    }
}