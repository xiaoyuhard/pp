using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    public bool ready;

    [SerializeField]
    public bool moving;

    private NavMeshAgent _navMeshAgent;

    
    private Animator horse_JockeyAnimator;


    // Start is called before the first frame update
    void Start()
    {
        horse_JockeyAnimator = transform.Find("Horse_JocKey").GetComponent<Animator>();
        _navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = _navMeshAgent.speed;
        horse_JockeyAnimator.SetFloat("speed",speed);
        
        horse_JockeyAnimator.SetBool("moving",moving);

        if (ready)
        {
            horse_JockeyAnimator.SetTrigger("ready");
        }
    }
}

public class LogHelp 
{

    public void Log(string str)
    {
        Debug.LogFormat(DateTime.Now.ToString(),str);
    }
    
    

}
