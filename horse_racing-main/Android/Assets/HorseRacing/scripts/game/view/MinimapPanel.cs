using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.U2D;

public enum MapName
{
    caodi,
    nidi
}
public class MinimapPanel : MonoBehaviour
{
    public FramesAnimation FrameUp100;
    
    public FramesAnimation FrameUnder100;
    
    public NumberSprite numberSprite;
    
    public SpriteAtlas caodiAtlas;
    
    public SpriteAtlas nidiAtlas;
    
    CanvasGroup canvas150;
    
    CanvasGroup canvas100;
    
    CanvasGroup minmapGroup;
    
    public int MapLength;
    
    public int RaceLength;
    
    public double CurrentLength;//当前距离
    
    double percent=0;
    
  //  public float maxPercent;//百分比
    // Start is called before the first frame update
    void Start()
    {
        CurrentLength = 0;
        minmapGroup = GetComponent<CanvasGroup>();
        canvas150 = FrameUp100.GetComponent<CanvasGroup>();
        canvas100 = FrameUnder100.GetComponent<CanvasGroup>();
        //初始化传入赛道名场，赛道长度，赛事总长度
        Init("nidi", 1800, 1200);
        //UpdateMap();
    }
    
   /// <summary>
   /// 初始化方法
   /// </summary>
   /// <param name="mapName">地图类型</param>
   /// <param name="MapLength">赛道长度</param>
   /// <param name="RaceLength">赛事长度</param>
   internal void Init(string mapName,int MapLength,int RaceLength)
    {
        
        MapName map = (MapName)System.Enum.Parse(typeof(MapName), mapName);
        switch (map)
        {
            case MapName.caodi:
                FrameUp100.Init(caodiAtlas);
                FrameUnder100.Init(caodiAtlas);
                break;
            case MapName.nidi:
                FrameUp100.Init(nidiAtlas);
                FrameUnder100.Init(nidiAtlas);
                break;

        }
        this.MapLength = MapLength;
        this.RaceLength = RaceLength;
        numberSprite.CreateNumber(Convert.ToInt16(RaceLength));
        // float maxPercent = (float) RaceLength / MapLength;    
        // if (maxPercent > 1)
        // {
        //     canvas150.alpha = 1;
        // }
        // else
        // {
        //     canvas150.alpha = 0;
        // }
    }
   
    private void UpdateMap()
    {
        //CurrentLength =Mathf.FloorToInt(MapLength * percent);
        // int temp = CurrentLength- CurrentLength % 100;

        int number = 100 * Convert.ToInt16(Math.Floor((RaceLength - CurrentLength) / 100.0f));
        percent = (RaceLength - CurrentLength) / MapLength;    
        numberSprite.UpdateNumber(number);
        if (percent > 1)
        {
            canvas150.alpha = 1;
            float mapRate = (float) (RaceLength - MapLength - CurrentLength) / MapLength;
            if (mapRate <1)
                FrameUp100.SeekTo(mapRate);
        }    
        else
        {
            canvas150.alpha = 0;
            float mapRate =  (float) percent;
            if (mapRate < 1)
                FrameUnder100.SeekTo(mapRate);
        }
    }
    public void ShowMap()
    {
        minmapGroup.alpha = 1;
    }
    public void HideMap()
    {
        minmapGroup.alpha = 0;
    }
    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        //if (percent < maxPercent)
        //{
        //    percent += 0.001f;

        //}
        //if(percent>maxPercent)
        //{
        //    percent = maxPercent;
        //}
        //根据百分比更新地图
        if (gameObject.activeSelf) 
            UpdateMap();
    }
}
