using System;
using Declarative;
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
    }
    
    [Serializable]
    public sealed class CharacterLife
    {
        public AtomicVariable<bool> isAlive = true;
    }

    [Serializable]
    public sealed class CharacterMovement
    {
        public Transform transform;
        
        public MovementDirectionVariable movementDirection;
    }
}