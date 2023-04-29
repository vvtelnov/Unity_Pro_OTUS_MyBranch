using UnityEngine;

namespace Lessons.Architecture.GameContexts
{
    public sealed class GameContextInstaller : MonoBehaviour
    {
        [SerializeField]
        private GameContext context;

        [Space]
        [SerializeField]
        private MonoBehaviour[] listeners;

        [Space]
        [SerializeField]
        private MonoBehaviour[] services;
        
        private void Awake()
        {
            foreach (var service in this.services)
            {
                this.context.AddService(service);
            }
        
            foreach (var listener in this.listeners)
            {
                this.context.AddListener(listener);
            }
        }
    }
}