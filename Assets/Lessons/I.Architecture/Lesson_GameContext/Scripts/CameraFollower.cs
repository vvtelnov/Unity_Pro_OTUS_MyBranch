using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.Architecture.GameContexts
{
    public sealed class CameraFollower : MonoBehaviour,
        IConstructListener,
        IStartGameListener,
        IFinishGameListener
    {
        private Transform targetCamera;

        private IComponent_GetPosition characterComponent;

        [SerializeField]
        private Vector3 offset;

        private void Awake()
        {
            this.enabled = false;
        }

        private void LateUpdate()
        {
            this.targetCamera.position = this.characterComponent.Position + this.offset;
        }

        void IConstructListener.Construct(GameContext context)
        {
            this.targetCamera = context
                .GetService<CameraService>()
                .CameraTransform;

            this.characterComponent = context
                .GetService<CharacterService>()
                .GetCharacter()
                .Get<IComponent_GetPosition>();
        }

        void IStartGameListener.OnStartGame()
        {
            this.enabled = true;
        }

        void IFinishGameListener.OnFinishGame()
        {
            this.enabled = false;
        }
    }
}