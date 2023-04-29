// public string DialogName { get; set; }
// public List<string> Choices { get; set; }
// public string Text { get; set; }
    
    
// this.DialogName = "DSName";
// this.Choices = new List<string>();
// this.Text = "Lorem Ipsum";

// private void ProcessConnections(List<Edge> edges)
// {
//     if (edges == null)
//     {
//         return;
//     }
//     
//     foreach (var edge in edges)
//     {
//         var inputNode = (DialogueNodeView) edge.input.node;
//         var outputNode = (DialogueNodeView) edge.output.node;
//         Debug.Log($"ADD EDGE {inputNode.Id} <- {outputNode.Id}");
//     }
// }
//
// private GraphViewChange OnGraphViewChanged(GraphViewChange changes)
// {
//         
//     this.ProcessConnections(changes.edgesToCreate);
//     return changes;
// }


    
    
    // const string newChoice = "New Choice";
    // TextField choiceTField = new TextField
    // {
    //     value = newChoice
    // };

    // port.Add(choiceTField);

    // this.outputContainer.contentContainer.childCount

    // this.Choices.Add(newChoice);
    //
    //
    // this.RefreshExpandedState();


    // textField.AddToClassList("ds-node__textfield");
    // customDataContainer.AddToClassList("ds-node__custom-data-containers");

    //

    // private void InitInputPort()

    // {

    //  

    // }


    // node.InitInputPort();



    //
    // public void Draw()
    // {
    //   
    //   

    //


    //
    //
    //     Button addChoiceButton = new Button(
    //         () =>
    //         {
    //             var choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
    //                 typeof(bool));
    //
    //
    //             choicePort.portName = "";
    //
    //             Button deleteButton = new Button
    //             {
    //                 text = "X"
    //             };
    //
    //             const string newChoice = "New Choice";
    //             TextField choiceTField = new TextField
    //             {
    //                 value = newChoice
    //             };
    //
    //             choicePort.Add(choiceTField);
    //             choicePort.Add(deleteButton);
    //
    //             outputContainer.Add(choicePort);
    //             this.Choices.Add(newChoice);
    //             this.RefreshExpandedState();
    //         }
    //     )
    //     {
    //         text = "Add Choice",
    //     };
    //
    //     this.mainContainer.Insert(1, addChoiceButton);
    //
    //     foreach (var choice in this.Choices)
    //     {
    //         var choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
    //             typeof(bool));
    //
    //
    //         choicePort.portName = "";
    //
    //         Button deleteButton = new Button
    //         {
    //             text = "X"
    //         };
    //
    //         TextField choiceTField = new TextField
    //         {
    //             value = choice
    //         };
    //
    //         choicePort.Add(choiceTField);
    //         choicePort.Add(deleteButton);
    //
    //         outputContainer.Add(choicePort);
    //     }
    //
    //     this.RefreshExpandedState();
    // }
    
    // private void InitOutputPorts(){
    //     outputPortsContainer = new VisualElement
    //     {
    //         style =
    //         {
    //             flexDirection = FlexDirection.Row
    //         }
    //     };
    //     this.extensionContainer.Add(outputPortsContainer);
    //
    //     // var button1 = new Button(() => Debug.Log("Button 1 clicked"));
    //     // button1.text = "Button 1";
    //     // container.Add(button1);
    //     //
    //     // var button2 = new Button(() => Debug.Log("Button 2 clicked"));
    //     // button2.text = "Button 2";
    //     // container.Add(button2);
    //     //
    //     // var button3 = new Button(() => Debug.Log("Button 3 clicked"));
    //     // button3.text = "Button 3";
    //     // container.Add(button3);
    // }
    
    
    
//
// Button loadButton = DSElementUtility.CreateButton("Load", () => Load());
// Button clearButton = DSElementUtility.CreateButton("Clear", () => Clear());
// Button resetButton = DSElementUtility.CreateButton("Reset", () => ResetGraph());
//
// miniMapButton = DSElementUtility.CreateButton("Minimap", () => ToggleMiniMap());
//
// toolbar.Add(fileNameTextField);
// toolbar.Add(loadButton);
// toolbar.Add(clearButton);
// toolbar.Add(resetButton);
// toolbar.Add(miniMapButton);
//
// toolbar.AddStyleSheets("DialogueSystem/DSToolbarStyles.uss");

// var json = JsonUtility.ToJson(dialogGraphData);
// Debug.Log($"SAVE JSON {json}");

//
// dialog.nodes = ;
// dialog.edges = ;