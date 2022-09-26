using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallWebServiceCommand : Command
{
    [Inject]
    public IWebService Service { get; set; }
    
    [Inject] 
    public RaceModel RaceModel { get; set; }

    private string response_cache = "";
    
    public override void Execute()
    {
        Retain();
        Service.Signal.AddListener(onComplete);
        Service.Request();
    }
    
    private void onComplete(string response_data)
    {
        Service.Signal.RemoveListener(onComplete);
        if (response_data == "{}")
        {
            Debug.Log("等待服务器返回数据.");
        }
        else
        {
            Debug.LogFormat("{0} parse json data ",DateTime.Now.ToString());
            var raceData = JsonUtility.FromJson<RaceData>(response_data);
            Debug.LogFormat("data info {0} # data result {1} # model info {2} # model result {3}",
                raceData.raceInfo.phase,raceData.raceResult.phase, RaceModel.RaceInfo.phase, RaceModel.RaceResult.phase);

            // var a = raceData.raceResult.phase == RaceModel.RaceResult.phase;

            if (raceData.raceResult.phase > RaceModel.RaceResult.phase )
            {
                if (RaceModel.RaceInfo.phase == RaceModel.RaceResult.phase && RaceModel.RaceInfo.phase == 0)
                {
                    Debug.LogFormat("{0} 期已经开赛，等待下一期 {1}", 
                        raceData.raceResult.phase, raceData.raceInfo.phase);
                }
                else
                {
                    RaceModel.RaceResult = raceData.raceResult;
                    foreach (var resultHorseData in RaceModel.RaceResult.horses)
                    {
                        foreach (var inforHorseData in RaceModel.RaceInfo.horses)
                        {
                            if (resultHorseData.rowNum == inforHorseData.rowNum)
                            {
                                // inforHorseData.distances.Length = 0;
                                inforHorseData.distances = resultHorseData.distances;
                            }
                        }
                    }
                    RaceModel.CalculatRaking();
                    
                    Debug.Log("racing  ..... ");
                    RaceModel.RaceInfoChanged.Dispatch("Ready");
                }
                
            }
            else if (raceData.raceInfo.phase > RaceModel.RaceInfo.phase && raceData.raceResult.phase <= 0 && RaceModel.RaceStateCanChangeToIntroduction)
            {
                RaceModel.Reset();
                RaceModel.RaceInfo = raceData.raceInfo;
                Debug.Log("introduction  ..... ");
                RaceModel.RaceInfoChanged.Dispatch("introduction");
            }
            else if (raceData.raceInfo.phase > RaceModel.RaceInfo.phase && RaceModel.RaceInfo.phase == RaceModel.RaceResult.phase)
            {
                Debug.LogFormat("{0} 期比赛进行中，下一期是 {1}",raceData.raceResult.phase,raceData.raceInfo.phase);
            }
        }
        Release();
    }
}

public static class JsonHelper11
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}