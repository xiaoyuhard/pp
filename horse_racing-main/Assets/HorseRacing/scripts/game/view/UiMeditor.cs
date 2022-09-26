using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMeditor :  Mediator
{
    
    [Inject]
    public RaceModel RaceModel { get; set; }
    
    [Inject]
    public UIView view { get; set; }
    
    
    
    private int frameIndex;

    private bool showRacing;
    
    public override void OnRegister()
    {
        view.Init();
        RaceModel.RaceInfoChanged.AddListener(onRaceInfoChanged);
        RaceModel.SortRankingChanged.AddListener(SortRankingChanged);
        //minimap.Init("nidi",RaceModel.MapLength, RaceModel.RaceLength);
        //view.SortRanking(RaceModel.SortPerFrame[0]);
        
        // var n = SimpleJSON.JSON.Parse(gameConfigTxt);
        // url = n ["url"];
        // timeout = n ["timeout"].AsInt;
    }
    
    private void onRaceInfoChanged(string state)
    {
        switch (state)
        {
            case "Ready":
                view.ChangeUIState("ReadyUI");
                showRacing = false;
                break;
            case "racing":
                frameIndex = 0;
                showRacing = true;
                view.ChangeUIState("ShowRacingUI");
                break;
            case "ShowRecord":
                view.ChangeUIState("ShowRecordUI");
                break;
            case "introduction":
                showRacing = false;
                //view.ChangeUIState("ShowRecordUI");
                break;
        }
    }

    void SortRankingChanged(SortItem[] si)
    {
        view.SortRanking(si);
    }
    /*private void Update()
    {
        if (showRacing)
        {
            //int index = (RaceModel.DataAndFrameSamplingRate * frameIndex / RaceModel.TimeSamplingRate) * RaceModel.TimeSamplingRate;
            int index = frameIndex * 2;//RaceModel.DataAndFrameSamplingRate * frameIndex;
            if (index < RaceModel.SortPerFrame.Count ) //* RaceModel.TimeSamplingRate
            {

                view.SortRanking(RaceModel.SortPerFrame[index]);
                //RaceModel.UpdateRankingSortHorsePerFrame(index);
                
                //Debug.LogFormat("{0} {1} {2}", frameIndex, index, RaceModel.SortPerFrame.Count);
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (showRacing)
        {
            frameIndex++;
        }
    }*/
}

