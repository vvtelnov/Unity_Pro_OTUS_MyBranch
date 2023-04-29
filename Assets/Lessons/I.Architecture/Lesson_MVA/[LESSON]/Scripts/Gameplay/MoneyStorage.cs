using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.MVA
{
    public sealed class MoneyStorage : MonoBehaviour
    {
        public event Action<BigNumber> OnMoneyChanged;

        public BigNumber Money
        {
            get { return this.money; }
        }

        [ReadOnly]
        [ShowInInspector]
        private BigNumber money;

        [Button]
        public void SetupMoney(BigNumber money)
        {
            this.money = money;
        }

        [Button]
        public void AddMoney(BigNumber range)
        {
            this.money += range;
            this.OnMoneyChanged?.Invoke(this.money);
        }

        [Button]
        public void SpendMoney(BigNumber range)
        {
            this.money -= range;
            this.OnMoneyChanged?.Invoke(this.money);
        }
    }
}