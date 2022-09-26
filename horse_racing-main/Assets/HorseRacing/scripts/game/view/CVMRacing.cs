using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CVMRacing : MonoBehaviour
{
    [SerializeField] private Transform tran_cvm_ZhiDao_1;
    [SerializeField] private Transform tran_cvm_ZhiDao_2;
    [SerializeField] private Transform tran_cvm_WanDao_1;


    public void SetTarget(Transform follow, Transform lookat)
    {
        tran_cvm_ZhiDao_1.GetComponent<CinemachineVirtualCamera>().Follow = follow;
        tran_cvm_ZhiDao_1.GetComponent<CinemachineVirtualCamera>().LookAt = lookat;
        
        tran_cvm_WanDao_1.GetComponent<CinemachineVirtualCamera>().LookAt = lookat;
        
        tran_cvm_ZhiDao_2.GetComponent<CinemachineVirtualCamera>().Follow = follow;
        tran_cvm_ZhiDao_2.GetComponent<CinemachineVirtualCamera>().LookAt = lookat;
    }
}
