using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoverLens : MonoBehaviour
{
    public double lens
    {
        get { return overlens + curentlens; }
    }

    public double overlens;


    public double curentlens;
}
