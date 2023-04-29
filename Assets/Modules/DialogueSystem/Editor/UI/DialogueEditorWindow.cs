#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace DialogueSystem.UnityEditor
{
    public sealed class DialogueEditorWindow : EditorWindow
    {
        private GraphView graphView;

        private ObjectField dialogField;

        [MenuItem("Window/Dialogue/Dialogue Window")]
        public static void ShowWindow()
        {
            GetWindow<DialogueEditorWindow>("Dialogue Window");
        }

        private void OnEnable()
        {
            this.InitGraphView();
            this.InitToolbar();
        }

        private void InitGraphView()
        {
            this.graphView = new DialogueGraphView();
            this.graphView.StretchToParentSize();
            this.rootVisualElement.Add(this.graphView);
        }

        private void InitToolbar()
        {
            this.dialogField = new ObjectField("Selected Dialog")
            {
                objectType = typeof(ScriptableDialogue),
                allowSceneObjects = false
            };

            var loadButton = new Button
            {
                text = "Load",
                clickable = new Clickable(() =>
                {
                    DialogueEditorManager.Load(
                        this.graphView,
                        this.dialogField.value as ScriptableDialogue
                    );
                })
            };

            var saveButton = new Button
            {
                text = "Save",
                clickable = new Clickable(() => DialogueEditorManager.Save(
                    this.graphView,
                    this.dialogField.value as ScriptableDialogue
                ))
            };

            var toolbar = new Toolbar();
            toolbar.Add(this.dialogField);
            toolbar.Add(loadButton);
            toolbar.Add(saveButton);

            this.rootVisualElement.Add(toolbar);
        }
    }
}
#endif