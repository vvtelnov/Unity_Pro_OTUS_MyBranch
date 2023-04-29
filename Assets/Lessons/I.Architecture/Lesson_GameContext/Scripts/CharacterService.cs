using Entities;
using UnityEngine;

namespace Lessons.Architecture.GameContexts
{
    public sealed class CharacterService : MonoBehaviour
    {
        [SerializeField]
        private MonoEntity character;

        public IEntity GetCharacter()
        {
            return this.character;
        }
    }
}