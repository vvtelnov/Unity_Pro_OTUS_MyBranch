using Game.GameEngine.GameResources;
using UnityEngine;

namespace Lessons.StateMachines.States
{
    public sealed class ResourceObject : MonoBehaviour
    {
        public ResourceType resourceType;
        public int amount;
    }
}