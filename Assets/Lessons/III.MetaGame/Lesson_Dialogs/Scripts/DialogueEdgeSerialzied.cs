using System;

namespace Lessons.MetaGame.Dialogs
{
    [Serializable]
    public struct DialogueEdgeSerialzied
    {
        public string sourceNodeId;
        public string targetNodeId;
        public int index;
    }
}