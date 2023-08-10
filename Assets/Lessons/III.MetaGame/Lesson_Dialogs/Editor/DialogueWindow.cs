#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;

namespace Lessons.MetaGame.Dialogs
{
    public sealed class DialogueWindow : EditorWindow
    {
        private DialogueGraph graphView;
        
        [MenuItem("Lessons/Dialogue Window")]
        public static void ShowWindow()
        {
            GetWindow<DialogueWindow>("Dialogue Window");
        }

        private void OnEnable()
        {
            this.AddGraphView();
        }

        private void AddGraphView()
        {
            this.graphView = new DialogueGraph();
            this.graphView.StretchToParentSize();
            this.rootVisualElement.Add(this.graphView);
        }
    }
}
#endif