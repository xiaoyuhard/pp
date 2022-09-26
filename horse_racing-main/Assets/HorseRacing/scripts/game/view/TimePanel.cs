using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour
{
    public Text timeText;
    float timer=0;
    public bool startTicker = false;
    // Start is called before the first frame update
    public void StartTimer()
    {
        timer = 0;
        startTicker = true;
    }
    public void EndTimer()
    {
        startTicker = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (startTicker)
        {
            timer += Time.deltaTime;
            timeText.text = string.Format("{0}:{1}", (int)(timer / 60), (timer % 60).ToString("00.0"));
        }
    }
}
