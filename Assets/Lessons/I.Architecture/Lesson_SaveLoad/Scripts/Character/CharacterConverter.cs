using Entities;
using Game.GameEngine.Mechanics;

namespace Lessons.Architecture.SaveLoad
{
    public static class CharacterConverter 
    {
        public static void SetupStats(IEntity character, CharacterData data)
        {
            character.Get<IComponent_SetMaxHitPoints>().SetMaxHitPoints(data.maxHitPoints);
            character.Get<IComponent_SetHitPoints>().SetHitPoints(data.currentHitPoints);
            character.Get<IComponent_SetMeleeDamage>().SetDamage(data.meleeDamage);
            character.Get<IComponent_SetMoveSpeed>().SetSpeed(data.moveSpeed);
        }

        public static CharacterData ConvertToData(IEntity character)
        {
            return new CharacterData
            {
                currentHitPoints = character.Get<IComponent_GetHitPoints>().HitPoints,
                maxHitPoints = character.Get<IComponent_GetMaxHitPoints>().MaxHitPoints,
                meleeDamage = character.Get<IComponent_GetMeleeDamage>().Damage,
                moveSpeed = character.Get<IComponent_GetMoveSpeed>().Speed
            };
        }
    }
}