namespace DialogueSystem
{
    public sealed class DialogueTree
    {
        public string CurrentMessage
        {
            get { return this.current.message; }
        }

        public DialogueChoice[] CurrentChoices
        {
            get { return this.current.choices; }
        }

        private DialogueNode current;

        public DialogueTree(DialogueNode root)
        {
            this.current = root;
        }

        public bool MoveNext(DialogueChoice choice)
        {
            if (choice.next == null)
            {
                return false;
            }

            var node = choice.next;
            this.current = node;
            return true;
        }
    }
}