using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseSprite
{
    //public HorsesItem horsesItem;
    public GameObject horsespriteGo;
}
public class ReadRaceInfo : MonoBehaviour
{
    public TextAsset readJson;
    public string jsonStr;
    //public RaceRoot raceResultInfo;
    //List<HorsesItem> horsesItems;
    public List<Sprite> horseSprites;
    public GameObject horseItemPrefab;
    public List<HorseSprite> horses;
    // Start is called before the first frame update
    void Start()
    {
        horses = new List<HorseSprite>();
        jsonStr = readJson.text;
        ReadInfo(jsonStr);
    }
    public void ReadInfo(string jsonText)
    {
        // raceResultInfo = JsonUtility.FromJson<RaceRoot>(jsonText);
        //horsesItems = raceResultInfo.raceResult.horses;
        //UpdateInfo();
    }
    //public void UpdateInfo()
    //{
    //    horsesItems.Sort((x, y) => -x.rank.CompareTo(y.rank));
    //    for (int i = 0; i < horsesItems.Count; i++)
    //    {
    //        int index = ContainsInfo(horsesItems[i]);
    //        Debug.Log(index);
    //        if (index == -1)
    //        {
    //            GameObject spriteGo = GameObject.Instantiate(horseItemPrefab, transform);
    //            HorseSprite horseSprite = new HorseSprite();
    //            Image image = spriteGo.GetComponent<Image>();
    //            image.sprite = horseSprites[horsesItems[i].serialNumber];
    //            Text horseNameText = spriteGo.transform.Find("horseName").GetComponent<Text>();
    //            horseNameText.text = horsesItems[i].name;
    //            Text serial= spriteGo.transform.Find("SerialNumber").GetComponent<Text>();
    //            serial.text = horsesItems[i].rank.ToString();
    //            horseSprite.horsespriteGo = spriteGo;
    //            horseSprite.horsesItem = horsesItems[i];
    //            horses.Add(horseSprite);
    //        }
    //        else
    //        {
    //            RectTransform rectTransform = horses[index].horsespriteGo.GetComponent<RectTransform>();
    //            rectTransform.SetSiblingIndex(i);
    //        }
    //    }
    //}
    //public int ContainsInfo(HorsesItem horsesItem)
    //{
    //    for(int i=0;i< horses.Count; i++)
    //    {
    //        if (horses[i].horsesItem.serialNumber == horsesItem.serialNumber)
    //            return i;
    //    }
    //    return -1;
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
