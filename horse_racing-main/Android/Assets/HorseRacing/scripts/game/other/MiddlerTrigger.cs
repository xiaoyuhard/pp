using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiddlerTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] Targets_1200m;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.StartsWith("horse")) return;
        NavMeshAgent _agent = other.transform.GetComponent<NavMeshAgent>();
        HorseItemView _horseItemView = other.transform.GetComponent<HorseItemView>();
        _agent.destination = Targets_1200m[_horseItemView.horseItem.rowNum-1].transform.position;
    }
}
