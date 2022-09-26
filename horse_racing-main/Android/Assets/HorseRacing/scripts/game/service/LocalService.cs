using System.Collections;
using System.IO;
using System.Text;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.Networking;

public class LocalService : IWebService
{
    [Inject(ContextKeys.CONTEXT_VIEW)] 
    public GameObject ContextView { get; set; }
    
    [Inject]
    public RaceModel RaceModel { get; set; }
    
    [Inject] 
    public CallbackWebServiceSignal Signal { get; set; }
    
//#if UNITY_EDITOR
//    string filePath = Application.dataPath + "/../StreamingAssets/";
//#else
    string filePath = Application.dataPath + "/StreamingAssets/";
//#endif
    private string url;

    public void Request()
    {
        this.url = filePath + "race_info.json";
        //this.url = filePath + "race_result.json";
        MonoBehaviour root = ContextView.GetComponent<GameBootstrap>();
        root.StartCoroutine(GetRequest());
    }
    
    private IEnumerator GetRequest()
    {
        var responseData = File.ReadAllText(url, Encoding.UTF8);
        
        string str_raceInfo = File.ReadAllText(filePath + "race_info.json",
            Encoding.UTF8);
        RaceData rd_info = JsonUtility.FromJson<RaceData>(str_raceInfo);

        string str_raceResult = File.ReadAllText(filePath + "race_result.json",
            Encoding.UTF8);
        RaceData rd_result = JsonUtility.FromJson<RaceData>(str_raceResult);

        
        RaceModel.RaceInfo = rd_info.raceInfo;
        RaceModel.RaceResult = rd_result.raceResult;

        // set distance to info data
        for (int i = 0; i < rd_info.raceInfo.horses.Count; i++)
        {
            HorseItem HorseItem_info_tmp;
            HorseItem HorseItem_result_tmp;

            HorseItem_info_tmp = rd_info.raceInfo.horses[i];
            for (int j = 0; j < rd_result.raceResult.horses.Length; j++)
            {
                HorseItem_result_tmp = rd_result.raceResult.horses[j];
                if (HorseItem_info_tmp.rowNum == HorseItem_result_tmp.rowNum)
                {
                    HorseItem_info_tmp.distances = HorseItem_result_tmp.distances;
                    break;
                }
            }
        }
        
        
        
        RaceModel.RaceInfoChanged.Dispatch("introduction");
        
        yield return new WaitForSeconds(3f);
        
        RaceModel.CalculatRaking();
        //Signal.Dispatch(responseData);
        
        RaceModel.RaceInfoChanged.Dispatch("Ready");
    }

    

}