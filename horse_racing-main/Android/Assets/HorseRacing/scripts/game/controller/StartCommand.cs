using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

public class StartCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)] 
    public GameObject ContextView { get; set; }
    
    
    
    [Inject] public MainRaceModel MainRaceModel { get; set; }
    
    [Inject] public RaceModel RaceModel { get; set; }

    public override void Execute()
    {
        // GameObject go = new GameObject();
        // go.name = "ExampleView";
        // go.AddComponent<RequestPerSecondsView>();
        // go.transform.parent = ContextView.transform;
        if (MainRaceModel.RaceInfo == null)
        {
            Debug.Log("Ther has noting info data !");
            return;
        }
        
        if (MainRaceModel.RaceResult == null)
        {
            Debug.Log("Ther has noting result data !");
            return;
        }

        Debug.Log("hello game");
        Debug.Log(MainRaceModel.RaceInfo.phase);
        
        RaceModel.RaceInfo = MainRaceModel.RaceInfo;
        RaceModel.RaceResult = MainRaceModel.RaceResult;
        
        RaceModel.RaceInfoChanged.Dispatch("introduction");
        RaceModel.CalculatRaking();
        RaceModel.RaceInfoChanged.Dispatch("Ready");

    }
}