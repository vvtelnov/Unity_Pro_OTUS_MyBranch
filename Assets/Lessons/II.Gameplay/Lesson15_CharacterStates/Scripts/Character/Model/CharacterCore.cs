using System;
using Declarative;
using Lessons.Character.Engines;
using Lessons.Utils;
using Sirenix.OdinInspector;
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
    }
    
    [Serializable]
    public sealed class CharacterLife
    {
        public AtomicVariable<bool> isAlive = true;
    }

    [Serializable]
    public sealed class CharacterMovement
    {
        [SerializeField]
        private Transform transform;

        [Title("Variables")]
        public AtomicVariable<float> movementSpeed = 6f;
        public AtomicVariable<float> rotationSpeed = 10f;
        public MovementDirectionVariable movementDirection;

        [Title("Engines")]
        public MoveInDirectionEngine moveInDirectionEngine;
        public RotateInDirectionEngine rotateInDirectionEngine;
        
        [Construct]
        public void Construct(CharacterLife life)
        {
            moveInDirectionEngine.Construct(transform, movementSpeed);
            rotateInDirectionEngine.Construct(transform, rotationSpeed);
            
            life.isAlive.ValueChanged += isAlive =>
            {
                if (!isAlive)
                {
                    movementDirection.Value = Vector3.zero;
                }
            };

            // MoveInDirectionMechanic
            movementDirection.ValueChanged += direction =>
            {
                if (life.isAlive)
                {
                    moveInDirectionEngine.SetDirection(direction);
                }
            };
            
            // RotateInDirectionMechanic
            movementDirection.ValueChanged += direction =>
            {
                if (life.isAlive)
                {
                    rotateInDirectionEngine.SetDirection(direction);
                }
            };
        }
    }
}