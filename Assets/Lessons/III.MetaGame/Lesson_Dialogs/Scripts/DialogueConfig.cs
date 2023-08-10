using System.Collections.Generic;
using UnityEngine;

namespace Lessons.MetaGame.Dialogs
{
    [CreateAssetMenu(
        fileName = "DialogueConfig",
        menuName = "Lessons/New DialogueConfig"
    )]
    public sealed class DialogueConfig : ScriptableObject
    {
        public string startNodeId;
        public List<DialogueNodeSerialized> nodes;
        public List<DialogueEdgeSerialzied> edges;
    }
}