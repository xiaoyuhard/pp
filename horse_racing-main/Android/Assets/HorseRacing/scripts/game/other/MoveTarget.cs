using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTarget : MonoBehaviour
{
    NavMeshAgent m_Agent;
    
    [SerializeField]
    public GameObject target;
    
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (m_Agent.isActiveAndEnabled)
            m_Agent.destination = target.transform.position;
    }
}
