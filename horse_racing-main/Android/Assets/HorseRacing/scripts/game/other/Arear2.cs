using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngineInternal;

public class Arear2 : MonoBehaviour
{
    [SerializeField] private Transform tran_cvm_ZhiDao_1;
    [SerializeField] private Transform tran_cvm_ZhiDao_2;
    [SerializeField] private Transform tran_cvm_WanDao_1;
    
    // 环形赛道半径
    private float rdd = 95.4930f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.StartsWith("horse")) return;
        setCurretn();

        //if (tran_cvm_ZhiDao_1.gameObject.activeSelf) tran_cvm_ZhiDao_1.gameObject.SetActive(false);
        //if (!tran_cvm_WanDao_1.gameObject.activeSelf) tran_cvm_WanDao_1.gameObject.SetActive(true);
        
        MoverLens sm = other.transform.GetComponent<MoverLens>();
        sm.overlens = 500;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.name.StartsWith("horse")) return;
        MoverLens sm = other.transform.GetComponent<MoverLens>();

        // 圆心
        Vector2 v2_o = new Vector2(-300,0);
        // 当前点
        Vector3 v3_p = other.gameObject.transform.position;
        // 当前点二维
        Vector2 v2_m = new Vector2(v3_p.x, v3_p.z);
        // 起始点
        Vector2 v2_n = new Vector2(-300,100);
        

        var tmp_angle = Vector2.Angle((v2_m - v2_o).normalized, (v2_n - v2_o).normalized);
        sm.curentlens = rdd *  Math.PI * (tmp_angle / 180.0f);

    }

    private void OnTriggerExit(Collider other)
    {
        
    }
    
    private void setCurretn()
    {
        tran_cvm_ZhiDao_1.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        tran_cvm_WanDao_1.GetComponent<CinemachineVirtualCamera>().Priority = 12;
        tran_cvm_ZhiDao_2.GetComponent<CinemachineVirtualCamera>().Priority = 10;
    }
}
