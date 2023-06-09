using Game.UI;
using UnityEngine;

namespace Lessons.Architecture.Loading
{
    public sealed class ApplicationLoader : MonoBehaviour
    {
        [SerializeField]
        private LoadingTask[] loadingTasks;

        public async void Start()
        {
            foreach (LoadingTask task in this.loadingTasks)
            {
                LoadingTask.Result result = await task.Do();
                if (!result.success)
                {
                    LoadingScreen.ReportError(result.error); //мб показ попапа ошибки...
                    break;
                }
            }
        }
    }
}