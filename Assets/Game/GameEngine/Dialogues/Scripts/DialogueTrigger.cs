using DialogueSystem;
using UnityEngine;

namespace Game.GameEngine
{
    [RequireComponent(typeof(Collider))]
    public sealed class DialogueTrigger : MonoBehaviour
    {
        public ScriptableDialogue Dialogue
        {
            get { return this.dialogue; }
        }

        [SerializeField]
        private ScriptableDialogue dialogue;
    }
}