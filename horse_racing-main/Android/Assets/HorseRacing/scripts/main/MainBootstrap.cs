using System.Diagnostics;
using strange.extensions.context.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace HorseRacing.scripts.main
{
    public class MainBootstrap : ContextView
    {
        void Start()
        {
            context = new MainContext(this);

            Debug.LogFormat("current screen {0} {1}", Screen.width, Screen.height);

            /*Debug.Log("data path");
            Debug.Log(Application.dataPath);
            
            
            Debug.Log("streaming path");
            Debug.Log(Application.streamingAssetsPath);
            
            
            Debug.Log("persistent path");
            Debug.Log(Application.persistentDataPath);*/
        }
        
       
    }
}