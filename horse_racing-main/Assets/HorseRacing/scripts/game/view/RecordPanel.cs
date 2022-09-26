using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPanel : MonoBehaviour
{
    public Sprite[] sprites;

    [SerializeField] private Transform HorseWinner;
    
    private GameObject recordTable;
    
    void Awake()
    {
        recordTable = transform.Find("RecordTable").gameObject;
    }

    // Update is called once per frame
    internal void UpdateDate(SortItem[] rankingData)
    {
        RecordTableItem[] tableItems = recordTable.GetComponentsInChildren<RecordTableItem>();
        for (int i = 0; i < tableItems.Length; i++)
        {
            tableItems[i].UpdateDate(Convert.ToString(rankingData[i].serialNumber),
                 rankingData[i].name, sprites[rankingData[i].dress - 1]);
            
        }
        
        transform.GetComponent<AudioSource>().Play();

        changeA(rankingData[0]);
    }

    private void changeA(SortItem sortItem)
    {
        
    }
}
