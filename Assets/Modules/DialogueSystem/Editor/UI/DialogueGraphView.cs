#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using static DialogueSystem.UnityEditor.DialogueEditorConsts;

namespace DialogueSystem.UnityEditor
{
    public sealed class DialogueGraphView : GraphView
    {
        public DialogueGraphView()
        {
            this.InitStyles();
            this.InitManipulators();
            this.InitGridBackground();
        }

        private void InitStyles()
        {
            this.styleSheets.Add((StyleSheet) EditorGUIUtility.Load(FOLDER_PATH + "Styles/DSStyleSheet.uss"));
            this.styleSheets.Add((StyleSheet) EditorGUIUtility.Load(FOLDER_PATH + "Styles/DSNode.uss"));
        }

        private void InitManipulators()
        {
            this.AddManipulator(new ContextualMenuManipulator(this.AddNode));
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
        }

        private void InitGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            foreach (var port in ports)
            {
                if (port != startPort &&
                    port.node != startPort.node &&
                    startPort.direction != port.direction)
                {
                    compatiblePorts.Add(port);
                }
            }

            return compatiblePorts;
        }

        private void AddNode(ContextualMenuPopulateEvent menuEvent)
        {
            menuEvent.menu.AppendAction("Add Node", actionEvent =>
            {
                var screenPosition = actionEvent.eventInfo.localMousePosition;
                var node = DialogueNodeView.Instantiate(screenPosition);
                this.AddElement(node);
            });
        }
    }
}
#endif



// this.AddManipulator(new ContextualMenuManipulator(this.MakeEntryNode));

// private void MakeEntryNode(ContextualMenuPopulateEvent menuEvent)
// {
//     menuEvent.menu.AppendAction("Make Entry Node", actionEvent =>
//     {
//         var screenPosition = actionEvent.eventInfo.localMousePosition;
//         var node = DialogNodeView.Instantiate(screenPosition);
//         node.SetAsEntry(true);
//         this.AddElement(node);
//     });
// }