using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChageCamera : MonoBehaviour
{
    private GameObject endPointCamera;
    public Camera main;
    public RenderTexture renderTexture;

    private void Start()
    {
        endPointCamera = GameObject.Find("Main Camera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (endPointCamera != null)
        {
            endPointCamera.SetActive(true);
            ChangeMainCameraRederTexture();
        }
    }

    void ChangeMainCameraRederTexture()
    {
        main.targetTexture = renderTexture;
    }
}
