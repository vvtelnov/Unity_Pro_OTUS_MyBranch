using System;
using Declarative;
using UnityEngine;

namespace Lessons.Gameplay.States
{
    [Serializable]
    public sealed class CharacterModel_Core
    {
        [Section]
        public Life life = new();

        [Section]
        public Move move = new();
        
        [Section]
        public States states = new ();

        [Serializable]
        public sealed class Life
        {
            public AtomicVariable<bool> isDeath;
        }

        [Serializable]
        public sealed class Move
        {
            [SerializeField]
            public Transform moveTransform;

            public AtomicEvent<Vector3> onMove = new();
            public AtomicVariable<bool> moveRequired = new();
            public AtomicVariable<Vector3> moveDirection = new();
            public AtomicVariable<float> speed = new();

            [Construct]
            public void Construct(Life life)
            {
                this.onMove += direction =>
                {
                    if (life.isDeath.Value)
                    {
                        return;
                    }

                    this.moveDirection.Value = direction;
                    this.moveRequired.Value = true;
                };
            }
        }
        
        [Serializable]
        public sealed class States
        {
            public StateMachine<CharacterStateType> fsm;
            
            public CharacterStateIdle idle = new();
            public CharacterStateMove move = new();
            public CharacterDeathState death = new();

            [Construct]
            public void Construct(Move move, Life life)
            {
                this.fsm.SetupStates(
                    (CharacterStateType.IDLE, this.idle),
                    (CharacterStateType.MOVE, this.move),
                    (CharacterStateType.DEATH, this.death)
                );
                
                var isDeath = life.isDeath;
                var moveRequired = move.moveRequired;
                var moveDirection = move.moveDirection;
                var speed = move.speed;
                var transform = move.moveTransform;
                
                this.idle.Construct(this.fsm, isDeath, moveRequired);
                this.move.Construct(moveRequired, moveDirection, speed, transform);
                this.move.Construct(this.fsm, isDeath);
                
                this.death.Construct(this.fsm, moveRequired, isDeath);
            }
        }
    }
}