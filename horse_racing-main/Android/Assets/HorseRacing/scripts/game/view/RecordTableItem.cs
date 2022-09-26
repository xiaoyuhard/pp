using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordTableItem : MonoBehaviour
{
    private Text rankingTxt;
    private Text racesTxt;
    private Image caiyiImage;
    
    void Awake()
    {
        rankingTxt = transform.Find("1_RankingTxt").GetComponent<Text>();
        racesTxt = transform.Find("2_RacesTxt").GetComponent<Text>();
        caiyiImage = transform.Find("Image").GetComponent<Image>();
    }

    // Update is called once per frame
    internal void UpdateDate(string ranking,string races,Sprite dressSprite)
    {
        rankingTxt.text = ranking;
        racesTxt.text = races;
        caiyiImage.sprite = dressSprite;
    }
}
