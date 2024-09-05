using System;
using System.Collections.Generic;
using Lessons.Architecture.PM.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.PopUpHelper
{
    public class CharacterInitInfo : MonoBehaviour
    {
        public uint MoveSpeed => _initStats[Stats.MOVE_SPEED];
        
        public uint Stamina => _initStats[Stats.STAMINA];
        
        public uint Dexterity => _initStats[Stats.DEXTERITY];
        
        public uint Intelligence => _initStats[Stats.INTELLIGENCE];
        
        public uint Damage => _initStats[Stats.DAMAGE];
        
        public uint Regeneration => _initStats[Stats.REGENERATION];
        
        public string Name => _name;
        public Sprite ProfilePicture => _profilePicture;
        public string Description => _description;
        
        public Dictionary<Stats, uint> InitStats => _initStats;

        public uint Xp => _xp;
        public uint Lvl => _lvl;
        
        [Title("Input Character stats")]
        [SerializeField] private uint _moveSpeed;    
        [SerializeField] private uint _stamina;
        [SerializeField] private uint _dexterity;
        [SerializeField] private uint _intelligence;    
        [SerializeField] private uint _damage;    
        [SerializeField] private uint _regeneration;

        [Title("Input Character Info")]
        [SerializeField] private string _name;
        [SerializeField] private Sprite _profilePicture;
        [TextArea(3, 10), SerializeField] private string _description;

        [Title("Input Character XP")]
        [SerializeField]
        private uint _lvl;
        [SerializeField, PropertyRange(0, 1000)]
        private uint _xp;

        private readonly Dictionary<Stats, uint> _initStats = new();

        public CharacterInitInfo()
        {
            ConstructInitStats();
        }

        private void ConstructInitStats()
        {
            Debug.Log("construdcted init stats");
            _initStats[Stats.MOVE_SPEED] = _moveSpeed;
            _initStats[Stats.STAMINA] = _stamina;
            _initStats[Stats.DEXTERITY] = _dexterity;
            _initStats[Stats.INTELLIGENCE] = _intelligence;
            _initStats[Stats.DAMAGE] = _damage;
            _initStats[Stats.REGENERATION] = _regeneration;
        }
    }
}