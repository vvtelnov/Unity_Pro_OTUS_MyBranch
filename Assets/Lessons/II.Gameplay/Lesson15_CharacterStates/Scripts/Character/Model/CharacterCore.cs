using System;
using Declarative;
using Lessons.Character.Engines;
using Lessons.Utils;
using UnityEngine;

namespace Lessons.Character.Model
{
    [Serializable]
    public sealed class CharacterCore
    {
        [Section]
        public CharacterMovement movement;
    }

    [Serializable]
    public sealed class CharacterMovement
    {
        public Transform transform;

        public AtomicVariable<float> movementSpeed = 6f;
        public AtomicVariable<float> rotationSpeed = 10f;
        public MovementDirectionVariable movementDirection;
        
        public MoveInDirectionEngine moveInDirectionEngine;
        public RotateInDirectionEngine rotateInDirectionEngine;

        [Construct]
        public void Construct()
        {
            moveInDirectionEngine.Construct(transform, movementSpeed);
            rotateInDirectionEngine.Construct(transform, rotationSpeed);

            movementDirection.ValueChanged += direction =>
            {
                moveInDirectionEngine.SetDirection(direction);
                rotateInDirectionEngine.SetDirection(direction);
            };
        }
    }
}