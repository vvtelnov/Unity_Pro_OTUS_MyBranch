using DialogueSystem;
using Game.GameEngine;
using Game.Meta;
using GameSystem;
using Sirenix.OdinInspector;

namespace Game.GameEngine
{
    public sealed class DialoguePopupShower : IDialogueShower
    {
        [GameInject]
        private PopupManager popupManager;

        [Button]
        public void ShowDialog(ScriptableDialogue dialogue)
        {
            var presenter = new DialoguePresentationModel(dialogue);
            this.popupManager.ShowPopup(PopupName.DIALOGUE, presenter);
        }
    }
}