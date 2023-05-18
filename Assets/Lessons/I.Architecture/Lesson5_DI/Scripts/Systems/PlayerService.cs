using UnityEngine;

namespace Lessons.Architecture.DI
{
    public sealed class PlayerService : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        public Player GetPlayer()
        {
            return this.player;
        }
    }
}