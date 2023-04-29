using Entities;
using Game.GameEngine.Mechanics;
using GameSystem;
using Lessons.Architecture.GameContexts;
using UnityEngine;
using GameContext = GameSystem.GameContext;

namespace Lessons.Architecture.MVA
{
    public sealed class HitPointsParameterAdapter : MonoBehaviour, 
        IGameConstructElement,
        IGameReadyElement,
        IGameFinishElement
    {
        [SerializeField]
        private PropertyPanel panel;
    
        private IEntity character;
    
        void IGameConstructElement.ConstructGame(GameContext context)
        {
            this.character = context.GetService<CharacterService>().GetCharacter();
            this.SetupPanel();
        }

        void IGameReadyElement.ReadyGame()
        {
            this.character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged += this.UpdatePanel;
        }

        void IGameFinishElement.FinishGame()
        {
            this.character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged -= this.UpdatePanel;
        }

        private void SetupPanel()
        {
            var hitPoints = this.character.Get<IComponent_GetHitPoints>().HitPoints;
            this.panel.SetupValue(hitPoints.ToString());
        }

        private void UpdatePanel(int newHitPoints)
        {
            this.panel.UpdateValue(newHitPoints.ToString());
        }
    }
}