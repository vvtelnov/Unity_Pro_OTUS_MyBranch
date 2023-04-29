using Sirenix.OdinInspector;

namespace DialogueSystem
{
    public sealed class DialogueNode
    {
        [ShowInInspector, ReadOnly]
        public string Message
        {
            get { return this.message; }
        }

        public DialogueChoice[] Choices
        {
            get { return this.choices; }
        }

        internal readonly string message;

        internal readonly DialogueChoice[] choices;

        public DialogueNode(string message, params DialogueChoice[] choices)
        {
            this.message = message;
            this.choices = choices;
        }
    }
}