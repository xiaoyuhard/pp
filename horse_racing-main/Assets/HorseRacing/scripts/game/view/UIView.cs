using System;
using System.Collections;
using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

public class UIView : View
{
    [Inject]
    public RaceModel RaceModel { get; set; }

    private GameObject racingUI;

    private GameObject recordUI;

    private GameObject readyUI;

    private GameObject replayUI;

    private RankingPanel rankingPanel;

    private MinimapPanel minmapPanel;

    private RecordPanel recordPanel;

    private ReadyPanel readyPanel;

    private GameObject paiming_1st;

    private Text replayUIText;


    [SerializeField] private Transform BGM_Audio;


    private void Start()
    {
        racingUI = transform.GetChild(0).GetChild(0).gameObject;
        recordUI = transform.GetChild(0).GetChild(1).gameObject;
        readyUI = transform.GetChild(0).GetChild(3).gameObject;
        replayUI = transform.GetChild(0).GetChild(4).gameObject;
        replayUIText = transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>();

        racingUI.SetActive(false);
        recordUI.SetActive(false);
        readyUI.SetActive(false);

        rankingPanel = racingUI.transform.Find("RankingPanel").GetComponent<RankingPanel>();
        minmapPanel = racingUI.transform.Find("MiniMapPanel").GetComponent<MinimapPanel>();
        paiming_1st = racingUI.transform.Find("Paiming_1st").gameObject;
        recordPanel = recordUI.GetComponent<RecordPanel>();
        readyPanel = readyUI.GetComponent<ReadyPanel>();

    }

    internal void Init()
    {
        Debug.LogFormat("{0} {1} {2}", minmapPanel.name, RaceModel.RaceLength, RaceModel.MapLength);
        // minmapPanel.Init("nidi", RaceModel.MapLength, RaceModel.RaceLength);
        // rankingPanel.UpdateInfo(new []{3,6,4,5,7,8,9,1,2,10});
    }

    internal void SortRanking(SortItem[] items)
    {
        if (items.Length > 0 && items[0].len >= 5 && items[0].len <= RaceModel.RaceLength - 100)
        {
            rankingPanel.gameObject.SetActive(true);
            minmapPanel.gameObject.SetActive(true);
            paiming_1st.SetActive(true);
            rankingPanel.UpdateInfo(items);
            minmapPanel.CurrentLength = items[0].len;
        }
        else
        {
            rankingPanel.gameObject.SetActive(false);
            minmapPanel.gameObject.SetActive(false);
            paiming_1st.SetActive(false);
        }
    }

    internal void ChangeUIState(string state)
    {
        switch (state)
        {
            case "ReadyUI":
                BGM_Audio.gameObject.SetActive(false);
                readyUI.SetActive(true);
                Time.timeScale = 1.5f;
                Invoke("WaiteForReady", 4f);
                readyPanel.PlayReadyGo();
                RaceModel.RaceStateChanged.Dispatch(RaceStateType.RACING_READY);
                break;
            case "ShowRacingUI":
                Invoke("showRacingUI", 5f);
                Invoke("showRecoderUI", 30f);
                Invoke("showReplayUI", 22f);
                recordUI.SetActive(false);
                //RaceModel.RaceStateChanged.Dispatch(RaceStateType.RACING_RUN);
                break;
            case "ShowRecordUI":
                racingUI.SetActive(false);
                recordUI.SetActive(true);
                Time.timeScale = 1;
                recordPanel.UpdateDate(RaceModel.RankingData);
                Invoke("hideRecoderUI", 37f);
                break;
        }
    }

    void showReplayUI()
    {
        Time.timeScale = 1;
        replayUI.SetActive(true);
        replayUI.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1f);
        replayUIText.DOColor(new Color(185 / 255f, 166 / 255f, 90 / 255f, 1), 1f);
        Invoke("hideReplayUI", 1f);
    }

    void hideReplayUI()
    {
        replayUI.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 2f);
        replayUIText.DOColor(new Color(185 / 255f, 166 / 255f, 90 / 255f, 0), 2f);
        StartCoroutine(hideFalseReplayUI());
    }

    IEnumerator hideFalseReplayUI()
    {
        yield return new WaitForSeconds(1f);
        replayUI.SetActive(false);
    }

    void showRacingUI()
    {
        CancelInvoke("showRacingUI");
        racingUI.SetActive(true);
    }

    void showRecoderUI()
    {
        CancelInvoke("showRecoderUI");
        ChangeUIState("ShowRecordUI");
        recordPanel.UpdateDate(RaceModel.RankingData);
    }

    void hideRecoderUI()
    {
        recordUI.SetActive(false);
        CancelInvoke("hideRecoderUI");
        RaceModel.RaceStateCanChangeToIntroduction = true;
    }

    void WaiteForReady()
    {
        readyUI.SetActive(false);
        RaceModel.RaceStateCanChangeToIntroduction = false;
        RaceModel.RaceInfoChanged.Dispatch("racing");
    }

}
