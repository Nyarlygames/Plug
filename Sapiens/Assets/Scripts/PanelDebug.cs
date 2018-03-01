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
        SkipDays = GameObject.Find("Debug_SkipDaysT").GetComponent<Text>();
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
            if ((res.width > 800) && (res.height > 600))
                DDopt.Add(opt);
        }
        ScreenSizes.options = DDopt;
    }


    public void SetScreenSizeClick()
    {
        string opt = ScreenSizes.captionText.text;
        int width = Convert.ToInt32(opt.Substring(0, opt.IndexOf("/")));
        int height = Convert.ToInt32(opt.Substring(opt.IndexOf("/")+1));
        //GameObject.Find("UI_Debug").GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
        //Screen.SetResolution
        Screen.SetResolution(width, height, false);
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
            int days = Convert.ToInt32(SkipDays.text);
            foreach (CharacterSave chara in GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.members)
            {
                AgeStruct addedTime = new AgeStruct();
                addedTime.days = days;
                chara.GainTime(addedTime, true); // TODO : switch when non automated activities
            }
            GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.time += days * 24;
            GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.SetAge();
            GameObject.Find("GameManager").GetComponent<GameManager>().timeSinceReload += days * 24;
            Debug.Log("skipped : " + days + " days");
        }
        catch (FormatException)
        {
            Debug.Log("skip error : wrong days number");
        }
        try
        {
            int years = Convert.ToInt32(SkipYears.text);
            foreach (CharacterSave chara in GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.members)
            {
                AgeStruct addedTime = new AgeStruct();
                addedTime.years = years;
                chara.GainTime(addedTime, true); // TODO : switch when non automated activities
            }
            GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.time += years * 365 * 24;
            GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>().tribeCurrent.SetAge();
            GameObject.Find("GameManager").GetComponent<GameManager>().timeSinceReload += years * 365 * 24;
            Debug.Log("skipped : " + years + " years");
        }
        catch (FormatException)
        {
            Debug.Log("skip error : wrong years number");

        }
    }

    void CreateCharClick()
    {
        try
        {
            TribeGO tribeGO = GameObject.Find("GameManager").GetComponent<GameManager>().TribeGO.GetComponent<TribeGO>();
            GameObject Tribe_Members = GameObject.Find("Tribe_Members");
            CharacterSave addedChar = new CharacterSave();
            addedChar.age.years = Convert.ToInt32(CharAge.text);
            GameObject.Find("GameManager").GetComponent<GameManager>().CreatePlayer(tribeGO.tribeCurrent.members.Count, CharName.text, addedChar, tribeGO, Tribe_Members);
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
