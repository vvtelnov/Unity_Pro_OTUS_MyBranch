using System;
using System.Collections;
using Elementary;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.Gameplay.CharacterStates
{
    public sealed class MoveInDirectionState : StateCoroutine
    {
        [SerializeField]
        private UMoveInDirectionMotor moveEngine;

        [SerializeField]
        private UTransformEngine transformEngine;

        [SerializeField]
        private FloatAdapter moveSpeed;

        protected override IEnumerator Do()
        {
            var delay = new WaitForFixedUpdate();
            while (true)
            {
                yield return delay;
                this.MoveTransform();
            }
        }

        private void MoveTransform()
        {
            var direction = this.moveEngine.Direction;
            var velocity = direction * (this.moveSpeed.Current * Time.fixedDeltaTime);
            this.transformEngine.MovePosition(velocity);
        }
    }
}