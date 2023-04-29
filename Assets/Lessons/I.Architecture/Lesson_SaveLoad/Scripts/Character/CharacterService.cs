using Entities;
using UnityEngine;

namespace Lessons.Architecture.SaveLoad
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