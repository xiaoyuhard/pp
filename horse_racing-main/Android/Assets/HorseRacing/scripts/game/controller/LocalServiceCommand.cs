using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

public class LocalServiceCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)] 
    public GameObject ContextView { get; set; }
    
    [Inject] 
    public RaceModel rm { get; set; }
    
    public override void Execute()
    {
        
    }
}
