using System.Collections.Generic;
using Game.Gameplay.Player;
using GameSystem;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.MetaGame
{
    public sealed class TimeRewardModule : MonoBehaviour, IGameConstructElement, IGameServiceGroup, IGameElementGroup
    {
        [ShowInInspector]
        public readonly TimeReward timeReward = new();

        [SerializeField]
        private TimeRewardConfig config;
        
        public IEnumerable<IGameElement> GetElements()
        {
            yield return this.timeReward;
        }

        public IEnumerable<object> GetServices()
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