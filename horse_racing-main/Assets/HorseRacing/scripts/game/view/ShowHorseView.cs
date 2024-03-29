using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ShowHorseView : View
{
    [Inject] public RaceModel RaceModel { get; set; }

    [SerializeField] public GameObject horseprefab;

    [SerializeField] public GameObject jiXieZhaXiang;

    [SerializeField] private Transform horseParent;

    [SerializeField] private Transform[] moveTargets;

    [SerializeField] private Transform[] Targets_100m;

    [SerializeField] private Transform[] Targets_1200m; //[FormerlySerializedAs("EndTargets")] 

    [SerializeField] private List<GameObject> speedCollList;

    [SerializeField] private Transform trans_cvmRacing;

    [SerializeField] private Transform replayHorseObject;
    private Transform replayHorseManagerObject;

    [SerializeField] private Transform endTarget;


    [SerializeField] private Transform HorseWinner;

    [SerializeField] private Camera cam_ZhongDian;

    [SerializeField] private GameObject cam_Start;

    [SerializeField] private Transform tran_cvm_ZhiDao_1;

    [SerializeField] private Transform trans_RacingName;

    private WinnerHorse winnerHorse;

    private Dictionary<int, GameObject> moveer;

    bool isReplayHorse = true;

    bool isSetReplay = false;

    bool isShowCamera_ZhongDian = false;

    private Animator jiXieZhaXiangAnimator;

    private TextMeshProUGUI TMP_RacingName;
    internal void Init()
    {
        moveer = new Dictionary<int, GameObject>();

        GameObject go = new GameObject();
        go.name = "RequestPerSecondsView";
        go.AddComponent<RequestPerSecondsView>();
        go.transform.parent = transform;


        jiXieZhaXiangAnimator = jiXieZhaXiang.GetComponent<Animator>();

        winnerHorse = HorseWinner.GetComponent<WinnerHorse>();
        TMP_RacingName = trans_RacingName.GetComponent<TextMeshProUGUI>();

        RaceModel.RaceStateChanged.AddListener(onStateChanged);
        RaceModel.SortRankingWinnerChanged.AddListener(SortRankingChanged2);

        trans_cvmRacing.gameObject.SetActive(false);
        cam_ZhongDian.gameObject.SetActive(false);
    }

    void SortRankingChanged(SortItem[] si)
    {
        foreach (var rowNum in moveer.Keys)
        {
            GameObject hour = moveer[rowNum];
            HorseItemView horseItemView = hour.GetComponent<HorseItemView>();
            if (horseItemView.horseItem.rowNum == si[0].rowNum)
            {
                trans_cvmRacing.GetComponent<CVMRacing>().SetTarget(horseItemView.transform, horseItemView.transform.Find("polySurface25"));

                // 只让冠军马播放马蹄声
                hour.GetComponent<AudioSource>().enabled = false;
                hour.GetComponent<AudioSource>().mute = true;

            }
            else if (horseItemView.horseItem.rowNum % 3 == 0)
            {
                hour.GetComponent<AudioSource>().enabled = true;
                hour.GetComponent<AudioSource>().mute = false;
            }
            else
            {
                hour.GetComponent<AudioSource>().enabled = false;
                hour.GetComponent<AudioSource>().mute = true;
            }
        }


    }

    void SortRankingChanged2(SortItem[] si)
    {
        foreach (var rowNum in moveer.Keys)
        {
            // var dd = RaceModel.RaceResult.horses[rowNum - 1];
            GameObject hour = moveer[rowNum];
            HorseItemView horseItemView = hour.GetComponent<HorseItemView>();
            if (horseItemView.horseItem.rowNum == si[0].rowNum)
            {
                winnerHorse.updated(horseItemView.horseItem);
                break;
            }
        }


    }

    private void LateUpdate()
    {
        SortItem[] RankingData = new SortItem[moveer.Count];
        foreach (var rowNum in moveer.Keys)
        {
            GameObject hour = moveer[rowNum];
            MoverLens moveLens = hour.GetComponent<MoverLens>();
            HorseItemView horseItemView = hour.GetComponent<HorseItemView>();
            SortItem siTmp = new SortItem(horseItemView.horseItem.rowNum,
                horseItemView.horseItem.serialNumber,
                0,
                moveLens.lens,
                horseItemView.horseItem.appearance.jockey.dress);
            siTmp.name = horseItemView.horseItem.name;
            RankingData[rowNum - 1] = siTmp;
        }
        Array.Sort(RankingData, SortItem.Compare);
        RaceModel.SortRankingChanged.Dispatch(RankingData);
        SortRankingChanged(RankingData);

        foreach (var rowNum in moveer.Keys)
        {
            GameObject hour = moveer[rowNum];
            if (hour.GetComponent<HorseItemView>().isStartRe && isReplayHorse)
            {
                replayHorseManagerObject.gameObject.SetActive(true);
                isReplayHorse = false;
                isSetReplay = true;
            }
            if (hour.GetComponent<HorseItemView>().isStartRe)
            {
                hour.GetComponent<HorseItemView>().ReplayHorsePos();
                isSetReplay = false;
            }
            if (hour.GetComponent<HorseItemView>().isEndHorseCount)
            {
                endHorseCount++;
            }
            hour.GetComponent<HorseItemView>().isStartRe = false;

        }
        if (endHorseCount >= 3)
        {
            foreach (var rowNum in moveer.Keys)
            {
                GameObject hour = moveer[rowNum];
                hour.GetComponent<HorseItemView>().isPlayEndHrose = false;
                hour.GetComponent<HorseItemView>().isEndHorseBl = false;
                hour.GetComponent<HorseItemView>().isEndHrose = false;
            }
        }
        else
        {
            endHorseCount = 0;
        }
        if (isShowCamera_ZhongDian)
        {
            cam_ZhongDian.gameObject.SetActive(true);
        }
    }
    int endHorseCount = 0;
    void onStateChanged(RaceStateType aRACING_READY)
    {
        foreach (var rowNum in moveer.Keys)
        {
            GameObject hour = moveer[rowNum];
            if (aRACING_READY == RaceStateType.RACING_READY)
            {
                hour.GetComponent<Animator>().SetBool("isReady", true);
            }
            if (aRACING_READY == RaceStateType.RACING_RUN)
            {
                hour.GetComponent<Animator>().SetBool("isReady", false);
            }
        }
    }

    void showReplayHorse()
    {
        CancelInvoke("showReplayHorse");
        foreach (var rowNum in moveer.Keys)
        {
            GameObject hour = moveer[rowNum];
            hour.GetComponent<HorseItemView>().isPlayEndHrose = true;
            hour.GetComponent<HorseItemView>().isEndHorseBl = true;
        }
        Time.timeScale = 0.5f;
        ReplayHorseManager.Singleton.isPlaying = true;
        Invoke("destoryReplayObject", 7f);
        isShowCamera_ZhongDian = true;
    }

    void destoryReplayObject()
    {
        Destroy(replayHorseManagerObject.gameObject);
    }

    internal void SetState(string state)
    {
        if (state == "Ready")
        {
            jiXieZhaXiangAnimator.SetBool("FrontDoor", false);
            //moveer.Clear();
            //Destory2();

            foreach (var rowNum in moveer.Keys)
            {
                GameObject hour = moveer[rowNum];
                hour.GetComponent<HorseItemView>().run();
                hour.GetComponent<HorseItemView>().speedColliderList = speedCollList;
            }

            // 广播-比赛准备指令，

            //RaceModel.RaceStateChanged.Dispatch(RaceStateType.RACING_READY);
        }

        if (state == "racing")
        {
            jiXieZhaXiangAnimator.SetBool("FrontDoor", true);
            foreach (var rowNum in moveer.Keys)
            {
                GameObject hour = moveer[rowNum];
                NavMeshAgent agent = hour.GetComponent<NavMeshAgent>();
                agent.enabled = true;
                agent.speed = 1;
                agent.destination = Targets_1200m[rowNum - 1].transform.position;
                hour.GetComponent<HorseItemView>().exerciseBl = true;
            }
            Invoke("showReplayHorse", 24f);
            replayHorseManagerObject = Instantiate(replayHorseObject);
            replayHorseManagerObject.gameObject.SetActive(false);
            trans_cvmRacing.gameObject.SetActive(true);
            cam_Start.gameObject.SetActive(false);

            // 广播比赛指令
            //RaceModel.RaceStateChanged.Dispatch(RaceStateType.RACING_RUN);

        }

        if (state == "introduction")
        {
            print(TMP_RacingName);
            TMP_RacingName.text = RaceModel.RaceInfo.matchName;
            jiXieZhaXiangAnimator.SetBool("FrontDoor", false);
            cam_ZhongDian.gameObject.SetActive(false);
            tran_cvm_ZhiDao_1.gameObject.SetActive(true);
            moveer.Clear();
            Destory();
            SetHorses();
            cam_Start.SetActive(true);
            cam_Start.GetComponent<CinemachineVirtualCamera>().Priority = 15;
            isReplayHorse = true;
            isShowCamera_ZhongDian = false;
            endHorseCount = 0;
            // 广播介绍指令
            //RaceModel.RaceStateChanged.Dispatch(RaceStateType.INTRODUCTION);
        }
    }

    internal void SetHorses()
    {
        for (int i = 0; i < RaceModel.RaceInfo.horses.Count; i++)
        {
            var horseItem = RaceModel.RaceInfo.horses[i];
            var house = createHorse("horse (" + (horseItem.serialNumber) + ")", horseParent,
                new Vector3(0, 0, i + 1));
            moveer.Add(i + 1, house);

            var horseItemView = house.GetComponent<HorseItemView>();
            horseItemView.horseItem = horseItem;
        }
    }

    private GameObject createHorse(string name, Transform horseParent, Vector3 x)
    {
        GameObject horse = Instantiate(horseprefab, new Vector3(.0f, .0f, .0f),
            Quaternion.AngleAxis(-90, Vector3.up)) as GameObject;
        horse.transform.parent = horseParent;
        horse.transform.localPosition = x;
        horse.name = name;
        //horse.layer = LayerMask.GetMask("Horse");
        return horse;
    }

    internal void Destory()
    {
        for (int i = horseParent.childCount - 1; i >= 0; i--)
        {
            Destroy(horseParent.GetChild(i).gameObject);
            //Destroy();
        }
        //transform.GetChild(0).gameObject.SetActive(false);
    }
}
