namespace DialogueSystem
{
    public sealed class DialogueChoice
    {
        public string Text
        {
            get { return this.text; }
        }

        internal readonly string text;

        internal readonly DialogueNode next;

        public DialogueChoice(string text, DialogueNode next = null)
        {
            this.text = text;
            this.next = next;
        }
    }
}