using System;
using Declarative;
using Game.GameEngine.GameResources;
using Lessons.Character.Animations;
using Lessons.StateMachines.States;
using UnityEngine;

namespace Lessons.Character.Model
{
    [Serializable]
    public sealed class CharacterVisual
    {
        public AnimatorStateMachine<AnimatorStateType> animatorMachine;

        public ParticleSystem chopVFX;
        public ParticleSystem mineVFX;

        public AudioSource audioSource;
        public AudioClip chopSFX;
        public AudioClip mineSFX;

        public GameObject axe;
        public GameObject pickaxe;

        [Construct]
        public void Constuct()
        {
            this.axe.SetActive(false);
            this.pickaxe.SetActive(false);
        }

        [Construct]
        public void ConstructStates()
        {
            this.animatorMachine.Construct(
                
                (AnimatorStateType.Idle, null),
                
                (AnimatorStateType.Run, null),
                
                (AnimatorStateType.Dead, null),
                
                (AnimatorStateType.Chop, new DelegateState(
                    onEnter: () => this.axe.SetActive(true),
                    onExit: () => this.axe.SetActive(false)
                )),
                
                (AnimatorStateType.Mine, new DelegateState(
                    onEnter: () => this.pickaxe.SetActive(true),
                    onExit: () => this.pickaxe.SetActive(false)
                ))
            );
        }

        [Construct]
        public void ConstructTransitions(CharacterStates states, CharacterGathering gathering)
        {
            var coreFSM = states.stateMachine;
            var resourceObject = gathering.target;

            this.animatorMachine.AddTransition(AnimatorStateType.Idle, () =>
                coreFSM.CurrentState == CharacterStateType.Idle);

            this.animatorMachine.AddTransition(AnimatorStateType.Run,
                () => coreFSM.CurrentState == CharacterStateType.Run);

            this.animatorMachine.AddTransition(AnimatorStateType.Dead,
                () => coreFSM.CurrentState == CharacterStateType.Dead);

            this.animatorMachine.AddTransition(AnimatorStateType.Chop, () =>
                coreFSM.CurrentState == CharacterStateType.Gathering &&
                resourceObject.Value.Get<IComponent_GetResourceType>().Type == ResourceType.WOOD);

            this.animatorMachine.AddTransition(AnimatorStateType.Mine, () =>
                coreFSM.CurrentState == CharacterStateType.Gathering &&
                resourceObject.Value.Get<IComponent_GetResourceType>().Type == ResourceType.STONE);
        }

        [Construct]
        public void Construct()
        {
            this.animatorMachine.OnMessageReceived += message =>
            {
                if (message == "chop")
                {
                    this.chopVFX.Play();
                    this.audioSource.PlayOneShot(this.chopSFX);
                }
                else if (message == "mine")
                {
                    this.mineVFX.Play();
                    this.audioSource.PlayOneShot(this.mineSFX);
                }
            };
        }
    }
}