using System;
using System.Collections.Generic;
using System.Linq;
using Lessons.Architecture.PM.Player;
using UnityEngine;

namespace Lessons.Architecture.PM.CharacterPopupPresenter
{
    public class PopupPresenter : ICharacterPopupPresenter
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; set; }
        
        public string CurrentLevel { get; }
        public string XpFullStr { get; }
        public uint CurrentXp { get; }
        public uint MaxBarXp { get; }
        public bool IsXpBarFull { get; }
        public bool CanLevelUp { get; }
        
        public Dictionary<Stats, string> CharacterStats => _characterStatsInViewFormat;

        private readonly Dictionary<Stats, string> _characterStatsInViewFormat = new();

        public PopupPresenter(CharacterStat[] stats,
            string name,
            string description,
            Sprite icon,
            uint currentLevel,
            uint currentXp,
            uint maxBarXp,
            bool canLevelUp)
        {
            Name = string.Concat("@", name);
            Description = description;
            Icon = icon;
            CurrentLevel = $"Level: {currentLevel}";
            XpFullStr = $"XP: {currentXp} / {maxBarXp}";
            CurrentXp = currentXp;
            MaxBarXp = maxBarXp;
            IsXpBarFull = currentXp == maxBarXp;
            CanLevelUp = canLevelUp;

            SetUpCharStatsForViewFormat(stats);
        }

        private void SetUpCharStatsForViewFormat(CharacterStat[] stats)
        {
            foreach (var stat in stats)
            {
                string viewFormat = $"{stat.Name}: {stat.Value}"; 

                _characterStatsInViewFormat.Add(stat.StatType, viewFormat);
            }
        }
    }
}