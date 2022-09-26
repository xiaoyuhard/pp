using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingPanel : MonoBehaviour
{
    
    public Sprite[] sprites;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInfo(SortItem[] items)
    {
        // only 10 house
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = sprites[items[i].dress-1];
            transform.GetChild(i).GetChild(0).GetComponent<Text>().text = items[i].serialNumber.ToString();
        }
    }
}