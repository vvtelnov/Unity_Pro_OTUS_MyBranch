using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.GameEngine
{
    [CreateAssetMenu(
        fileName = "DialogueConfig",
        menuName = "Dialogs/New DialogueConfig"
    )]
    public sealed class DialogueConfig : ScriptableObject
    {
        [Header("Meta")]
        [SerializeField]
        public Sprite icon;

        [Header("Graph")]
        [SerializeField]
        public string entryNodeId;

        [SerializeField]
        public List<Node> nodes;

        [SerializeField]
        public List<Edge> edges;

        public bool FindEntryNode(out Node node)
        {
            return this.FindNode(this.entryNodeId, out node);
        }

        public bool FindNextNode(string prevNode, int choiceIndex, out Node nextNode)
        {
            for (int i = 0, count = this.edges.Count; i < count; i++)
            {
                var edge = this.edges[i];
                if (edge.nodeId == prevNode && edge.choiceIndex == choiceIndex)
                {
                    if (this.FindNode(edge.nextId, out nextNode))
                    {
                        return true;
                    }
        
                    return false;
                }
            }

            nextNode = default;
            return false;
        }

        private bool FindNode(string id, out Node result)
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

        [Serializable]
        public struct Node
        {
            [SerializeField]
            public string id;

            [SerializeField]
            public string content;

            [SerializeField]
            public string[] choices;

            [SerializeField]
            public Vector2 posiition;
        }

        [Serializable]
        public struct Edge
        {
            [SerializeField, FormerlySerializedAs("outputId")]
            public string nodeId;

            [SerializeField, FormerlySerializedAs("outputIndex")]
            public int choiceIndex;

            [SerializeField, FormerlySerializedAs("inputId")]
            public string nextId;
        }
    }
}