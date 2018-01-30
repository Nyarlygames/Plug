using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsScript : MonoBehaviour
{
    Text TribeAge;
    SpriteRenderer TribeSprite;
    TribeScript TribeScript;
    public SpriteRenderer DuskDawn;
    GameObject UIDebug;
    GameObject UICharactersPanelPrefab;
    GameObject UITribePanelPrefab;
    GameObject UIMapPanelPrefab;
    GameObject UIResourcesPanelPrefab;
    public GameObject UICharactersPanel;
    public GameObject UITribePanel;
    public GameObject UIMapPanel;
    public GameObject UIResourcesPanel;
    CameraScript CamScript;
    bool debug_panel = true;
    public bool char_panel = false;
    public bool tribe_panel = false;
    public bool resources_panel = false;
    public bool map_panel = false;
    public bool dusk_cycle = true;
    public Color duskmax = new Color(1f, 1f, 1f, 0.4f);
    public Color duskmin = new Color(1f, 1f, 1f, 0.0f);
    private float duskspeed = 0.2f;
    Vector3 MousePos = Vector3.zero;
    Transform Cursor_Targett;
    public GameObject Cursor_Target;
    Transform Cursor_Mouset;
    public TimeStruct TS = new TimeStruct();

    public float GroundPlane = 0.0f;
    public float TribePlane = 1.0f;
    public float CharacterPlane = 2.0f;
    public float UIPlane = 3.0f;
    private float timeScaleBeforePause = 0.0f;
    private float timeScaleBeforeMenu = 0.0f;

    void Start()
    {
        UIDebug = GameObject.Find("UI_Debug");
        UIDebug.SetActive(debug_panel);
        PlayerPrefs.GetFloat("TribeAge"); // keep previous experience when loading/saving is done
        TribeAge = GameObject.Find("TribeAge").GetComponent<Text>();
        TribeSprite = GameObject.Find("Tribe").GetComponent<SpriteRenderer>();
        TribeScript = GameObject.Find("Tribe").GetComponent<TribeScript>();
        CamScript = GameObject.Find("Camera").GetComponent<CameraScript>();
        Time.timeScale = 1.0f;
        DuskDawn = GameObject.Find("DuskDawn").GetComponent<SpriteRenderer>();
        Cursor_Mouset = GameObject.Find("Cursor_Mouse").GetComponent<Transform>();
        Cursor_Targett = GameObject.Find("Cursor_Target").GetComponent<Transform>();
        Cursor_Target = GameObject.Find("Cursor_Target");
        UICharactersPanelPrefab = Resources.Load<GameObject>("Play/Prefabs/UI_CharacterPanel");
        UITribePanelPrefab = Resources.Load<GameObject>("Play/Prefabs/UI_TribePanel");
        UIResourcesPanelPrefab = Resources.Load<GameObject>("Play/Prefabs/UI_ResourcesPanel");
        UIMapPanelPrefab = Resources.Load<GameObject>("Play/Prefabs/UI_MapPanel");

        UICharactersPanel = Instantiate(UICharactersPanelPrefab, Vector3.zero, Quaternion.identity);
        UITribePanel = Instantiate(UITribePanelPrefab, Vector3.zero, Quaternion.identity);
        UIResourcesPanel = Instantiate(UIResourcesPanelPrefab, Vector3.zero, Quaternion.identity);
        UIMapPanel = Instantiate(UIMapPanelPrefab, Vector3.zero, Quaternion.identity);

        UICharactersPanel.SetActive(false);
        UITribePanel.SetActive(false);
        UIResourcesPanel.SetActive(false);
        UIMapPanel.SetActive(false);
    }

    void FixedUpdate()
    {
        TS = format_Time((float)Time.time);
        TribeAge.text = "Session time : " + TS.formattedString; // shows the session's age
        DayNightCycle();
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.y = UIPlane;
        Cursor_Mouset.position = MousePos;
    }


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && (Time.timeScale != 0.0f) && (timeScaleBeforePause == 0.0f) && (timeScaleBeforeMenu == 0.0f)) // pause
        {
            timeScaleBeforePause = Time.timeScale;
            Time.timeScale = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.P) && (Time.timeScale == 0.0f) && (timeScaleBeforeMenu == 0.0f) && (timeScaleBeforePause != 0.0f)) // unpause
        {
            Time.timeScale = timeScaleBeforePause;
            timeScaleBeforePause = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.C)) // hide/show character panel
        {
            OpenPanel("CharacterPanel");
        }
        if (Input.GetKeyDown(KeyCode.M)) // hide/show map panel
        {
            OpenPanel("MapPanel");
        }
        if (Input.GetKeyDown(KeyCode.R)) // hide/show resources panel
        {
            OpenPanel("ResourcesPanel");
        }

        if (Input.GetKeyDown(KeyCode.T)) // hide/show tribe panel
        {
            OpenPanel("TribePanel");
        }

        if (Input.GetKeyDown(KeyCode.S)) // simulate save
        {
            PlayerPrefs.SetFloat("TribeAge", PlayerPrefs.GetFloat("TribeAge") + PlayerPrefs.GetFloat("TribeTime"));
        }

        if (Input.GetKeyDown(KeyCode.H)) // hide/show debug ui
        {
            debug_panel = !debug_panel;
            UIDebug.SetActive(debug_panel);
        }

        // dezoom if space is pressed, else zoom
        if (Input.GetKey(KeyCode.Space))
        {
            CamScript.Dezoom();
        }
        else
        {
            CamScript.Zoom();
        }
        
        if (Input.GetMouseButtonDown(1)) // right click to move tribe
        {
            Vector3 TargetCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TargetCoords.y = UIPlane;
            Cursor_Target.SetActive(true);
            Cursor_Targett.position = TargetCoords;
            TargetCoords.y = TribePlane;
            TribeScript.targetHit = TargetCoords;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // cancel move 
        {
            TribeScript.targetHit = Vector3.zero;
        }

        if (TribeScript.targetHit == Vector3.zero)
            Cursor_Target.SetActive(false);
    }

    void OpenPanel(string name)
    {
        switch (name)
        {
            case "CharacterPanel":
                if (char_panel == false)
                {
                    char_panel = true;
                    UICharactersPanel.SetActive(true);
                    if (map_panel == true)
                    {
                        UIMapPanel.SetActive(false);
                        map_panel = false;
                    }
                    else if (tribe_panel == true)
                    {
                        UITribePanel.SetActive(false);
                        tribe_panel = false;

                    }
                    else if (resources_panel == true)
                    {
                        UIResourcesPanel.SetActive(false);
                        resources_panel = false;

                    }
                    else
                    {
                        if (timeScaleBeforePause == 0.0f)
                        {
                            timeScaleBeforeMenu = Time.timeScale;
                            Time.timeScale = 0.0f;
                        }
                        else
                        {
                            timeScaleBeforeMenu = timeScaleBeforePause;
                        }
                    }
                }
                else
                {
                    UICharactersPanel.SetActive(false);
                    char_panel = false;

                    if ((timeScaleBeforePause == 0.0f) && (timeScaleBeforeMenu != 0.0f))
                    {
                        Time.timeScale = timeScaleBeforeMenu;
                    }
                    timeScaleBeforeMenu = 0.0f;
                }
                break;
                
            case "TribePanel":
                if (tribe_panel == false)
                {
                    tribe_panel = true;
                    UITribePanel.SetActive(true);
                    if (char_panel == true)
                    {
                        UICharactersPanel.SetActive(false);
                        char_panel = false;
                    }
                    else if (resources_panel == true)
                    {
                        UIResourcesPanel.SetActive(false);
                        resources_panel = false;

                    }
                    else if (map_panel == true)
                    {
                        UIMapPanel.SetActive(false);
                        map_panel = false;

                    }
                    else
                    {
                        if (timeScaleBeforePause == 0.0f)
                        {
                            timeScaleBeforeMenu = Time.timeScale;
                            Time.timeScale = 0.0f;
                        }
                        else
                        {
                            timeScaleBeforeMenu = timeScaleBeforePause;
                        }
                    }
                }
                else
                {
                    UITribePanel.SetActive(false);
                    tribe_panel = false;

                    if ((timeScaleBeforePause == 0.0f) && (timeScaleBeforeMenu != 0.0f))
                    {
                        Time.timeScale = timeScaleBeforeMenu;
                    }
                    timeScaleBeforeMenu = 0.0f;
                }
                break;

            case "ResourcesPanel":
                if (resources_panel == false)
                {
                    resources_panel = true;
                    UIResourcesPanel.SetActive(true);
                    if (char_panel == true)
                    {
                        UICharactersPanel.SetActive(false);
                        char_panel = false;
                    }
                    else if (tribe_panel == true)
                    {
                        UITribePanel.SetActive(false);
                        tribe_panel = false;

                    }
                    else if (map_panel == true)
                    {
                        UIMapPanel.SetActive(false);
                        map_panel = false;

                    }
                    else
                    {
                        if (timeScaleBeforePause == 0.0f)
                        {
                            timeScaleBeforeMenu = Time.timeScale;
                            Time.timeScale = 0.0f;
                        }
                        else
                        {
                            timeScaleBeforeMenu = timeScaleBeforePause;
                        }
                    }
                }
                else
                {
                    UIResourcesPanel.SetActive(false);
                    resources_panel = false;

                    if ((timeScaleBeforePause == 0.0f) && (timeScaleBeforeMenu != 0.0f))
                    {
                        Time.timeScale = timeScaleBeforeMenu;
                    }
                    timeScaleBeforeMenu = 0.0f;
                }
                break;

            case "MapPanel":
                if (map_panel == false)
                {
                    map_panel = true;
                    UIMapPanel.SetActive(true);
                    if (char_panel == true)
                    {
                        UICharactersPanel.SetActive(false);
                        char_panel = false;
                    }
                    else if (tribe_panel == true)
                    {
                        UITribePanel.SetActive(false);
                        tribe_panel = false;

                    }
                    else if (resources_panel == true)
                    {
                        UIResourcesPanel.SetActive(false);
                        resources_panel = false;

                    }
                    else
                    {
                        if (timeScaleBeforePause == 0.0f)
                        {
                            timeScaleBeforeMenu = Time.timeScale;
                            Time.timeScale = 0.0f;
                        }
                        else
                        {
                            timeScaleBeforeMenu = timeScaleBeforePause;
                        }
                    }
                }
                else
                {
                    UIMapPanel.SetActive(false);
                    map_panel = false;

                    if ((timeScaleBeforePause == 0.0f) && (timeScaleBeforeMenu != 0.0f))
                    {
                        Time.timeScale = timeScaleBeforeMenu;
                    }
                    timeScaleBeforeMenu = 0.0f;
                }
                break;
            default:
                break;
        }
    }

    void DayNightCycle()
    {
        if (dusk_cycle == true)
        {
            if ((TS.hours >= 20) || (TS.hours < 5))
            {
                if (DuskDawn.color.a < duskmax.a)
                {
                    DuskDawn.color = new Color(1f, 1f, 1f, DuskDawn.color.a + (duskspeed * Time.deltaTime));
                }
                if (DuskDawn.color.a > duskmax.a)
                    DuskDawn.color = duskmax;
            }
            else
            {
                if (DuskDawn.color.a > duskmin.a)
                {
                    DuskDawn.color = new Color(1f, 1f, 1f, DuskDawn.color.a - (duskspeed * Time.deltaTime));
                }
                if (DuskDawn.color.a < duskmin.a)
                    DuskDawn.color = duskmin;
            }
        }


        if ((TS.hours >= 21) || (TS.hours < 6))
        {

            TribeSprite.sprite = TribeScript.CampOff;
        }
        else
        {
            TribeSprite.sprite = TribeScript.CampOn;
        }
    }

    TimeStruct format_Time(float time)
    {
        // TO redo timings!
        float temp_time = 0;
        float temp_timecumul = 0;
        int HoursInDay = 24; //(24*30)
        int HoursinWeek = 168; //(24*7)
        int HoursinMonth = 720; //(24*30)
        int HoursinYear = 8760; //(24*365)

        temp_time = time;
        temp_timecumul = time + PlayerPrefs.GetFloat("TribeAge");

        if (temp_time > HoursinYear)
        { // years
            TS.years = (int)temp_time / HoursinYear;
            TS.cumulyears = (int)time / HoursinYear;
            temp_time = temp_time % HoursinYear;
        }
        if (temp_time > HoursinMonth)
        {  // months
            TS.months = (int)temp_time / HoursinMonth;
            TS.cumulmonths = (int)time / HoursinMonth;
            temp_time = temp_time % HoursinMonth;
        }
        if (temp_time > HoursinWeek) // weeks
        {
            TS.weeks = (int)temp_time / HoursinWeek;
            TS.cumulweeks = (int)time / HoursinWeek;
            temp_time = temp_time % HoursinWeek;
        }
        if (temp_time >= HoursInDay) // days
        {
            TS.days = (int)temp_time / HoursInDay;
            TS.cumuldays = (int)time / HoursInDay;
            temp_time = temp_time % HoursInDay;
        }
        if (temp_time < HoursInDay)
        {
            TS.hours = (int)temp_time;
            TS.cumulhours = (int)time / 1;
        }


        // save time
        if (temp_timecumul > HoursinYear)
        { // years
            TS.saveyears = (int)temp_timecumul / HoursinYear;
            //temp_timecumul = temp_timecumul % HoursinYear;
        }
        if (temp_timecumul > HoursinMonth)
        {  // months
            TS.savemonths = (int)temp_timecumul / HoursinMonth;
           // temp_timecumul = temp_timecumul % HoursinMonth;
        }
        if (temp_timecumul > HoursinWeek) // weeks
        {
            TS.saveweeks = (int)temp_timecumul / HoursinWeek;
          //  temp_timecumul = temp_timecumul % HoursinWeek;
        }
        if (temp_timecumul >= HoursInDay) // days
        {
            TS.savedays = (int)temp_timecumul / HoursInDay;
           // temp_timecumul = temp_timecumul % HoursInDay;
        }
        TS.savehours = (int)temp_timecumul;




        TS.formattedString = "";
        TS.formattedcumulString = "";
        TS.formattedcumulsaveString = "";

        TS.formattedString += TS.hours + " hours, ";
        TS.formattedcumulString += TS.cumulhours + " hours, ";
        TS.formattedcumulsaveString += TS.savehours + " hours, ";

        TS.formattedString += TS.days + " days, ";
        TS.formattedcumulString += TS.cumuldays + " days, ";
        TS.formattedcumulsaveString += TS.savedays + " days, ";

        TS.formattedString += TS.weeks + " weeks, ";
        TS.formattedcumulString += TS.cumulweeks + " weeks, ";
        TS.formattedcumulsaveString += TS.saveweeks + " weeks, ";

        TS.formattedString += TS.months + " months, ";
        TS.formattedcumulString += TS.cumulmonths + " months, ";
        TS.formattedcumulsaveString += TS.savemonths + " months, ";

        TS.formattedString += TS.years + " years";
        TS.formattedcumulString += TS.cumulyears + " years";
        TS.formattedcumulsaveString += TS.saveyears + " years";

        PlayerPrefs.SetString("FormattedTime", TS.formattedString);
        PlayerPrefs.SetString("FormattedcumulTime", TS.formattedcumulString);
        PlayerPrefs.SetString("FormattedcumulsaveTime", TS.formattedcumulsaveString);
        PlayerPrefs.SetInt("TribeTS.hours", TS.hours);
        PlayerPrefs.SetInt("TribeDays", TS.days);
        PlayerPrefs.SetInt("TribeWeeks", TS.weeks);
        PlayerPrefs.SetInt("TribeMonth", TS.months);
        PlayerPrefs.SetInt("TribeYears", TS.years);
        PlayerPrefs.SetString("TribeAgeUtc", System.DateTime.Now.ToString()); // system date, use to skip later.
        PlayerPrefs.SetFloat("TribeTime", time); // system date, use to skip later.

        return TS;
    }
}
