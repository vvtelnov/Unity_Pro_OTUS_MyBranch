using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(
        fileName = "ScriptableDialogue",
        menuName = "Dialogs/New ScriptableDialogue"
    )]
    public sealed class ScriptableDialogue : ScriptableObject
    {
        [Header("Meta")]
        [SerializeField]
        public Sprite icon;

        [Header("Graph")]
        [SerializeField]
        public string entryNodeId;

        [SerializeField]
        public List<SerializedDialogueNode> nodes;

        [SerializeField]
        public List<SerializedDialogueEdge> edges;

        public DialogueTree InstantiateDialog()
        {
            if (!this.FindEntryNode(out var nodeData))
            {
                throw new Exception("Entry point is absent!");
            }

            var node = new DialogueNode(
                message: nodeData.content,
                choices: this.CreateChoices(nodeData.id, nodeData.choices)
            );
            return new DialogueTree(node);
        }
        
        private bool FindEntryNode(out SerializedDialogueNode node)
        {
            return this.FindNode(this.entryNodeId, out node);
        }

        private bool FindNode(string id, out SerializedDialogueNode result)
        {
            foreach (var node in this.nodes)
            {
                if (node.id == id)
                {
                    result = node;
                    return true;
                }
            }

            result = default;
            return false;
        }

        private DialogueChoice[] CreateChoices(string nodeId, string[] choiceTexts)
        {
            var count = choiceTexts.Length;
            var result = new DialogueChoice[count];
            for (var i = 0; i < count; i++)
            {
                var choiceText = choiceTexts[i];
                var choice = new DialogueChoice(
                    text: choiceText,
                    next: this.FindNextNode(nodeId, i)
                );
                result[i] = choice;
            }

            return result;
        }

        private DialogueNode FindNextNode(string sourceId, int choiceIndex)
        {
            for (int i = 0, count = this.edges.Count; i < count; i++)
            {
                var edge = this.edges[i];
                if (edge.outputId == sourceId && edge.outputIndex == choiceIndex)
                {
                    if (this.FindNode(edge.inputId, out var data))
                    {
                        return new DialogueNode(
                            message: data.content,
                            choices: this.CreateChoices(data.id, data.choices)
                        );
                    }

                    return null;
                }
            }

            return null;
        }
    }
}