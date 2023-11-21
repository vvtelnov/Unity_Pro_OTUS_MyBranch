using UnityEngine;

namespace Lessons.Architecture.DI
{
    [RequireComponent(typeof(GameManager))]
    public sealed class GameManagerInstaller : MonoBehaviour 
    {
        private void Awake() 
        {
            var gameManager = this.GetComponent<GameManager>();
            var listeners = this.GetComponentsInChildren<IGameListener>();
            
            foreach (var listener in listeners)
            {
                gameManager.AddListener(listener);
            }
        }
    }
}