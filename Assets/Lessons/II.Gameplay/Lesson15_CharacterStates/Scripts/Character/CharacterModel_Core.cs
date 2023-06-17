using System;
using Declarative;
using UnityEngine;

namespace Lessons.Gameplay.States
{
    [Serializable]
    public sealed class CharacterModel_Core
    {
        [Section]
        public Move move = new();

        [Section]
        public Life life = new();

        [Serializable]
        public sealed class Move
        {
            [SerializeField]
            public Transform moveTransform;

            public AtomicEvent<Vector3> onMove = new();
            public AtomicVariable<bool> moveRequired = new();
            public AtomicVariable<Vector3> moveDirection = new();
            public AtomicVariable<float> speed = new();

            public FixedUpdateMechanics fixedUpdate = new FixedUpdateMechanics();

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

                this.fixedUpdate.Do(deltaTime =>
                {
                    if (this.moveRequired.Value)
                    {
                        this.moveTransform.position += this.moveDirection.Value * (this.speed.Value * deltaTime);
                        this.moveTransform.rotation = Quaternion.LookRotation(this.moveDirection.Value, Vector3.up);
                        this.moveRequired.Value = false;
                    }
                });
            }
        }

        [Serializable]
        public sealed class Life
        {
            public AtomicVariable<bool> isDeath;
        }
    }
}