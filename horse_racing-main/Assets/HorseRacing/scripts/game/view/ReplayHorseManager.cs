using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayHorseManager : MonoBehaviour
{
    public static ReplayHorseManager Singleton;
    public int recordRate = 120;
    public Action<float> OnReplayTimeChange;
    public Action OnReplayStart;
    public bool isRecording = false;

    private float _startTime;

    void Awake()
    {
        if (ReplayHorseManager.Singleton == null)
        {
            ReplayHorseManager.Singleton = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        _startTime = Time.time;
        isRecording = true;
        isPlaying = false;
    }
    public float GetCurrentTime()
    {
        return Time.time - _startTime;
    }
    float t = 0;
    public bool isPlaying = false;
    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            t += Time.deltaTime * Time.timeScale;
            OnReplayTimeChange(t);

        }
    }
}
