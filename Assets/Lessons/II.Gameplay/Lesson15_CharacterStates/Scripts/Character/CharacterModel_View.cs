using System;
using Declarative;
using UnityEngine;

namespace Lessons.Gameplay.States
{
    [Serializable]
    public sealed class CharacterModel_View
    {
        private static readonly int State = Animator.StringToHash("State");
        private const int IDLE_STATE = 0;
        private const int MOVE_STATE = 1;
        private const int DEATH_STATE = 5;

        public Animator animator;

        private readonly LateUpdateMechanics lateUpdate = new();

        [Construct]
        public void Construct(CharacterModel_Core core)
        {
            this.lateUpdate.SetAction(_ =>
            {
                if (core.life.isDeath.Value)
                {
                    this.animator.SetInteger(State, DEATH_STATE);
                    return;
                }

                if (core.move.moveRequired.Value)
                {
                    this.animator.SetInteger(State, MOVE_STATE);
                }
                else
                {
                    this.animator.SetInteger(State, IDLE_STATE);
                }
            });
        }
    }
}