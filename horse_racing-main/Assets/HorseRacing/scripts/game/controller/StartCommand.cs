using System.Collections;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)] 
    public GameObject ContextView { get; set; }

    public override void Execute()
    {
        // GameObject go = new GameObject();
        // go.name = "ExampleView";
        // go.AddComponent<RequestPerSecondsView>();
        // go.transform.parent = ContextView.transform;
    }
}