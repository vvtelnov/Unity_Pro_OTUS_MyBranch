using System;
using Declarative;
using Lessons.Character.Engines;
using Lessons.Character.Variables;
using Lessons.Engine.Atomic.Values;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Character
{
    public sealed class CharacterModel : DeclarativeModel
    {
        [Section]
        public Core core;

        [Section]
        public Visual visual;

        [Serializable]
        public sealed class Core
        {
            [Section]
            public Life life;
            
            [Section]
            public Move move;

            [Serializable]
            public sealed class Life
            {
                public AtomicVariable<bool> isAlive = true;
            }
            
            [Serializable]
            public sealed class Move
            {
                [SerializeField]
                public Transform targetTransform;

                [Title("Variables")]
                public AtomicVariable<float> movementSpeed = 6f;
                public AtomicVariable<float> rotationSpeed = 10f;

                public MovementDirectionVariable movementDirection;
                
                [Title("Engines")]
                public MovementEngine movementEngine;
                public RotationEngine rotationEngine;

                [Construct]
                public void Construct(Life life)
                {
                    movementEngine.Construct(targetTransform, movementSpeed);
                    rotationEngine.Construct(targetTransform, rotationSpeed);

                    // MoveInDirection mechanic
                    movementDirection.OnChanged += direction =>
                    {
                        if (life.isAlive)
                        {
                            movementEngine.SetDirection(direction);
                        }
                    };
                    
                    // RotateInDirection mechanic
                    movementDirection.OnChanged += direction =>
                    {
                        if (life.isAlive)
                        {
                            rotationEngine.SetDirection(direction);
                        }
                    };
                }
            }
        }
        
        [Serializable]
        public sealed class Visual
        {
            public Animator animator;
            
            private readonly int _state = Animator.StringToHash("State");
                
            [Construct]
            public void Construct(Core core)
            {
                core.life.isAlive.OnChanged += isAlive =>
                {
                    SetAnimatorState(isAlive ? AnimatorState.Idle : AnimatorState.Dead);
                };
                    
                core.move.movementDirection.MovementStarted += () => 
                {
                    if (core.life.isAlive)
                    {
                        SetAnimatorState(AnimatorState.Move);
                    }
                };

                core.move.movementDirection.MovementFinished += () =>
                {
                    if (core.life.isAlive)
                    {
                        SetAnimatorState(AnimatorState.Idle);
                    }
                };
            }

            private void SetAnimatorState(AnimatorState state)
            {
                animator.SetInteger(_state, (int) state);
            }

            private enum AnimatorState
            {
                Idle = 0,
                Move = 1,
                Dead = 5
            }
        }
    }
}