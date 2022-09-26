using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HorseRacing.introduction;
public class HorseChangeSignals : MonoBehaviour
{
    public Transform view;

    public Transform ui;


    IntroductionHorseShowView introView;
    
    public void Change(string value)
    {
        if (introView == null)
            introView = view.GetComponent<IntroductionHorseShowView>();
        
        int index = 0;
        if (int.TryParse(value, out index))
        {
            introView.Change(index);
            introView.ChagneUI(ui, index);
        }
        
    }
}
