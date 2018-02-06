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
    public float init_speed;
    // Use this for initialization
    void Start ()
    {
        TimeSlider = GameObject.Find("TimeSlider").GetComponent<Slider>();
        Button temp = GameObject.Find("Debug_CharCreateButton").GetComponent<Button>();
        temp.onClick.AddListener(CreateCharClick);
        RatioSpeed = GameObject.Find("TimeSlider_Info").GetComponent<Text>();
        CharName = GameObject.Find("Debug_CharCreateNameText").GetComponent<Text>();
        CharAge = GameObject.Find("Debug_CharCreateAgeText").GetComponent<Text>();
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
