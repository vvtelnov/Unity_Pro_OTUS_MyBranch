using System.Threading.Tasks;
using GameSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameEngine.GUI
{
    public static class GameInterfaceDeployer
    {
        private const string OBJECT_NAME = "[GAME_INTERFACE]";

        public static async Task DeployInterface(GameContext gameContext)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("GameInterface");
            await handle.Task;

            var prefab = handle.Result;
            var gameInteface = GameObject.Instantiate(prefab);
            gameInteface.name = OBJECT_NAME;

            var gameElement = gameInteface.GetComponent<IGameElementGroup>();
            gameContext.RegisterElement(gameElement);

            var gameService = gameInteface.GetComponent<IGameServiceGroup>();
            gameContext.RegisterService(gameService);
        }
    }
}