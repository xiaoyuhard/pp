using System;
using System.Collections.Generic;
using System.Data;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AppInterfaceView : View
{

     
    public Signal<string> TestButtonClickSignal = new Signal<string>();
    
    public Signal<string,float> SetVolumeSignal = new Signal<string,float>();
    
    public List<AssetReference> LevelReferences = new List<AssetReference>();
    
    [SerializeField] private GameObject cam;
    
    AudioManager audioManager;

    [SerializeField] private Animator m_animator;

    private int m_AnimatorParameters_FadeIn, m_AnimatorParameters_FadeOut;

    private Slider m_progressBar;
    private TMP_Text m_progressLabel;
    
    internal void Init()
    {
        Debug.Log("AppInterfaceView init");
    }

    protected override void Start()
    {
        m_AnimatorParameters_FadeIn = Animator.StringToHash("FadeIn");
        m_AnimatorParameters_FadeOut = Animator.StringToHash("FadeOut");
        audioManager = GetComponent<AudioManager>();
        m_progressBar = m_animator.transform.Find("ProgressBar").GetComponent<Slider>();
        m_progressLabel = m_animator.transform.Find("ProgressLabel").GetComponent<TMP_Text>();
        m_progressBar.gameObject.SetActive(false);
        m_progressLabel.gameObject.SetActive(false);
    }

    public void SetVolume(string volume)
    {
        float value = 0.00f;
        if (float.TryParse(volume, out value))
        {
            SetMasterVolume(value);
        }
    }
    
    internal void SetMasterVolume(float volume)
    {
        if (audioManager)
        {
            audioManager.SetMasterVolume(volume - 60.0f);
        }
    }

    
    /*
   public void SetBgmVolume(string strVolume)
   {
       Debug.Log("SetBgmVolume +++++++ " + strVolume);
       float volume = 0.00f;
       if (!float.TryParse(strVolume, out volume))
       {
           view.AudioManager.SetBgmVolume(volume-80.0f);
       }
       Debug.Log("SetBgmVolume ------- " + strVolume);
   }

   public void SetEventVolume(string strVolume)
   {
       Debug.Log("SetEventVolume " + strVolume);
       float volume = 0.00f;
       if (!float.TryParse(strVolume, out volume))
       {
           view.AudioManager.SetEventVolume(volume-80.0f);
       }
   }
   
   public void SetHoofsVolume(string strVolume)
   {
       Debug.Log("SetHoofsVolume " + strVolume);
       float volume;
       if (!float.TryParse(strVolume, out volume))
       {
           view.AudioManager.SetHoofsVolume(volume-80.0f);
       }
   }*/
    
#if UNITY_EDITOR  
    /*void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            TestButtonClickSignal.Dispatch(AppInterfaceMeditor.SceneInfo);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            TestButtonClickSignal.Dispatch(AppInterfaceMeditor.SceneGame);
        }
    }  */
     

    float a, b, volume;
    
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Introduction Scene", GUILayout.Height(80), GUILayout.Width(200)))
        {
            TestButtonClickSignal.Dispatch(AppInterfaceMeditor.SceneInfo);
            PlayAnimatorStart();
        }
        GUILayout.Space(40);
        if (GUILayout.Button("   Load Racing Scene   ", GUILayout.Height(80), GUILayout.Width(200)))
        {
            TestButtonClickSignal.Dispatch(AppInterfaceMeditor.SceneGame);
            PlayAnimatorStart();
        }
        GUILayout.EndHorizontal();
        //a = GUI.HorizontalSlider(new Rect (10,260,200,20),a,0,100);
        //GUI.Label(new Rect(10,290,200,50),"master vomume="+a);

        if (volume != a)
        {
            volume = a;
            SetVolumeSignal.Dispatch("SetVolume",volume);
        }

        Addressables.InitializeAsync(); 
        
        var handle = Addressables.CheckForCatalogUpdates(false);
    }
#endif   
    public void PlayAnimatorStart()
    {
       
    }
    public void PlayAnimatorFadeOut()
    {
        m_animator.SetBool(m_AnimatorParameters_FadeIn, false);
        m_animator.SetBool(m_AnimatorParameters_FadeOut, true);
    }

    public void PlayAnimatorFadeIn()
    {
        m_animator.SetBool(m_AnimatorParameters_FadeIn, true);
        m_animator.SetBool(m_AnimatorParameters_FadeOut, false);
    }

    public void ShowProgress()
    {
        m_progressBar.gameObject.SetActive(true);
        m_progressLabel.gameObject.SetActive(true);
    }
    
    public void HideProgress()
    {
        m_progressBar.gameObject.SetActive(false);
        m_progressLabel.gameObject.SetActive(false);
    }

    public void SetProgressValue(float value)
    {
         m_progressBar.value = value;
         //SetProgressLabel(value);
    }
    
    void SetProgressLabel(float value)
    {
        //m_progressLabel.text = "Size: " + $"{(value / 1214f) / 1024f:0.00}" + "MB";
        //m_progressLabel.gameObject.SetActive(value > 0);
    }

 
    
}
