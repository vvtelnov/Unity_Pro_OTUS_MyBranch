using System;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public struct SerializedDialogueEdge
    {
        [SerializeField]
        public string outputId;

        [SerializeField]
        public string inputId;

        [SerializeField]
        public int outputIndex;
    }
}