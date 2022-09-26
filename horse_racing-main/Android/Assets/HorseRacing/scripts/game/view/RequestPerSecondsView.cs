using System;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;


public class RequestPerSecondsView : View
{
    public ClickSignal signal_Click = new ClickSignal();
    
    [Inject] public RaceModel raceModel { get; set; }
    
    internal void Init()
    {
        //MonoBehaviour root = GetComponent<RequestPerSecondsView>();
        //root.InvokeRepeating("Request",2f,1f);
        
        
        // InvokeRepeating("Request",5f,1f);
        Invoke("Request2",1f);

    }

    private void Request()
    {
        // CancelInvoke("Request");
        if (!raceModel.RequestisBusy)
            signal_Click.Dispatch();
    }

    private void Request2()
    {
        CancelInvoke("Request");
        signal_Click.Dispatch();
    }

}