using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM.Player
{
    public sealed class CharacterStat
    {
        public event Action<uint> OnValueChanged; 

        public Stats StatType { get; private set; }
        public string Name { get; private set; }
        public uint Value { get; private set; }
        

        public CharacterStat(Stats statType, uint value)
        {
            StatType = statType;
            Value = value;
            Name = StatTypeToViewNameConverter.GetStatViewFormat(statType);
        }

        public void ChangeValue(uint value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(value);
        }
    }
}