using GameSystem;
using Sirenix.OdinInspector;

namespace Game.GameEngine
{
    public sealed class DialogueModule : GameModule
    {
        [GameService]
        [ShowInInspector]
        private DialoguePopupShower shower = new();
    }
}