using Game.GameEngine.Mechanics;
using GameSystem;
using InputModule;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay.Hero
{
    public sealed class HeroMoveController : 
        IGameInitElement,
        IGameStartElement,
        IGameFinishElement
    {
        private HeroService heroService;
        
        private JoystickInput input;
        
        private IComponent_MoveInDirection heroComponent;

        [GameInject]
        public void Construct(HeroService heroService, JoystickInput input)
        {
            this.heroService = heroService;
            this.input = input;
        }
        
        void IGameInitElement.InitGame()
        {
            this.heroComponent = this.heroService.GetHero().Get<IComponent_MoveInDirection>();
        }

        void IGameStartElement.StartGame()
        {
            this.input.OnDirectionMoved += this.OnDirectionMoved;
        }

        void IGameFinishElement.FinishGame()
        {
            this.input.OnDirectionMoved -= this.OnDirectionMoved;
        }

        private void OnDirectionMoved(Vector2 screenDirection)
        {
            var worldDirection = new Vector3(screenDirection.x, 0.0f, screenDirection.y);
            this.heroComponent.Move(worldDirection);
        }
    }
}