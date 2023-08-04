using System.Collections.Generic;
using Game.Gameplay.Player;
using GameSystem;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.MetaGame
{
    public sealed class TimeRewardModule : MonoBehaviour, 
        IGameServiceGroup,
        IGameElementGroup,
        IGameConstructElement
    {
        [ShowInInspector]
        public readonly TimeReward timeReward = new();

        [SerializeField]
        private TimeRewardConfig config;

        IEnumerable<object> IGameServiceGroup.GetServices()
        {
            yield return this.timeReward;
        }

        IEnumerable<IGameElement> IGameElementGroup.GetElements()
        {
            yield return this.timeReward;
        }

        void IGameConstructElement.ConstructGame(GameContext context)
        {
            var moneyStorage = context.GetService<MoneyStorageDecorator>();
            this.timeReward.Construct(moneyStorage, this.config);
            
            var saveLoader = ServiceLocator.GetService<RealtimeSaveLoader>();
            saveLoader.RegisterTimer(this.timeReward);
        }
    }
}