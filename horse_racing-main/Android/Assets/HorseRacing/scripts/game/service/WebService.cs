using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.Networking;

public class WebService : IWebService
{
    [Inject(ContextKeys.CONTEXT_VIEW)] 
    public GameObject ContextView { get; set; }
    [Inject] 
    public CallbackWebServiceSignal Signal { get; set; }

    [Inject] public RaceModel raceModel { get; set; }
    
    [Inject]
    public IGameConfig gameConfig{ get; set; }

    private Dictionary<int,DateTime> logtime = new Dictionary<int,DateTime>();

    public void Request()
    {
        MonoBehaviour root = ContextView.GetComponent<GameBootstrap>();
        root.StartCoroutine(GetRequest());
    }
    
    public IEnumerator GetRequest()
    {
        //DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(System.DateTime.Now);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(gameConfig.url))
        {
            webRequest.useHttpContinue = false;
            int requestId = webRequest.GetHashCode();
            // logtime.Add(requestId, startTime);
            //Debug.LogFormat("request [{0}] ", requestId);
            webRequest.timeout = gameConfig.timeout;
            raceModel.RequestisBusy = true;
            yield return webRequest.SendWebRequest();
            //DateTime nowTime = TimeZone.CurrentTimeZone.ToLocalTime(System.DateTime.Now);
            //requestId = webRequest.GetHashCode();
            //DateTime startTimeOfRequest;
            //bool isSuccessfull = logtime.TryGetValue(requestId,out startTimeOfRequest);
            //double seconds = (nowTime - startTimeOfRequest).TotalSeconds;
            //Debug.LogFormat("response [{0}] ", requestId);
            
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);
                //Debug.LogWarningFormat("请求 {0} 返回时间差 {1}", requestId, seconds);
            }
            else
            {
                Signal.Dispatch(webRequest.downloadHandler.text);
            }
            raceModel.RequestisBusy = false;
        }
    }

}