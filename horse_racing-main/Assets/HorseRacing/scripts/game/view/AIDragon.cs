using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class AIDragon : MonoBehaviour
{
    public GameObject target;
    private NavMeshPath path;
    private Animator animator;
    NavMeshAgent agent;
    private float dis;
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool groundPlayer;
    bool aniBl = false;
    float oldSpeed = 0;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        float speedHorse = transform.GetComponent<HorseItemView>().allSpeed;
        bool shouldMove = speedHorse > 0.5f;
        animator.SetBool("move", shouldMove);

        animator.SetFloat("speed", shouldMove ? speedHorse : 0);
        oldSpeed = speedHorse;

        if (speedHorse > 30)
        {
            animator.speed = 1.5f;
        }

    }
    void OnDrawGizmos()
    {
        if (agent != null && agent.enabled == true)
        {
            var path = agent.path;
            // color depends on status
            Color c = Color.white;
            switch (path.status)
            {
                case UnityEngine.AI.NavMeshPathStatus.PathComplete:
                    c = Color.white;
                    break;

                case UnityEngine.AI.NavMeshPathStatus.PathInvalid:
                    c = Color.red;
                    break;

                case UnityEngine.AI.NavMeshPathStatus.PathPartial:
                    c = Color.yellow;
                    break;
            }
            // draw the path
            for (int i = 1; i < path.corners.Length; ++i)
                Debug.DrawLine(path.corners[i - 1], path.corners[i], c);
        }

    }
}
