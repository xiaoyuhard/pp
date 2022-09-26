using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.impl;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowHorseMediator : Mediator
{
    [Inject]
    public ShowHorseView view { get; set; }

    [Inject]
    public RaceInfoChanged RaceInfoChanged { get; set; }

    public override void OnRegister()
    {
        RaceInfoChanged.AddListener(onRaceInfoChanged);
        view.Init();
    }

    private void onRaceInfoChanged(string state)
    {
        view.SetState(state);
    }
}
