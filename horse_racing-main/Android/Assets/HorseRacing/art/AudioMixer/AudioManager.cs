using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixer audioMixer; 

    public void SetMasterVolume(float volume)
    {
        if (audioMixer)
        { 
            audioMixer.SetFloat("MasterVolume", Mathf.Clamp(volume,-80,100));
        }
        else
        {
            Debug.Log("There has not audio mixer");
        }
    }

    /*
    public void SetBgmVolume(float volume)
    {
        Debug.Log("SetBgmVolume A" + volume);
        if (audioMixer)
        {
            audioMixer.SetFloat("BgmVolume", volume); 
        }
        else
        {
            Debug.Log("33 AudioManager SetBgmVolume");
        }
        
    }

    public void SetEventVolume(float volume)
    {
        Debug.Log("SetEventVolume A" + volume);
        if (audioMixer)
        {
            audioMixer.SetFloat("EventVolume", volume);
        }
        else
        {
            Debug.Log("47 AudioManager SetEventVolume");
        }
        
    }
    
    public void SetHoofsVolume(float volume)
    {
        Debug.Log("SetHoofsVolume A" + volume);
        if (audioMixer)
        {
            audioMixer.SetFloat("HoofsVolume", volume);
        }
        else
        {
            Debug.Log("61 AudioManager HoofsVolume");
        }
       
    }
    */
}