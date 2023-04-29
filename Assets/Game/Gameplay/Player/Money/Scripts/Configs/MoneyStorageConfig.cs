using UnityEngine;

namespace Game.Gameplay.Player
{
    [CreateAssetMenu(
        fileName = "MoneyConfig",
        menuName = "Gameplay/Player/New MoneyStorageConfig"
    )]
    public sealed class MoneyStorageConfig : ScriptableObject
    {
        public const string CONFIG_PATH = "PlayerMoneyConfig";

        public int InitialMoney
        {
            get { return this.initialMoney; }
        }

        [SerializeField]
        private int initialMoney;

        public static MoneyStorageConfig LoadAsset()
        {
            return Resources.Load<MoneyStorageConfig>(CONFIG_PATH);
        }
    }
}