using Entities;
using Lessons.Character.Components;
using UnityEngine;

namespace Lessons.Character
{
    [RequireComponent(typeof(CharacterModel))]
    public sealed class CharacterEntity : MonoEntityBase
    {
        private void Awake()
        {
            var characterModel = GetComponent<CharacterModel>();
            Add(new MoveInDirectionComponent(characterModel.core.move.movementDirection));
        }
    }
}