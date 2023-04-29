using System;
using DialogueSystem;
using UnityEngine;
using static Game.Meta.IDialoguePresentationModel;

namespace Game.Meta
{
    public sealed class DialoguePresentationModel : IDialoguePresentationModel
    {
        public event Action OnStateChanged;

        public event Action OnFinished;

        public Sprite Icon
        {
            get { return this.icon; }
        }

        public string CurrentMessage
        {
            get { return this.dialogue.CurrentMessage; }
        }

        public IOption[] CurrentOptions
        {
            get { return this.currentOptions; }
        }

        private readonly Sprite icon;

        private readonly DialogueTree dialogue;

        private IOption[] currentOptions;

        public DialoguePresentationModel(ScriptableDialogue config)
        {
            this.icon = config.icon;
            this.dialogue = config.InstantiateDialog();

            this.UpdateOptions();
        }

        private void UpdateOptions()
        {
            var choices = this.dialogue.CurrentChoices;
            var count = choices.Length;

            this.currentOptions = new IOption[count];
            for (var i = 0; i < count; i++)
            {
                var choice = choices[i];
                var option = new Option(this, choice);
                this.currentOptions[i] = option;
            }
        }

        private void MoveNext(DialogueChoice choice)
        {
            if (this.dialogue.MoveNext(choice))
            {
                this.UpdateOptions();
                this.OnStateChanged?.Invoke();
            }
            else
            {
                this.OnFinished?.Invoke();
            }
        }

        private sealed class Option : IOption
        {
            string IOption.Text
            {
                get { return this.choice.Text; }
            }

            private readonly DialoguePresentationModel parent;

            private readonly DialogueChoice choice;
            
            public Option(DialoguePresentationModel parent, DialogueChoice choice)
            {
                this.parent = parent;
                this.choice = choice;
            }

            void IOption.OnSelected()
            {
                this.parent.MoveNext(this.choice);
            }
        }
    }
}