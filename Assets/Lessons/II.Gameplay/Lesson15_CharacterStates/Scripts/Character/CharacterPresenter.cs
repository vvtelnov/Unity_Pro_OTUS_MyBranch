using Entities;
using UnityEngine;

namespace Lessons.Gameplay.States
{
    [RequireComponent(typeof(CharacterModel))]
    public sealed class CharacterPresenter : MonoEntityBase
    {
        private void Awake()
        {
            var characterModel = this.GetComponent<CharacterModel>();
            this.Add(new MoveComponent(characterModel.core.move.onMove));
            this.Add(new DeathComponent(characterModel.core.life.isDeath));
        }
    }
}