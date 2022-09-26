using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScriptSequence : MonoBehaviour
{
    // Update is called once per frame
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod ()
    {
        Debug.Log("Before scene loaded");
    }

    void Awake()
    {
        Debug.Log("Awake");
    }
    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        Debug.Log("After scene loaded");
    }

    //[RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("RuntimeMethodLoad: After scene loaded");
    }

    void Start()
    {
        Debug.Log("Start");
    }

}
