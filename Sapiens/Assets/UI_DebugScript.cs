using System.Collections;
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
    ControlsScript Cs;

    // Use this for initialization
    void Start () {
        TimeScaler = GameObject.Find("TimeScaler").GetComponent<Slider>();
        TimeScalerText = GameObject.Find("TimeScalerText").GetComponent<Text>();
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
        Cs.dusk_cycle = true;
    }
	
    void TimeScalerApply_Click()
    {
        Time.timeScale = Convert.ToInt32(TimeScaler.value);
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            Cs.dusk_cycle = true;
            TDuskText.text = "Dusk/Dawn : On";
        }
        else
        {
            Cs.dusk_cycle = false;
            TDuskText.text = "Dusk/Dawn : Off";
        }
    }

    // Update is called once per frame
    void Update ()
    {
        TimeScalerCurrentText.text = "Current speed : " + Time.timeScale.ToString();
        TimeScalerText.text = Convert.ToInt32(TimeScaler.value).ToString();

    }
}
