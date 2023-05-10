using System.Collections.Generic;
using Services;
using UnityEngine;

namespace Game.App
{
    [CreateAssetMenu(
        fileName = "RepositoryServicePack",
        menuName = "App/New RepositoryServicePack"
    )]
    public sealed class RepositoryServicePack : ServicePackBase
    {
        public override IEnumerable<object> ProvideServices()
        {
            yield return new RepositorySynchronizer();
        }
    }
}