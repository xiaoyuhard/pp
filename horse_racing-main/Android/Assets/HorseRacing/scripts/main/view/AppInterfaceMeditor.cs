using System;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class AppInterfaceMeditor : Mediator
{

    public const string SceneInfo = "info";
    public const string SceneGame = "game";
    
    SceneInstance m_LoadedScene;
    
    bool m_ReadyToLoad = true;
    
    string addressToAdd;
    
    [Inject] public AppInterfaceView view { get; set; }

    [Inject] public MainRaceModel MainRaceModel { get; set; }

    public override void OnRegister()
    {
        view.Init();
        view.TestButtonClickSignal.AddListener(ChangeScene);
        view.SetVolumeSignal.AddListener(OnVolumeChanged);
    }

    public bool SetResultData(string resultData)
    {
        Debug.Log("SetResultData");//Debug.Log(resultData);
        try
        {
            var raceData = JsonUtility.FromJson<RaceData>(resultData);
            Debug.LogFormat("data result {0} # data result {1}",
                raceData.raceInfo.phase, raceData.raceResult.phase);

            MainRaceModel.RaceResult = raceData.raceResult;
        }
        catch (ArgumentException argEx)
        {
            Debug.LogError(argEx.Message);
            return false;
        }

        return true;
    }

    public bool SetInfoData(string infoData)
    {
        Debug.Log("SetInfoData");//Debug.Log(infoData);
        try
        {
            var raceData = JsonUtility.FromJson<RaceData>(infoData);

            Debug.LogFormat("data info {0} # data result {1} ",
                raceData.raceInfo.phase, raceData.raceResult.phase);

            MainRaceModel.RaceInfo = raceData.raceInfo;
        }
        catch (ArgumentException argEx)
        {
            Debug.LogError(argEx.Message);
            return false;
        }

        return true;
    }

    public void SetMasterVolume(string volume)
    {
        Debug.Log("to here 2 +++");
    }

    public void ChangeScene(string sceneName)
    {
        Debug.Log("ChangeScene");
        switch (sceneName)
        {
            case SceneInfo:
                UnloadScene();
                StartDownloadScene(0);
                //LoadScene("Info");
                break;
            case SceneGame:
                UnloadScene();
                StartDownloadScene(1);
                //LoadScene("Game");
                break;
            default:
                Debug.Log("has no scene" + sceneName);
                break;
        }
    }


    public async void Lod()
    {
        //await targetToLoaded.InstantiateAsync().Task;

        //await targetToLoaded.LoadSceneAsync().Task;
    }

    public void LoadScene(string sceneName)
    {
        // string activeSceneName = SceneManager.GetActiveScene().name;
        // if (activeSceneName != "main")
        // {
        //     SceneManager.UnloadSceneAsync(activeSceneName);
        // }
        //Debug.Log(SceneManager.GetAllScenes());

        if (m_ReadyToLoad)
        {
            view.PlayAnimatorFadeIn();
            Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).Completed += OnSceneLoaded;
            m_ReadyToLoad = false;
        }
        else
        {
            //Addressables.UnloadSceneAsync(m_LoadedScene).Completed += OnSceneUnloaded;
        }
        
        
        
        
        
        // int count = SceneManager.sceneCount;
        // if (count > 1)
        // {
        //     var scene = SceneManager.GetSceneAt(1);
        //     SceneManager.UnloadSceneAsync(scene);
        // }

        // SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    // public void UnloadScene(string sceneName)
    // {
    //     if (sceneName != null)
    //         SceneManager.UnloadSceneAsync(sceneName);
    // }
    
    void OnSceneUnloaded(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            m_ReadyToLoad = true;
            m_LoadedScene = new SceneInstance();
            addressToAdd = string.Empty;
        }
        else
        {
            Debug.LogError("Failed to unload scene at address: ");
        }
    }

    void OnSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            m_LoadedScene = obj.Result;
            m_ReadyToLoad = false;
            addressToAdd = m_LoadedScene.Scene.name;
        }
        else
        {
            Debug.LogError("Failed to load scene at address: ");
        }
    }

    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }
    //
    //
    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }


    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     //SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.name));
    //     Debug.Log("SceneName = " + scene.name);
    //     Debug.Log("ActiveScene = " + SceneManager.GetActiveScene().name);
    //     //CurrentScene = scene;Debug.Log("here");
    //     // if (scene.name != "main")
    //     //     cam.SetActive(false);
    // }

    void OnVolumeChanged(string type, float volume)
    {
        switch (type)
        {
            case "SetVolume":
                view.SetMasterVolume(volume);
                break;
            case "SetBgmVolume":
                //view.AudioManager.SetBgmVolume(volume);
                break;
            case "SetEventVolume":
                //view.AudioManager.SetEventVolume(volume);
                break;
            case "SetHoofsVolume":
                //view.AudioManager.SetHoofsVolume(volume);
                break;
        }
    }


    public AsyncOperationHandle<SceneInstance> handle;
    
    private int sceneDownloaded = 0;

    public void StartDownloadScene(int index)
    {
        StartCoroutine(DownloadScene(index));
    }
    
    IEnumerator DownloadScene(int index)
    {
        view.PlayAnimatorFadeIn();
        var downloadScene = Addressables.DownloadDependenciesAsync(view.LevelReferences[index], false);
        downloadScene.Completed += SceneDownloadComplete;
        sceneDownloaded = index;

        while (!downloadScene.IsDone)
        {
            var status = downloadScene.GetDownloadStatus();
            float progress = status.Percent;
            view.ShowProgress();
            view.SetProgressValue(progress);
            //_UIManager.ShowDownloadPanel(true, index, progress);
            yield return null;
        }
    }
    
    private void SceneDownloadComplete(AsyncOperationHandle obj)
    {
        var loadScene = Addressables.LoadSceneAsync(view.LevelReferences[sceneDownloaded], LoadSceneMode.Additive);
        loadScene.Completed += obj =>
        {
            handle = obj;
            view.HideProgress();
            view.PlayAnimatorFadeOut();
        };
    }

    public void UnloadScene()
    {
        if (!handle.IsValid())
            return;
        
        Addressables.UnloadSceneAsync(handle, true).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Successfully unloaded scene.");
                //_UIManager.OnExitLevel();
            }
        };
    }
}
