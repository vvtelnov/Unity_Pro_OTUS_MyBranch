using UnityEngine;

namespace Lessons.Architecture.DI
{
    public sealed class MoveController : MonoBehaviour, 
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField]
        private PlayerService playerService;

        [SerializeField]
        private KeyboardInput input;
        
        void IGameStartListener.OnStartGame()
        {
            this.input.OnMove += this.OnMove;
        }

        void IGameFinishListener.OnFinishGame()
        {
            this.input.OnMove -= this.OnMove;
        }

        private void OnMove(Vector2 direction)
        {
            var offset = new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
            this.playerService.GetPlayer().Move(offset);
        }
    }
}