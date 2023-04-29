using Game.App;
using Game.GameEngine.GameResources;
using JetBrains.Annotations;

namespace Game.Gameplay.Player
{
    [UsedImplicitly]
    public sealed class ResourceMediator : LazyMediator<ResourceRepository, ResourceStorage>
    {
        protected override void OnLoadData(ResourceRepository repository, ResourceStorage storage)
        {
            if (!repository.LoadResources(out var resources))
            {
                var config = ResourceStorageConfig.LoadAsset();
                resources = config.InitialResources;
            }

            storage.Setup(resources);
        }

        protected override void OnSaveData(ResourceRepository repository, ResourceStorage storage)
        {
            var resources = storage.GetAllResources();
            repository.SaveResources(resources);
        }

        protected override void OnStartGame(ResourceStorage storage)
        {
            storage.OnResourceChanged += this.OnResourcesChanged;
        }

        protected override void OnStopGame(ResourceStorage storage)
        {
            storage.OnResourceChanged -= this.OnResourcesChanged;
        }

        private void OnResourcesChanged(ResourceType _, int __)
        {
            this.MarkSaveRequired();
        }
    }
}