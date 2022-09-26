using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

public class ShowHorseCommand : Command
{
    //[Inject(ContextKeys.CONTEXT_VIEW)] 
    //public GameObject ContextView { get; set; }

    public override void Execute()    
    {
        // GameObject go = new GameObject();
        // go.name = "ShowHorseView";
        // go.AddComponent<ShowHorseView>();
        // go.transform.parent = ContextView.transform;
    }
}