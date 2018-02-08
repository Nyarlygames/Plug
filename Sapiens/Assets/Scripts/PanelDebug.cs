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
    Dropdown ScreenSizes;
    public float init_speed;
    // Use this for initialization
    void Start ()
    {
        TimeSlider = GameObject.Find("TimeSlider").GetComponent<Slider>();
        Button temp = GameObject.Find("Debug_CharCreateButton").GetComponent<Button>();
        temp.onClick.AddListener(CreateCharClick);
        temp = GameObject.Find("Debug_SkipButton").GetComponent<Button>();
        temp.onClick.AddListener(SkipClick);
        temp = GameObject.Find("Debug_ScreenSize").GetComponent<Button>();
        temp.onClick.AddListener(SetScreenSizeClick);
        RatioSpeed = GameObject.Find("TimeSlider_Info").GetComponent<Text>();
        CharName = GameObject.Find("Debug_CharCreateNameText").GetComponent<Text>();
        CharAge = GameObject.Find("Debug_CharCreateAgeText").GetComponent<Text>();
        SkipYears = GameObject.Find("Debug_SkipYearsT").GetComponent<Text>();
        //SkipDays = GameObject.Find("Debug_SkipDaysT").GetComponent<Text>();
        //SkipHours = GameObject.Find("Debug_SkipHoursT").GetComponent<Text>();
        TimeSlider.onValueChanged.AddListener(delegate {
            ScaleChange(TimeSlider);
        });
        TimeSlider.value = 3.5f;
        Time.timeScale = 3.5f;
        RatioSpeed.text = "time ratio : x" + Time.timeScale;
        ScreenSizes = GameObject.Find("Debug_ScreenSizeSelector").GetComponent<Dropdown>();
        SetExistingSize();
    }


    public void SetExistingSize()
    {
        List<Dropdown.OptionData> DDopt = new List<Dropdown.OptionData>();
        foreach (Resolution res in Screen.resolutions)
        {
            Dropdown.OptionData opt = new Dropdown.OptionData();
            opt.text = res.width+"/"+res.height;
            DDopt.Add(opt);
        }
        ScreenSizes.options = DDopt;
    }


    public void SetScreenSizeClick()
    {
        string opt = ScreenSizes.captionText.text;
        int width = Convert.ToInt32(opt.Substring(0, opt.IndexOf("/")));
        int height = Convert.ToInt32(opt.Substring(opt.IndexOf("/")+1));
        Debug.Log("A " + GameObject.Find("UI_Debug").GetComponent<CanvasScaler>().referenceResolution);
        //GameObject.Find("UI_Debug").GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
        Debug.Log("B " + GameObject.Find("UI_Debug").GetComponent<CanvasScaler>().referenceResolution);
        Debug.Log("A " + Screen.currentResolution);
        //Screen.SetResolution
        Screen.SetResolution(width, height, false);
        Debug.Log("B " + Screen.currentResolution);
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
            TribeGO tribeGO = GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>();
            GameObject Tribe_Members = GameObject.Find("Tribe_Members");
            GameObject.Find("GameManager").GetComponent<GameManager>().CreatePlayer(tribeGO.tribeCurrent.members.Count, CharName.text, Convert.ToInt32(CharAge.text), "Play/TribeChar/Man2", tribeGO, Tribe_Members);
            GameObject Charlist = GameObject.Find("UI_CharPanel");
            if (Charlist != null)
            {
                Charlist.GetComponent<PanelChar>().SetExistingChars();
            }
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
