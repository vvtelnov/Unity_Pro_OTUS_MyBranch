using System.Collections;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.Gameplay.CharacterStates
{
    public class RotateInDirectionState : StateCoroutine
    {
        [SerializeField]
        private UMoveInDirectionMotor moveEngine;

        [SerializeField]
        private UTransformEngine transformEngine;

        [SerializeField]
        private float rotationSpeed = 60;
    
        protected override IEnumerator Do()
        {
            var delay = new WaitForFixedUpdate();
            while (true)
            {
                yield return delay;
                this.RotateTransform();
            }
        }

        private void RotateTransform()
        {
            var direction = this.moveEngine.Direction;
            this.transformEngine.RotateTowardsInDirection(direction, this.rotationSpeed, Time.fixedDeltaTime);
        }
    }
}