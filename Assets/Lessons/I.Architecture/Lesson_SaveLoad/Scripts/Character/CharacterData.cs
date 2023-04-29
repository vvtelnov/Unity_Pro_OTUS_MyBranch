using System;

namespace Lessons.Architecture.SaveLoad
{
    [Serializable]
    public struct CharacterData
    {
        public int currentHitPoints;
        public int maxHitPoints;
        public int meleeDamage;
        public float moveSpeed;
    }
}