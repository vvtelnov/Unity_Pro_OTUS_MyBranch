using System.Collections.Generic;
using Lessons.Architecture.PM.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM.PopUpHelper
{
    public class CharacterInfoSetter : MonoBehaviour
    {
        public void IncreaseStat(Stats stat, uint increment = 1)
        {
            
        }

        public void DecreaseStat(Stats stat, uint increment = 1)
        {
            
        }

        [Title("Set XP")]
        [ShowInInspector]
        private int _xpValue;

        [Button(name: "Increase XP by value")]
        private void IncreaseXpBy_fieldXpValue()
        {
            
        }

        [Title("Increase stats by one")]
        [Button]
        private void IncreaseMoveSpeed()
        {
            IncreaseStat(Stats.MOVE_SPEED);
        }
        
        [Button]
        private void IncreaseStamina()
        {
            IncreaseStat(Stats.STAMINA);
        }
        
        [Button]
        private void IncreaseDexterity()
        {
            IncreaseStat(Stats.DEXTERITY);
        }
        
        [Button]
        private void IncreaseIntelligence()
        {
            IncreaseStat(Stats.INTELLIGENCE);
        }

        [Button]
        private void IncreaseDamage()
        {
            IncreaseStat(Stats.DAMAGE);
        }

        [Button]
        private void IncreaseRegeneration()
        {
            IncreaseStat(Stats.REGENERATION);
        }
        
        
        [Title("Decrease stats by one")]
        [Button]
        private void DecreaseMoveSpeed()
        {
            DecreaseStat(Stats.MOVE_SPEED);
        }
        
        [Button]
        private void DecreaseStamina()
        {
            DecreaseStat(Stats.STAMINA);
        }
        
        [Button]
        private void DecreaseDexterity()
        {
            DecreaseStat(Stats.DEXTERITY);
        }
        
        [Button]
        private void DecreaseIntelligence()
        {
            DecreaseStat(Stats.INTELLIGENCE);
        }

        [Button]
        private void DecreaseDamage()
        {
            DecreaseStat(Stats.DAMAGE);
        }

        [Button]
        private void DecreaseRegeneration()
        {
            DecreaseStat(Stats.REGENERATION);
        }
    }
}