using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ActivityStruct {

    public string name = "";
    public int level = 0;
    public float xp = 0.0f;
    public bool status = false;
    public bool statusPexHigh = false;
    public float xpUp = 0.0f;
    public float xpCurrent = 0.0f;
    public float nextLevel = 0.0f;
    public float ratiolvl = 0.0f;
    public float ratiogain = 0.0f;
    public bool primaryActivity = false;

    public IDictionary<string, int> affinities = new Dictionary<string, int>();
}
