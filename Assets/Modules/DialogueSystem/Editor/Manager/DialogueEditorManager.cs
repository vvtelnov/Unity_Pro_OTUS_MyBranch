#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.UnityEditor
{
    public static class DialogueEditorManager
    {
        public static void Load(GraphView graphView, ScriptableDialogue dialogue)
        {
            ClearGraph(graphView);

            if (dialogue == null)
            {
                Debug.LogWarning("Dialog is null!");
                return;
            }

            var nodesCache = new List<DialogueNodeView>();
            foreach (var data in dialogue.nodes)
            {
                var node = DialogueNodeView.Instantiate(data.posiition);
                node.Id = data.id;
                node.Content = data.content;

                var choices = data.choices;
                for (var i = 0; i < choices.Length; i++)
                {
                    var choice = choices[i];
                    node.AddChoice(choice);
                }

                nodesCache.Add(node);
                graphView.AddElement(node);
            }

            foreach (var data in dialogue.edges)
            {
                var outputId = data.outputId;
                var outputNode = nodesCache.First(it => it.Id == outputId);
                var outputPort = outputNode.Choices[data.outputIndex].port;

                var inputId = data.inputId;
                var inputNode = nodesCache.First(it => it.Id == inputId);

                var edge = new Edge
                {
                    output = outputPort,
                    input = inputNode.InputPort
                };

                graphView.AddElement(edge);
            }
        }

        private static void ClearGraph(GraphView graphView)
        {
            foreach (var edge in graphView.edges)
            {
                graphView.RemoveElement(edge);
            }

            foreach (var node in graphView.nodes)
            {
                graphView.RemoveElement(node);
            }
        }

        public static void Save(GraphView graphView, ScriptableDialogue dialogue)
        {
            if (dialogue == null)
            {
                Debug.LogWarning("Dialog is null!");
                return;
            }

            dialogue.nodes = ConvertNodesData(graphView);
            dialogue.edges = ConvertEdgesToData(graphView);
            EditorUtility.SetDirty(dialogue);
        }

        private static List<SerializedDialogueNode> ConvertNodesData(GraphView graphView)
        {
            var result = new List<SerializedDialogueNode>();
            var nodes = graphView.nodes;
            foreach (var view in nodes)
            {
                var nodeView = (DialogueNodeView) view;
                var data = new SerializedDialogueNode
                {
                    id = nodeView.Id,
                    content = nodeView.Content,
                    posiition = nodeView.GetPosition().center,
                    choices = ConvertChoicesToData(nodeView)
                };

                result.Add(data);
            }

            return result;
        }

        private static string[] ConvertChoicesToData(DialogueNodeView nodeView)
        {
            var choices = nodeView.Choices;
            var count = choices.Count;

            var result = new string[count];
            for (var i = 0; i < count; i++)
            {
                var choiceText = choices[i].textField.value;
                result[i] = choiceText;
            }

            return result;
        }

        private static List<SerializedDialogueEdge> ConvertEdgesToData(GraphView graphView)
        {
            var result = new List<SerializedDialogueEdge>();
            var edges = graphView.edges;
            foreach (var edge in edges)
            {
                var output = edge.output;
                var outputNode = (DialogueNodeView) output.node;
                var inputNode = (DialogueNodeView) edge.input.node;

                var data = new SerializedDialogueEdge
                {
                    inputId = inputNode.Id,
                    outputId = outputNode.Id,
                    outputIndex = outputNode.IndexOfChoice(output)
                };

                result.Add(data);
            }

            return result;
        }
    }
}
#endif