using System;
using Elementary;
using Game.GameEngine.GameResources;
using Lessons.Character.Model;
using UnityEngine;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public class GatherAnimatorState : IState
    {
        [SerializeField]
        private string stateName = "State";

        private Animator _animator;
        private Variable<ResourceObject> _resource;
        private int _state;

        public void Construct(Animator animator, Variable<ResourceObject> resource)
        {
            _animator = animator;
            _resource = resource;
            _state = Animator.StringToHash(stateName);
        }
        
        void IState.Enter()
        {
            if (_resource.Current.resourceType == ResourceType.WOOD)
            {
                _animator.SetInteger(_state, (int) AnimatorStateType.Chop);
            }
            else
            {
                _animator.SetInteger(_state, (int) AnimatorStateType.Mine);
            }
        }

        void IState.Exit()
        {
            
        }
    }
}