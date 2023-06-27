using System;
using Declarative;
using Lessons.Character.Engines;
using Lessons.StateMachines;
using Lessons.StateMachines.States;
using Lessons.Utils;
using UnityEngine;

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
        public AtomicEvent<ResourceObject> onStart;
        public AtomicVariable<ResourceObject> target;
        public AtomicEvent onComplete;

        [Construct]
        public void Construct()
        {
            this.onStart += resource =>
            {
                Debug.Log($"Start gathering {resource.resourceType}");
                this.target.Value = resource;
            };

            this.onComplete += () =>
            {
                var resource = this.target.Value;
                resource.gameObject.SetActive(false);
                Debug.Log($"<color=green>Complete gathering {resource.resourceType} {resource.amount}</color>");
                this.target.Value = null;
            };
        }
    }

    [Serializable]
    public sealed class CharacterStates
    {
        public StateMachine stateMachine;

        [Section]
        public IdleState idleState;

        [Section]
        public RunState runState;

        [Section]
        public DeadState deadState;

        [Section]
        public GatherResourceState gatherState;

        [Construct]
        public void Construct()
        {
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
                if (life.isAlive)
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