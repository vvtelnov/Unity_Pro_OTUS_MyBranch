using Entities;
using Game.GameEngine.Mechanics;
using GameSystem;
using Lessons.Architecture.GameContexts;
using UnityEngine;
using GameContext = GameSystem.GameContext;

namespace Lessons.Architecture.MVA
{
    public sealed class SpeedParameterAdapter : MonoBehaviour, 
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
            this.character.Get<IComponent_OnMoveSpeedChanged>().OnSpeedChanged += this.UpdatePanel;
        }

        void IGameFinishElement.FinishGame()
        {
            this.character.Get<IComponent_OnMoveSpeedChanged>().OnSpeedChanged -= this.UpdatePanel;
        }

        private void SetupPanel()
        {
            var hitPoints = this.character.Get<IComponent_GetMoveSpeed>().Speed;
            this.panel.SetupValue(hitPoints.ToString("F1"));
        }

        private void UpdatePanel(float speed)
        {
            this.panel.UpdateValue(speed.ToString("F1"));
        }
    }
}