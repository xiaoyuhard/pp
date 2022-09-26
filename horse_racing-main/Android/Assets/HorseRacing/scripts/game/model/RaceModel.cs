using UnityEngine;

using System;
using System.Collections.Generic;
using strange.extensions.signal.impl;

public class RaceModel
{

    //[Inject]
    //public Signal ChangeCurrentRaceData_Signal { get; set; }
    
    [Inject]
    public RaceStateChanged RaceStateChanged { get; set; }
    
    [Inject]
    public RaceInfoChanged RaceInfoChanged { get; set; }


    public bool RaceStateCanChangeToIntroduction
    {
        get { return _RaceStateCanChangeToIntroduction; }
        set { _RaceStateCanChangeToIntroduction = value; }
    }


    private bool _RaceStateCanChangeToIntroduction = true;
    
    [Inject]
    public RaceResultChanged RaceResultChanged { get; set; }
    
    
    [Inject]
    public SortRankingChanged SortRankingChanged { get; set; }
    
    [Inject]
    public SortRankingWinnerChanged SortRankingWinnerChanged { get; set; }
    
    public static int TimeSamplingRate = 2;
    
    /// <summary>
    /// 数据包帧率
    /// </summary>
    public static float dataframeDeltaTime = 0.04f;

    /// <summary>
    /// 数据帧率 0.01s , 动画播放帧率 0.02s
    /// 动画播放帧率 / 数据帧率
    /// </summary>
    public static int DataAndFrameSamplingRate = 2;

    public bool isShow = true;

    public bool isRacing = true;
    
    public int CostSeconds = 1;

    public int RaceLength = 1200;

    public int MapLength = 1800;

    public bool RequestisBusy = false;

    
    public RaceResult RaceResult = new RaceResult();

    public RaceInfo RaceInfo = new RaceInfo();
    
    public SortItem[] score = new SortItem[10];

    //public Dictionary<int, SortItem[]> SortPerFrame = new Dictionary<int, SortItem[]>();

    public SortItem[] RankingData;
    
    public void CalculatRaking()
    {

        #region 计算比赛成绩

        RankingData = new SortItem[RaceInfo.horses.Count];

        for (int i = 0; i < RaceInfo.horses.Count; i++)
        {
            // Debug.Log(RaceInfo.horses[i].serialNumber + "," +
            //           RaceInfo.horses[i].distances.Length + "," +
            //           RaceInfo.horses[i].distances[RaceInfo.horses[i].distances.Length - 1]);

            SortItem siTmp = new SortItem(RaceInfo.horses[i].rowNum,
                RaceInfo.horses[i].serialNumber,
                dataframeDeltaTime * RaceInfo.horses[i].distances.Length,
                RaceInfo.horses[i].distances[RaceInfo.horses[i].distances.Length - 1],
                RaceInfo.horses[i].appearance.jockey.dress);
            
            siTmp.name = RaceInfo.horses[i].name;
            RankingData[i] = siTmp;
        }

        Array.Sort(RankingData, SortItem.Compare);
        Array.Copy(RankingData, score, 10);
        Debug.LogWarning("比赛成绩：\n" + string.Join<SortItem>("\n", score));

        SortRankingWinnerChanged.Dispatch(score);
        
        #endregion


        /*
        #region 计算每个时间片的名次

        // 找到最快的马
        int fastHorseCostTime = RaceInfo.horses[0].distances.Length;
        for (int i = 1; i < RaceInfo.horses.Count; i++)
        {
            var tmpHor = RaceInfo.horses[i];
            if (tmpHor.distances.Length < fastHorseCostTime)
                fastHorseCostTime = tmpHor.distances.Length; 
        }

        HorseItem horseItemTmp;

        double ln = Math.Floor(fastHorseCostTime / 2f);
        for (int i = 0; i < ln; i++)
        {
            SortItem[] sortItemTmp = new SortItem[RaceInfo.horses.Count];
            for (int j = 0; j < RaceInfo.horses.Count; j++)
            {
                horseItemTmp = RaceInfo.horses[j];
                sortItemTmp[j] = new SortItem(horseItemTmp.rowNum, horseItemTmp.serialNumber, i, 
                    0.01f * horseItemTmp.distances[i+2], horseItemTmp.appearance.jockey.dress);
                sortItemTmp[j].name = horseItemTmp.name;
            }

            Array.Sort(sortItemTmp, SortItem.Compare);
            SortPerFrame.Add(i, sortItemTmp);
        }
        horseItemTmp = null;
        #endregion
        foreach (var a in SortPerFrame)
        {
            Debug.Log(a.Key + "-> \n" + string.Join<SortItem>("\n", a.Value));
        }*/

    }

    // public void UpdateRankingSortHorsePerFrame(int frameIndex)
    // {
    //     SortRankingChanged.Dispatch(SortPerFrame[frameIndex]);
    // }

    public void Reset()
    {
        //SortPerFrame.Clear();
        RankingData = null;
        RaceResult = new RaceResult();
        RaceInfo = new RaceInfo();
    }

}


public class SortItem
{
    public int rowNum;
    public int serialNumber;
    public float time;
    /// <summary>
    /// 单位 m
    /// </summary>
    public double len;
    public int dress;

    public string name;

    public SortItem()
    {

    }

    public SortItem(int rowNum, int serialNumber, float time, double len, int dress)
    {
        this.rowNum = rowNum;
        this.serialNumber = serialNumber;
        this.time = time;
        this.len = len;
        this.dress = dress;
    }

    public override string ToString()
    {
        // return "rowNum:" + rowNum + " time:" + time + " len:" + len;
        return rowNum + "," + serialNumber + "," + time + "," + len + "," + dress;
    }

    public string TimeToString()
    {
        return string.Format("{0}:{1}.{2}", (int) (time / 3600), (time % 60).ToString("00.0"));
    }

    public static int Compare(SortItem a, SortItem b)
    {
        if (a.time == b.time)
        {
            return a.len < b.len ? 1 : -1;
        }

        return a.time < b.time ? -1 : 1;
    }
}
