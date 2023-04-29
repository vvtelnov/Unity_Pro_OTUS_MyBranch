#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Game.Tutorial.UnityEditor
{
    [CustomEditor(typeof(TutorialManager))]
    public sealed class TutorialManagerEditor : Editor
    {
        private SerializedProperty config;

        private TutorialManager manager;

        private void Awake()
        {
            this.config = this.serializedObject.FindProperty(nameof(this.config));
            this.manager = (TutorialManager) this.target;
        }

        public override void OnInspectorGUI()
        {
            if (EditorApplication.isPlaying)
            {
                GUI.enabled = false;
                EditorGUILayout.Toggle("Initialized", this.manager.IsInitialized);
                EditorGUILayout.Toggle("Completed", this.manager.IsCompleted);
                EditorGUILayout.EnumPopup("Current Step", this.manager.CurrentStep);
                GUI.enabled = true;
                
                EditorGUILayout.Space(8);
                if (GUILayout.Button("Move Next"))
                {
                    this.manager.FinishCurrentStep();
                    this.manager.MoveToNextStep();
                }
            }
            
            EditorGUILayout.Space(4.0f);
            EditorGUILayout.PropertyField(this.config, includeChildren: true);

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif