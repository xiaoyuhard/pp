using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class RacingView : View
{
    
    [Inject]
    public RaceModel RaceModel { get; set; }

    [SerializeField]
    public GameObject horseprefab;

    [SerializeField]
    Transform horseParent;
    
    [SerializeField]
    Transform moveTargets;
    
    [SerializeField]
    Transform cvmTrans;

    private CinemachineVirtualCamera cvm;
    
    private void Start()
    {
        horseParent = transform.GetChild(0).GetChild(1);
        cvm = cvmTrans.GetComponent<CinemachineVirtualCamera>();
    }
    
    internal void Init()
    {
        
    }
    
    internal void SetHorses()
    {
        if (RaceModel.RaceResult.horses.Length > 0)
        {
            for (int i = horseParent.childCount - 1; i >= 0; i--)
            {
                Destroy(horseParent.GetChild(i));
            }

            float speedmax = .0f;
            for (int i = 0; i < RaceModel.RaceResult.horses.Length; i++)
            {
                var dis = RaceModel.RaceResult.horses[i].distances;
                var len = dis[dis.Length - 1];
                var t = dis.Length / 100;
                float speed = len / t;
                
                var horse = createHorse("horse (" + i + ")", horseParent, 
                    moveTargets.GetChild(i).gameObject.transform,
                     i * 1, speed);
                
                var chi = horseParent.GetChild(i);
               
                if (speedmax < speed)
                {
                    speedmax = speed;    
                    cvm.Follow = chi.transform;
                    cvm.LookAt = chi.transform;
                }
            }
        }
    }

    private GameObject createHorse(string name, Transform horseParent, Transform moveTarget, float x, float speed)
    {
        GameObject horse = Instantiate(horseprefab, new Vector3(.0f,.0f,.0f), 
            Quaternion.AngleAxis(-90,Vector3.up)) as GameObject;
        horse.name = name;
        horse.transform.parent = horseParent;
        horse.transform.localPosition = new Vector3(0, 0, x);
        horse.GetComponent<NavMeshAgent>().speed = speed;
        horse.GetComponent<NavMeshAgent>().enabled = true;
        horse.GetComponent<NavMeshAgent>().destination = moveTarget.position;
        //horse.GetComponent<MoveTarget>().target = moveTarget;

        return horse;
    }
}
