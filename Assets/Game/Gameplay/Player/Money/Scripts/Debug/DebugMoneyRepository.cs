#if UNITY_EDITOR
using JetBrains.Annotations;

namespace Game.Gameplay.Player
{
    [UsedImplicitly]
    public sealed class DebugMoneyRepository : MoneyRepository
    {
        public override bool LoadMoney(out int money)
        {
            var config = DebugMoneyConfig.LoadAsset();
            if (config.debugMode)
            {
                money = config.money;
                return true;
            }

            return base.LoadMoney(out money);
        }
    }
}
#endif