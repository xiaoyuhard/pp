using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RacingMeditor : Mediator
{
    [Inject]
    public RacingView view { get; set; }
    
    [Inject]
    public RaceResultChanged RaceResultChanged { get; set; }
    
    
    public override void OnRegister()
    {
        RaceResultChanged.AddListener(onRaceResultChanged);
        view.Init();
    }
    
    private void onRaceResultChanged(bool show)
    {
        if (show)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            view.SetHorses();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    
}
