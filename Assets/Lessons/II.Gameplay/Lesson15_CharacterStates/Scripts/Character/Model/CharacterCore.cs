using System;
using Declarative;
using Entities;
using Game.GameEngine.GameResources;
using Game.GameEngine.Mechanics;
using Lessons.Character.Engines;
using Lessons.Gameplay.Interaction;
using Lessons.StateMachines;
using Lessons.StateMachines.States;
using Lessons.Utils;
using UnityEngine;
using TransformSynchronizer = Lessons.Utils.TransformSynchronizer;

namespace Lessons.Character.Model
{
    [Serializable]
    public sealed class CharacterCore
    {
        [Section]
        public CharacterLife life;

        [Section]
        public CharacterMovement movement;

        [Section]
        public CharacterGathering gathering;

        [Section]
        public CharacterCollision collision;

        [Section]
        public CharacterStates states;
    }

    [Serializable]
    public sealed class CharacterLife
    {
        public AtomicVariable<bool> isAlive;
    }

    [Serializable]
    public sealed class CharacterMovement
    {
        public Transform transform;

        public AtomicVariable<float> movementSpeed = new(6f);
        public AtomicVariable<float> rotationSpeed = new(10f);
        public MovementDirectionVariable movementDirection;

        public MoveInDirectionEngine moveInDirectionEngine;
        public RotateInDirectionEngine rotateInDirectionEngine;

        [Construct]
        public void Construct()
        {
            moveInDirectionEngine.Construct(transform, movementSpeed);
            rotateInDirectionEngine.Construct(transform, rotationSpeed);
        }
    }

    [Serializable]
    public sealed class CharacterGathering
    {
        public AtomicVariable<float> duration = new(3);
        public AtomicEvent<IEntity> onStart;
        public AtomicEvent onComplete;
        public AtomicVariable<IEntity> target;

        [Construct]
        public void Construct()
        {
            this.onStart += resource =>
            {
                
                Debug.Log($"Start gathering {resource.Get<IComponent_GetResourceType>()}");
                this.target.Value = resource;
            };

            this.onComplete += () =>
            {
                var resource = this.target.Value;
                resource.Get<IComponent_Destoy>().Destroy();
                var resourceType = resource.Get<IComponent_GetResourceType>().Type;
                var resourceAmount = resource.Get<IComponent_GetResourceCount>().Count;
                this.target.Value = null;
                Debug.Log($"<color=green>Complete gathering {resourceType} {resourceAmount}</color>");
            };
        }
    }

    [Serializable]
    public sealed class CharacterCollision
    {
        public CollisionSensor sensor;
        public TransformSynchronizer synchronizer;
    }

    [Serializable]
    public sealed class CharacterStates
    {
        public StateMachine<CharacterStateType> stateMachine;

        [Section]
        public IdleState idleState;

        [Section]
        public RunState runState;

        [Section]
        public DeadState deadState;

        [Section]
        public GatherCompositeState gatherState;

        [Construct]
        public void Construct(CharacterModel root)
        {
            root.onStart += () => this.stateMachine.Enter();
        
            stateMachine.Construct(
                (CharacterStateType.Idle, idleState),
                (CharacterStateType.Run, runState),
                (CharacterStateType.Dead, deadState),
                (CharacterStateType.Gathering, gatherState)
            );
        }

        [Construct]
        public void ConstructTransitions(CharacterLife life, CharacterMovement movement, CharacterGathering gathering)
        {
            life.isAlive.ValueChanged += isAlive =>
            {
                var stateType = isAlive ? CharacterStateType.Idle : CharacterStateType.Dead;
                stateMachine.SwitchState(stateType);
            };

            movement.movementDirection.MovementStarted += () =>
            {
                if (life.isAlive)
                {
                    stateMachine.SwitchState(CharacterStateType.Run);
                }
            };

            movement.movementDirection.MovementFinished += () =>
            {
                if (life.isAlive && stateMachine.CurrentState == CharacterStateType.Run)
                {
                    stateMachine.SwitchState(CharacterStateType.Idle);
                }
            };

            gathering.onStart += _ =>
            {
                if (life.isAlive)
                {
                    stateMachine.SwitchState(CharacterStateType.Gathering);
                }
            };

            gathering.onComplete += () =>
            {
                if (life.isAlive)
                {
                    stateMachine.SwitchState(CharacterStateType.Idle);
                }
            };
        }
    }
}