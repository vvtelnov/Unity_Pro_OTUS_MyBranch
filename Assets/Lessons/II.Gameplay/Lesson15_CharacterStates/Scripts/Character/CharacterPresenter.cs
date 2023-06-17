using Entities;
using UnityEngine;

namespace Lessons.Gameplay.States
{
    [RequireComponent(typeof(CharacterModel))]
    public sealed class CharacterPresenter : MonoEntityBase
    {
        private void Awake()
        {
            var heroModel = this.GetComponent<CharacterModel>();
            this.Add(new MoveComponent(heroModel.core.move.onMove));
            this.Add(new DeathComponent(heroModel.core.life.isDeath));
        }
    }
}