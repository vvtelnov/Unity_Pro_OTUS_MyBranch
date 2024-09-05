using System.Collections.Generic;
using Lessons.Architecture.PM.Player;
using UnityEngine;

namespace Lessons.Architecture.PM.CharacterPopupPresenter
{
    public interface ICharacterPopupPresenter
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; set; }
        public string CurrentLevel { get; }
        public string XpFullStr { get; }
        public uint CurrentXp { get; }
        public uint MaxBarXp { get; }
        public bool IsXpBarFull => CurrentXp == MaxBarXp;
        public bool CanLevelUp { get; }

        public Dictionary<Stats, string> CharacterStats { get; }

    }
}