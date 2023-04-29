using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.MVA
{
    public sealed class GemsStorage : MonoBehaviour
    {
        public event Action<int> OnGemsChanged;

        public int Gems
        {
            get { return this.gems; }
        }

        [ReadOnly]
        [ShowInInspector]
        private int gems;

        [Button]
        public void SetupGems(int gems)
        {
            this.gems = gems;
        }

        [Button]
        public void AddGems(int range)
        {
            this.gems += range;
            this.OnGemsChanged?.Invoke(this.gems);
        }

        [Button]
        public void SpendGems(int range)
        {
            this.gems -= range;
            this.OnGemsChanged?.Invoke(this.gems);
        }
    }
}