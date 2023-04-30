using System.Threading.Tasks;
using Asyncoroutine;
using UnityEngine;

namespace Game.Tutorial.App
{
    public sealed class TutorialAssetSupplier
    {
        private const string ENGINE_KEY = "TutorialEngine";

        private const string INTERFACE_KEY = "TutorialInterface";

        private const string STEP_CONFIG_PATH = "TutorialStepConfig";

        public TutorialList LoadStepConfig()
        {
            return Resources.Load<TutorialList>(STEP_CONFIG_PATH);
        }

        public async Task<GameObject> LoadTutorialEngine()
        {
            var handle = Resources.LoadAsync<GameObject>(ENGINE_KEY);
            await new WaitUntil(() => handle.isDone);
            return (GameObject) handle.asset;
        }

        public async Task<GameObject> LoadTutorialInterface()
        {
            var handle = Resources.LoadAsync<GameObject>(INTERFACE_KEY);
            await new WaitUntil(() => handle.isDone);
            return (GameObject) handle.asset;
        }
    }
}