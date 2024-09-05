using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM.Player
{
    public enum Stats
    {
        MOVE_SPEED = 0,    
        STAMINA = 1,
        DEXTERITY = 2,
        INTELLIGENCE = 3,
        DAMAGE = 4,
        REGENERATION = 5,
    }

    public sealed class CharacterStat
    {
        public event Action<uint> OnValueChanged; 

        public Stats StatType { get; private set; }
        public string Name { get; private set; }
        public uint Value { get; private set; }
        
        // нарушает SRP. Но я не знаю куда эту логику положить.
        private readonly Dictionary<Stats, string> StatTypeToViewNameMap = new()
        {
            { Stats.MOVE_SPEED,  "Move Speed"},
            { Stats.STAMINA,  "Stamina"},
            { Stats.DEXTERITY, "Dexterity"},
            { Stats.INTELLIGENCE,  "Intelligence"},
            { Stats.DAMAGE,  "Damage"},
            { Stats.REGENERATION, "Regeneration"},
        };

        public CharacterStat(Stats statType, uint value)
        {
            StatType = statType;
            Value = value;
            Name = StatTypeToViewNameMap[StatType];
        }

        public void ChangeValue(uint value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(value);
        }
    }
}