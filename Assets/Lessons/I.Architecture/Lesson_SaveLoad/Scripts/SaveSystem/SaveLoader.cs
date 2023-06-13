using GameSystem;
using Newtonsoft.Json;

namespace Lessons.Architecture.SaveLoad
{
    public abstract class SaveLoader<TData, TService> : ISaveLoader
    {
        protected string DataKey
        {
            get { return typeof(TData).Name; }
        }

        void ISaveLoader.LoadGame(IGameRepository repository, GameContext context)
        {
            var gameService = context.GetService<TService>();

            if (repository.TryGetData(this.DataKey, out var serializedData))
            {
                TData data = JsonConvert.DeserializeObject<TData>(serializedData);
                this.SetupData(gameService, data);
            }
            else
            {
                this.SetupByDefault(gameService);
            }
        }

        void ISaveLoader.SaveGame(IGameRepository repository, GameContext context)
        {
            var gameService = context.GetService<TService>();
            var data = this.ConvertToData(gameService);
            var serializedData = JsonConvert.SerializeObject(data);
            repository.SetData(this.DataKey, serializedData);
        }

        protected abstract void SetupData(TService gameService, TData data);

        protected abstract TData ConvertToData(TService gameService);

        protected virtual void SetupByDefault(TService gameService)
        {
        }
    }
}