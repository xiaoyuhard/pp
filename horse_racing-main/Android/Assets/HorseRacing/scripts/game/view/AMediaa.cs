using System;
using strange.extensions.mediation.impl;
using UnityEngine;

public class AMediaa : Mediator
{
    [Inject] 
    public RequestPerSecondsView view { get; set; }    
    
    [Inject] 
    public CallWebServiceSignal ws { get; set; }

    public override void OnRegister()
    {
        view.Init();
        view.signal_Click.AddListener(OnClick);
    }
    public override void OnRemove()
    {
        base.OnRemove();
    }
    
    public void OnClick()
    {
        ws.Dispatch();
    }
}
