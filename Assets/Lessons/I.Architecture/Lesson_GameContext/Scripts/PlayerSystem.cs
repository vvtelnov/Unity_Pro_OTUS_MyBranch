using UnityEngine;

namespace Lessons.Architecture.GameContexts
{
    public sealed class PlayerSystem : MonoBehaviour
    {
        private string playerName;
    
        public CharacterService Service { get; } 
    }
}