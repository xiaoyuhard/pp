using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using strange.extensions.context.api;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainTestCommand : MainStartupCommand
{
    [Inject] public MainRaceModel MainRaceModel { get; set; }

    override public void Execute()
    {
        
        Debug.Log("Main test command");
        
        //GetText();
        
        GetTestData();
    }

    private void GetTestData()
    {
        //Resources.Load<TextAsset>("race_info").text;
        //Resources.Load<TextAsset>("race_result").text;
        
        string str_raceInfo = Resources.Load<TextAsset>("race_info").text;
        //string str_raceInfo = File.ReadAllText(Application.dataPath + "/../StreamingAssets/race_info.json");
        RaceData rd_info = JsonUtility.FromJson<RaceData>(str_raceInfo);
        
        string str_raceResult =Resources.Load<TextAsset>("race_result").text;
        //string str_raceResult = File.ReadAllText(Application.dataPath + "/../StreamingAssets/race_result.json");
        RaceData rd_result = JsonUtility.FromJson<RaceData>(str_raceResult);

        MainRaceModel.RaceInfo = rd_info.raceInfo;
        MainRaceModel.RaceResult = rd_result.raceResult;
    }
    
    //不同平台下StreamingAssets的路径是不同的，这里需要注意一下。
    public static readonly string PathURL =
#if UNITY_ANDROID
        "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
        Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
    "file://" + Application.dataPath + "/StreamingAssets/";
#else
        string.Empty;
#endif

    IEnumerator GetText()
    {
        Debug.Log("get text");
        UnityWebRequest www = UnityWebRequest.Get("http://www.iafsucc.org/Android/race_info.json");
        www.timeout = 5;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //byte[] results = www.downloadHandler.data;
            Debug.Log(www.downloadHandler.text);
        }
    }

    
    private IEnumerable<int>  loadBundleMain(string path)
    {
        //WWW bundle = new WWW(path);
        //  yield return bundle;
        //Instantiate(bundle.assetBundle.mainAsset);
        //bundle.assetBundle.Unload(false);
        yield return 1;
    }
    
    
}

public interface IService
{
    void Request(string url);

    ReSignal Response { get; }
}
public class  ReSignal : Signal <string>{}

public class TeService
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public  GameObject contextView { get; set; }
    
    [Inject]
    public ReSignal ReSignal { get; set; }

    private string url;



    public void Request()
    {
        
    }
    
}