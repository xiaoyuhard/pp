using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndPostionReduceSpeed : MonoBehaviour
{
    public Transform EndPostion;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.StartsWith("horse")) return;
        NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
        agent.destination = EndPostion.position;

        HorseItemView horseItemView = other.gameObject.GetComponent<HorseItemView>();
        horseItemView.isPassEnd = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.name.StartsWith("horse")) return;
        NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 0;
        agent.autoBraking = true;
    }
}
