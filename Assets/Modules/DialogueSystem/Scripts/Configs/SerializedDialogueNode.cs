using System;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public struct SerializedDialogueNode
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
}