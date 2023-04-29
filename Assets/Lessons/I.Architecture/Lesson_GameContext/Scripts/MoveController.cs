using Entities;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.Architecture.GameContexts
{
    public sealed class MoveController : MonoBehaviour,
        IConstructListener,
        IStartGameListener,
        IFinishGameListener
    {
        [SerializeField]
        private KeyboardInput input;

        private IComponent_MoveInDirection characterComponent;

        void IConstructListener.Construct(GameContext context)
        {
            this.input = context.GetService<KeyboardInput>();
            this.characterComponent = context.GetService<CharacterService>()
                    .GetCharacter()
                    .Get<IComponent_MoveInDirection>();
        }

        void IStartGameListener.OnStartGame()
        {
            this.input.OnMove += this.OnMove;
        }

        void IFinishGameListener.OnFinishGame()
        {
            this.input.OnMove -= this.OnMove;
        }

        private void OnMove(Vector3 direction)
        {
            this.characterComponent.Move(direction);
        }
    }
}