using Entities;
using Game.GameEngine.Mechanics;
using GameSystem;
using Lessons.Architecture.GameContexts;
using UnityEngine;
using GameContext = GameSystem.GameContext;

namespace Lessons.Architecture.MVA
{
    public sealed class DamageParameterAdapter : MonoBehaviour, 
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
            this.character.Get<IComponent_OnMeleeDamageChanged>().OnDamageChanged += this.UpdatePanel;
        }

        void IGameFinishElement.FinishGame()
        {
            this.character.Get<IComponent_OnMeleeDamageChanged>().OnDamageChanged -= this.UpdatePanel;
        }

        private void SetupPanel()
        {
            var damage = this.character.Get<IComponent_GetMeleeDamage>().Damage;
            this.panel.SetupValue(damage.ToString());
        }

        private void UpdatePanel(int newDamage)
        {
            this.panel.UpdateValue(newDamage.ToString());
        }
    }
}