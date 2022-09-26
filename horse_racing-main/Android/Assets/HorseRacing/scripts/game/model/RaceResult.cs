﻿//如果好用，请收藏地址，帮忙分享。
using System;
using System.Collections.Generic;

[Serializable]
public class RaceData
{
    /// <summary>
    /// 比赛结果
    /// </summary>
    public RaceResult raceResult;
    /// <summary>
    /// 赛事信息
    /// </summary>
    public RaceInfo raceInfo;
}

[Serializable]
public class RaceResult
{
    // public string matchName;
    /// <summary>
    /// 期号；字符串。如：21042700。
    /// </summary>
    public int phase;

    /// <summary>
    /// 
    /// </summary>
    public HorseItem[] horses;
    
}

[Serializable]
public class Racecourse
{
    /// <summary>
    /// 赛马场名称；字符串。如：月球背面奥体中心赛马场。
    /// </summary>
    public string name;
   
    /// <summary>
    /// 赛道长度；数字。如：1200。
    /// </summary>
    public int length;
   
    /// <summary>
    /// 赛道类型；数字。如：1，草地；2，泥地。
    /// </summary>
    public int trackType;

    public void Destory()
    {
        name = string.Empty;
    }
}

public class RaceHorses
{
   
    /// <summary>
    /// 马匹名称；字符串。如：汗血宝马。
    /// </summary>
    public string name;
    
    /// <summary>
    /// 马匹编号；字符串。如：HanXueBaoMa。
    /// </summary>
    public int serialNumber;

    /// <summary>
    /// 马排位号
    /// </summary>
    public int rowNum;
    
    /// <summary>
    /// 骑师和马的外观
    /// </summary>
    public Appearance appearance;
}

[Serializable]
public class RaceResultHorsesItem : RaceHorses
{
    /// <summary>
    /// 比赛排名
    /// </summary>
    public int rank;
    
    /// <summary>
    /// 
    /// </summary>
    public float[] distances;
}


[Serializable]
public class RaceInfo
{
    /// <summary>
    /// 比赛名 
    /// </summary>
    public string matchName;
    
    /// <summary>
    /// 期号；字符串。如：21042701。
    /// </summary>
    public int phase;
   
    /// <summary>
    /// 
    /// </summary>
    public Racecourse racecourse;
    
    /// <summary>
    /// 
    /// </summary>
    public List<HorseItem> horses = new List<HorseItem>();
    
    public void Destroy()
    {
        phase = 0;
        matchName = string.Empty;
        racecourse.Destory();
        racecourse = null;
    }
}

[Serializable]
public class Record
{
    /// <summary>
    /// 最近比赛次数；数字；取值范围[0,20]。如：0,1,2...20。
    /// </summary>
    public int matchTimes;
    
    /// <summary>
    /// 最近比赛次数中取得第一名的次数；数字；取值范围[0,20]。如：0,1,2...20。
    /// </summary>
    public int first;
    
    /// <summary>
    /// 最近比赛次数中取得第二名的次数；数字；取值范围[0,20]。如：0,1,2...20。
    /// </summary>
    public int second;
    
    /// <summary>
    /// 最近比赛次数中取得第三名的次数；数字。如：0,1,2...20。
    /// </summary>
    public int third;

    public string ToString()
    {
        return string.Format("{0}-{1}-{2}", first, second, third);
    }
}

[Serializable]
public class HorseItem //: RaceHorses
{ 
    /// <summary>
    /// 马匹名称；字符串。如：汗血宝马。
    /// </summary>
    public string name;
    
    /// <summary>
    /// 马匹编号；字符串。如：HanXueBaoMa。
    /// </summary>
    public int serialNumber;

    /// <summary>
    /// 马排位号
    /// </summary>
    public int rowNum;
    
    /// <summary>
    /// 骑师和马的外观
    /// </summary>
    public Appearance appearance;
    
    /// <summary>
    /// 速度级别；数字；取值范围[1,5]。如：1。
    /// </summary>
    public int speedLevel;
    
    /// <summary>
    /// 耐力级别；数字；取值范围[1,5]。如：1
    /// </summary>
    public int enduranceLevel;
    
    /// <summary>
    /// 比赛历时记录
    /// </summary>
    public Record record;
    
    /// <summary>
    /// 比赛排名
    /// </summary>
    public int rank;
    
    /// <summary>
    /// 距离终点的点位
    /// </summary>
    public int[] distances;
}

[Serializable]
public class Appearance
{
    public Horse horse;
    public Jockey jockey;
}

[Serializable]
public class Horse
{
    public int skin;
}
[Serializable]
public class Jockey
{
    public int dress;
}