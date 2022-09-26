using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HorseSound : MonoBehaviour
{

    [SerializeField] private AudioClip _audioClip_MaTiSheng_Man;
    
    [SerializeField] private AudioClip _audioClip_MaTiSheng_Kuai;


    private NavMeshAgent _navMeshAgent;
    
    private AudioSource _AudioSource;
    private void Awake()
    {
        _AudioSource = transform.GetComponent<AudioSource>();
        _navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_navMeshAgent.speed < 42 || _AudioSource.clip == _audioClip_MaTiSheng_Kuai) return;
        _AudioSource.Stop();
        _AudioSource.clip = _audioClip_MaTiSheng_Kuai;
        if(_AudioSource.enabled) _AudioSource.Play();
    }
}
