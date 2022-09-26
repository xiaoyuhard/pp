using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerAudioTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Play();
    }
}
