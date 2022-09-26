using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Arear3 : MonoBehaviour
{
    [SerializeField] private Transform tran_cvm_ZhiDao_1;
    [SerializeField] private Transform tran_cvm_ZhiDao_2;
    [SerializeField] private Transform tran_cvm_WanDao_1;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.StartsWith("horse")) return;
        setCurretn();
        //if (tran_cvm_WanDao_1.gameObject.activeSelf) tran_cvm_WanDao_1.gameObject.SetActive(false);
        //if (!tran_cvm_ZhiDao_2.gameObject.activeSelf) tran_cvm_ZhiDao_2.gameObject.SetActive(true);
        
        MoverLens sm = other.transform.GetComponent<MoverLens>();
        sm.overlens = 500 + 300;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.name.StartsWith("horse")) return;
        MoverLens sm = other.transform.GetComponent<MoverLens>();
        sm.curentlens = Math.Abs(other.gameObject.transform.position.x + 300);
    }

    private void OnTriggerExit(Collider other)
    {
        tran_cvm_ZhiDao_1.GetComponent<CinemachineVirtualCamera>().Priority = 12;
        tran_cvm_WanDao_1.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        tran_cvm_ZhiDao_2.GetComponent<CinemachineVirtualCamera>().Priority = 10;
    }

    private void setCurretn()
    {
        tran_cvm_ZhiDao_1.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        tran_cvm_WanDao_1.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        tran_cvm_ZhiDao_2.GetComponent<CinemachineVirtualCamera>().Priority = 12;
    }
}
