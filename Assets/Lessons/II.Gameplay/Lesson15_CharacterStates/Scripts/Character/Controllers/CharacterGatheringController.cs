using Entities;
using GameSystem;
using Lessons.Character.Components;
using UnityEngine;

namespace Lessons.Character.Controllers
{
    public sealed class CharacterGatheringController : MonoBehaviour, IGameInitElement, IGameUpdateElement
    {
        [SerializeField]
        private MonoEntity character;

        [SerializeField]
        private MonoEntity resourceObject;

        private GatherResourceComponent _gatherResourceComponent;

        void IGameInitElement.InitGame()
        {
            _gatherResourceComponent = character.Get<GatherResourceComponent>();
        }

        void IGameUpdateElement.OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gatherResourceComponent.StartGather(resourceObject);
            }
        }
    }
}