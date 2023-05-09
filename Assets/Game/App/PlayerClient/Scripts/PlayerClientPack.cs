using System.Collections.Generic;
using Services;
using UnityEngine;

namespace Game.App
{
    [CreateAssetMenu(
        fileName = "PlayerClientPack",
        menuName = "App/New PlayerClientPack"
    )]
    public sealed class PlayerClientPack : ServicePackBase
    {
        public override IEnumerable<object> ProvideServices()
        {
            yield return new PlayerClient();
        }
    }
}