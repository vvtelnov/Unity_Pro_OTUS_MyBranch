using Declarative;

namespace Lessons.Gameplay.States
{
    public sealed class CharacterModel : DeclarativeModel
    {
        [Section]
        public CharacterModel_Core core;

        [Section]
        public CharacterModel_View view;
    }
}