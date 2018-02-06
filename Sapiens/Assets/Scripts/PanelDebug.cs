using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PanelDebug : MonoBehaviour
{
    Slider TimeSlider;
    Text RatioSpeed;
    Text CharName;
    Text CharAge;
    Text SkipYears;
    Text SkipDays;
    Text SkipHours;
    public float init_speed;
    // Use this for initialization
    void Start ()
    {
        TimeSlider = GameObject.Find("TimeSlider").GetComponent<Slider>();
        Button temp = GameObject.Find("Debug_CharCreateButton").GetComponent<Button>();
        temp.onClick.AddListener(CreateCharClick);
        temp = GameObject.Find("Debug_SkipButton").GetComponent<Button>();
        temp.onClick.AddListener(SkipClick);
        RatioSpeed = GameObject.Find("TimeSlider_Info").GetComponent<Text>();
        CharName = GameObject.Find("Debug_CharCreateNameText").GetComponent<Text>();
        CharAge = GameObject.Find("Debug_CharCreateAgeText").GetComponent<Text>();
        SkipYears = GameObject.Find("Debug_SkipYearsT").GetComponent<Text>();
        SkipDays = GameObject.Find("Debug_SkipDaysT").GetComponent<Text>();
        SkipHours = GameObject.Find("Debug_SkipHoursT").GetComponent<Text>();
        TimeSlider.onValueChanged.AddListener(delegate {
            ScaleChange(TimeSlider);
        });
        TimeSlider.value = 3.5f;
        Time.timeScale = 3.5f;
        RatioSpeed.text = "time ratio : x" + Time.timeScale;
    }

    public void ScaleChange(Slider scale)
    {
        Time.timeScale = scale.value;
        RatioSpeed.text = "time ratio : x" + Time.timeScale;
    }

    void SkipClick()
    {
        try
        {
            int year = Convert.ToInt32(SkipYears.text);
            foreach(CharacterSave chara in GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.members)
            {
                float xpacc = chara.SkipYear(year);
                chara.next = chara.SkipStats(xpacc, chara.next);
            }
            GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.time += year * 365 * 24;
            GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.SetAge();
            GameObject.Find("GameManager").GetComponent<GameManager>().timeSinceReload += year * 365 * 24;
            Debug.Log("skipped : " + year + " years");
        }
        catch (FormatException)
        {

        }
    }

    void CreateCharClick()
    {
        try
        {
            int age = Convert.ToInt32(CharAge.text);
            TribeGO tribeGO = GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>();
            GameObject.Find("GameManager").GetComponent<GameManager>().CreatePlayer(tribeGO.tribeCurrent.members.Count, CharName.text, Convert.ToInt32(CharAge.text), "Play/TribeChar/Man2", tribeGO);
            Debug.Log("Char : " + CharName.text + " created.");
        }
        catch (FormatException)
        {

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
