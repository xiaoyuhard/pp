using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

public class MainStartupCommand : Command
{
    //[Inject(ContextKeys.CONTEXT_VIEW)]
    //public GameObject contextView { get; set; }

    [Inject] public MainRaceModel MainRaceModel { get; set; }

    override public void Execute ()
    {
       Debug.Log("run game");

       //Transform go = contextView.transform.GetChild(2);
       //GameObject go = new GameObject();
       //go.name = "AppInterfaceView";
       //go.gameObject.AddComponent<AppInterfaceView>();
       //go.transform.parent = contextView.transform;
    }

    
}
