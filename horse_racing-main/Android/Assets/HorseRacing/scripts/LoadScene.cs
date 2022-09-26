using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void Start()
    {
        AddScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddScene(int id)
    {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
