using System;
using Sirenix.OdinInspector;

namespace Lessons.Architecture.PM.Player
{
    public sealed class PlayerLevel
    {
        public event Action OnLevelUp;
        public event Action<uint> OnExperienceChanged;

        public uint CurrentLevel { get; internal set; } = 1;

        public uint CurrentExperience { get; internal set; }

        public uint RequiredExperience
        {
            get { return 100 * (this.CurrentLevel + 1); }
        }

        public void AddExperience(int range)
        {
            var xp = Math.Min(this.CurrentExperience + range, this.RequiredExperience);

            if (xp < 0)
                throw new InvalidOperationException("Xp Cannot be less than 0");

            this.CurrentExperience = (uint)xp;
            this.OnExperienceChanged?.Invoke((uint)xp);
        }

        public void LevelUp()
        {
            if (this.CanLevelUp())
            {
                this.CurrentExperience = 0;
                this.CurrentLevel++;
                this.OnLevelUp?.Invoke();
            }
        }

        public bool CanLevelUp()
        {
            return this.CurrentExperience == this.RequiredExperience;
        }
    }
}