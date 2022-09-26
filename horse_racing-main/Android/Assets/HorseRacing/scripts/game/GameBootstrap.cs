using System;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : ContextView
{
    private void Awake()
    {
#if UNITY_EDITOR_WIN
        LogSys.Init(new FileLogger(true));
        LogSys.Filter(LogType.Log);
#elif UNITY_STANDALONE
        LogSys.Init(new FileLogger(true));
        LogSys.Filter(LogType.Warning);
#endif    
        
    }

    void Start()
    {
        context = new GameContext(this);
    }
}