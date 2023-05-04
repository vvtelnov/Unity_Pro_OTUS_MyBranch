using System;
using Game.GameEngine;
using UnityEngine;

namespace Game.App
{
    public sealed class LoadingTask_LoadPopups : ILoadingTask
    {
        public async void Do(Action<LoadingResult> callback)
        {
            var popupCatalog = Resources.Load<PopupCatalog>(nameof(PopupCatalog));
            await popupCatalog.PreloadPrefabs();
            callback.Invoke(LoadingResult.Success());
        }
    }
}