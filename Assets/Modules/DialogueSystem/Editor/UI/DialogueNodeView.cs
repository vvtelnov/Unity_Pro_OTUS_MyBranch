#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.UnityEditor
{
    public sealed class DialogueNodeView : Node
    {
        public string Id
        {
            get { return this.idTextField.value; }
            set { this.idTextField.value = value; }
        }

        public string Content
        {
            get { return this.contentTextField.value; }
            set { this.contentTextField.value = value; }
        }

        public Port InputPort
        {
            get { return this.inputPort; }
        }

        public List<Choice> Choices
        {
            get { return this.choices; }
        }

        private TextField idTextField;

        private TextField contentTextField;

        private Port inputPort;

        private readonly List<Choice> choices = new();

        public static DialogueNodeView Instantiate(Vector2 position)
        {
            var node = new DialogueNodeView();
            node.SetPosition(new Rect(position, Vector2.zero));
            node.InitTitle();
            node.InitBody();
            node.InitButton_AddChoice();
            node.RefreshExpandedState();
            return node;
        }

        private void InitTitle()
        {
            this.inputPort = InstantiatePort(
                Orientation.Horizontal,
                Direction.Input,
                Port.Capacity.Multi,
                typeof(bool)
            );

            this.inputPort.portColor = Color.white;
            this.inputPort.portName = "";
            this.titleContainer.Insert(0, this.inputPort);

            this.idTextField = new TextField
            {
                value = "Dialog Id",
                maxLength = int.MaxValue,
                multiline = false
            };

            this.titleContainer.Insert(1, this.idTextField);
        }

        private void InitBody()
        {
            this.contentTextField = new TextField
            {
                value = "Message",
                multiline = true
            };

            var foldout = new Foldout
            {
                text = "Content"
            };

            foldout.Add(this.contentTextField);
            this.inputContainer.Insert(0, foldout);
        }

        private void InitButton_AddChoice()
        {
            var button = new Button
            {
                text = "Add Choice",
                clickable = new Clickable(() => this.AddChoice("Answer"))
            };

            this.extensionContainer.Add(button);
        }

        public Choice AddChoice(string content, bool refresh = true)
        {
            var port = InstantiatePort(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Single,
                typeof(bool)
            );
            port.portName = "";

            var deleteButton = new Button
            {
                text = "-",
                clickable = new Clickable(() => this.RemoveOutputPort(port))
            };

            var choiceText = new TextField
            {
                value = content
            };

            port.Add(deleteButton);
            port.Add(choiceText);

            this.outputContainer.Add(port);

            var result = new Choice
            {
                port = port,
                textField = choiceText
            };
            this.choices.Add(result);

            if (refresh)
            {
                this.RefreshExpandedState();
            }

            return result;
        }

        public void RemoveOutputPort(Port outputPort, bool refresh = true)
        {
            this.outputContainer.Remove(outputPort);

            var choice = this.choices.FirstOrDefault(it => it.port == outputPort);
            if (choice.port != null)
            {
                this.choices.Remove(choice);
            }

            if (refresh)
            {
                this.RefreshExpandedState();
            }
        }

        public int IndexOfChoice(Port output)
        {
            for (var i = 0; i < this.choices.Count; i++)
            {
                var choice = this.choices[i];
                if (choice.port == output)
                {
                    return i;
                }
            }

            throw new Exception("Index of port is not found!");
        }

        public struct Choice
        {
            public Port port;

            public TextField textField;
        }

        private void InitStyleSheets()
        {
            this.extensionContainer.AddToClassList("ds-node__extension-container");
            this.mainContainer.AddToClassList("ds-node__main-container");
        }
    }
}
#endif