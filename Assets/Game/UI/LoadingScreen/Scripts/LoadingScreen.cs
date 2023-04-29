using UnityEngine;

namespace Game.UI
{
    public sealed class LoadingScreen : MonoBehaviour
    {
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}