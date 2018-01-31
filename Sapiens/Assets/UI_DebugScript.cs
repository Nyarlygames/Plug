﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_DebugScript : MonoBehaviour {

    Text TimeScalerText;
    Text TimeScalerCurrentText;
    Slider TimeScaler;
    Toggle TDusk;
    Text TDuskText;
    Text TribeAgeCumul;
    Text TribeAgeCumulSave;
    ControlsScript Cs;

    // Use this for initialization
    void Start ()
    {
        TimeScaler = GameObject.Find("TimeScaler").GetComponent<Slider>();
        TimeScalerText = GameObject.Find("TimeScalerText").GetComponent<Text>();
        TribeAgeCumul = GameObject.Find("TribeCumulAge").GetComponent<Text>();
        TribeAgeCumulSave = GameObject.Find("TribeCumulAgeSave").GetComponent<Text>();
        TimeScalerCurrentText = GameObject.Find("TimeScalerCurrentText").GetComponent<Text>();
        Button temp = GameObject.Find("TimeScalerApply").GetComponent<Button>();
        temp.onClick.AddListener(TimeScalerApply_Click);
        Cs = GameObject.Find("Controls").GetComponent<ControlsScript>();
        temp.onClick.AddListener(TimeScalerApply_Click);
        Cs = GameObject.Find("Controls").GetComponent<ControlsScript>();
        TDusk = GameObject.Find("ToggleDusk").GetComponent<Toggle>();
        TDuskText = GameObject.Find("ToggleDuskText").GetComponent<Text>();
        TDusk.onValueChanged.AddListener(delegate {
            ToggleValueChanged(TDusk);
        });
       // Cs.dusk_cycle = true;
    }
	
    void TimeScalerApply_Click()
    {
        Time.timeScale = Convert.ToInt32(TimeScaler.value);
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            if ((Cs != null) && (Cs.DuskDawn != null)) // set dusk/dawn cycle On
            {
                Cs.dusk_cycle = true;
                TDuskText.text = "Dusk/Dawn : On";
            }
        }
        else
        {
            if ((Cs != null) && (Cs.DuskDawn != null)) // set dusk/dawn cycle Off
            {
                Cs.dusk_cycle = false;
                Cs.DuskDawn.color = Cs.duskmin;
                TDuskText.text = "Dusk/Dawn : Off";
            }
        }
    }
    
    void FixedUpdate ()
    {
        TribeAgeCumul.text = "Cumul session : " + Cs.TS.formattedcumulString;
        TribeAgeCumulSave.text = "Cumul save : " + Cs.TS.formattedcumulsaveString;
        // set dusk/dawn cycle On
        TimeScalerCurrentText.text = "Current speed : " + Time.timeScale.ToString();
        TimeScalerText.text = Convert.ToInt32(TimeScaler.value).ToString();

    }
}
