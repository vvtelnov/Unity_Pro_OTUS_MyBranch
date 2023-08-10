using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lessons.MetaGame.Dialogs
{
    public sealed class DialogueNode : Node
    {
        public DialogueNode()
        {
            this.AddTitle();
            this.AddChoiceButton();
            this.AddInputPort();
            this.AddMessage();
            this.AddChoices();
            this.RefreshExpandedState();
        }

        private void AddTitle()
        {
            TextField title = new TextField
            {
                value = "Dialogue Title",
                multiline = false
            };
            
            title.AddToClassList("node_title");
            this.titleContainer.Insert(0, title);
        }

        private void AddChoiceButton()
        {
            var button = new Button
            {
                text = "Add Choice",
                clickable = new Clickable(this.CreateChoice),
            };

            this.mainContainer.Insert(1, button);
        }

        private void AddChoices()
        {
            for (int i = 0; i < 3; i++)
            {
                this.CreateChoice();
            }
        }

        private void AddInputPort()
        {
            Port inputPort = this.InstantiatePort(
                Orientation.Horizontal,
                Direction.Input,
                Port.Capacity.Multi,
                typeof(bool)
            );
            inputPort.portName = "Input Connection";
            this.inputContainer.Add(inputPort);
        }

        private void AddMessage()
        {
            TextField message = new TextField
            {
                value = "Dialogue Message",
                multiline = true
            };
            
            message.AddToClassList("node_message");

            this.extensionContainer.Add(message);
        }
        
        private void CreateChoice()
        {
            Port port = this.InstantiatePort(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Multi,
                typeof(bool)
            );

            port.portName = string.Empty;
            port.portColor = Color.cyan;

            TextField choiceText = new TextField
            {
                value = "Dialogue Choice",
                multiline = false
            };

            Button deleteButton = new Button
            {
                text = "X",
                clickable = new Clickable(() => this.outputContainer.Remove(port))
            };

            port.Add(choiceText);
            port.Add(deleteButton);

            this.outputContainer.Add(port);
        }
    }
}