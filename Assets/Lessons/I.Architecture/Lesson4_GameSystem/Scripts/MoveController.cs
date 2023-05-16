using UnityEngine;

namespace Lessons.Architecture.GameSystem
{
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        [SerializeField]
        private KeyboardInput input;

        private void OnEnable()
        {
            this.input.OnMove += this.OnMove;
        }

        private void OnDisable()
        {
            this.input.OnMove -= this.OnMove;
        }

        private void OnMove(Vector2 direction)
        {
            var offset = new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
            this.player.Move(offset);
        }
    }
}