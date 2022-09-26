using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleState : StateMachineBehaviour
{

    public int numberOfState = 4;

    public float minNormaTime = 0f;

    public float maxNormalTime = 5f;

    protected float m_RandomNormalTime;

    private readonly int m_HashRandomIdle = Animator.StringToHash("RandomIdle");
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // m_RandomNormalTime = Random.Range(minNormaTime, maxNormalTime);
        animator.SetInteger(m_HashRandomIdle,Random.Range(1,numberOfState));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if (animator.IsInTransition(0) &&
        //     animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        // {
        //     animator.SetInteger(m_HashRandomIdle,-1);
        // }
        //
        // if (stateInfo.normalizedTime > m_RandomNormalTime && !animator.IsInTransition(0))
        // {
        //     animator.SetInteger(m_HashRandomIdle,Random.Range(0,numberOfState));
        // }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     
    // }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
