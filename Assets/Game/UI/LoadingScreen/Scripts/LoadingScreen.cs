using UnityEngine;

namespace Game.UI
{
    public sealed class LoadingScreen : MonoBehaviour
    {
        private static LoadingScreen instance;

        [SerializeField]
        private LoadingProgressBar progressBar;
        
        private void Awake()
        {
            instance = this;
            this.progressBar.SetProgress(0.0f);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public static void Show()
        {
            instance.gameObject.SetActive(true);
        }

        public static void ReportProgress(float progress)
        {
            instance.progressBar.SetProgress(progress);
        }

        public static void Hide()
        {
            instance.gameObject.SetActive(false);
        }
    }
}