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
    public SpriteRenderer Fog;
    GameObject UIDebug;
    GameObject UIChar;
    GameObject UITribe;
    CameraScript CamScript;
    bool debug_panel = true;
    bool char_panel = false;
    bool tribe_panel = false;
    public bool dusk_cycle = true;
    public Color duskmax = new Color(1f, 1f, 1f, 0.4f);
    public Color duskmin = new Color(1f, 1f, 1f, 0.0f);
    private float duskspeed = 0.2f;
    Vector3 MousePos = Vector3.zero;
    Transform Cursor_Targett;
    public GameObject Cursor_Target;
    Transform Cursor_Mouset;

    public float GroundPlane = 0.0f;
    public float TribePlane = 1.0f;
    public float CharacterPlane = 2.0f;
    public float UIPlane = 3.0f;

    void Start ()
    {
        UIDebug = GameObject.Find("UI_Debug");
        UIDebug.SetActive(debug_panel);
        UIChar = GameObject.Find("UI_CharacterPanel");
        UIChar.SetActive(char_panel);
        UITribe = GameObject.Find("UI_TribePanel");
        UITribe.SetActive(tribe_panel);
        PlayerPrefs.SetFloat("TribeAge", 0.0f); // keep previous experience when loading/saving is done
        TribeAge = GameObject.Find("TribeAge").GetComponent<Text>();
        TribeSprite = GameObject.Find("Tribe").GetComponent<SpriteRenderer>();
        TribeScript = GameObject.Find("Tribe").GetComponent<TribeScript>();
        CamScript = GameObject.Find("Camera").GetComponent<CameraScript>();
        Time.timeScale = 1.0f;
        Fog = GameObject.Find("Fog").GetComponent<SpriteRenderer>();
        Cursor_Mouset = GameObject.Find("Cursor_Mouse").GetComponent<Transform>();
        Cursor_Targett = GameObject.Find("Cursor_Target").GetComponent<Transform>();
        Cursor_Target = GameObject.Find("Cursor_Target");
    }

    void FixedUpdate()
    {
        TribeAge.text = "Tribe's Age : " + format_Time((float)Time.time); // shows the tribe's age
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.y = UIPlane;
        Cursor_Mouset.position = MousePos;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // hide/show character panel
        {
            char_panel = !char_panel;
            UIChar.SetActive(char_panel);
        }

        if (Input.GetKeyDown(KeyCode.T)) // hide/show tribe panel
        {
            tribe_panel = !tribe_panel;
            UITribe.SetActive(tribe_panel);
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
        if (TribeScript.targetHit == Vector3.zero)
            Cursor_Target.SetActive(false);
    }

    string format_Time(float time)
    {
        float temp_time = 0;
        int hours = 0;
        int days = 0;
        int weeks = 0;
        int months = 0;
        int years = 0;

        temp_time = time;

        if (temp_time > 8760)
        { // years
            years = (int)temp_time / 8760;
            temp_time = temp_time % 8760;
        }
        if (temp_time > 720)
        {  // months
            months = (int)temp_time / 720;
            temp_time = temp_time % 720;
        }
        if (temp_time > 168) // weeks
        {
            weeks = (int)temp_time / 168;
            temp_time = temp_time % 168;
        }
        if (temp_time >= 24) // days
        {
            days = (int)temp_time / 24;
            temp_time = temp_time % 24;
        }
        if (temp_time < 24)
        {
            hours = (int)temp_time;
        }



        string formattedString = "";
        if (hours >= 10)
            formattedString += hours + " hours, ";
        else
            formattedString += "0" + hours + " hours, ";

        if (days >= 10)
            formattedString += days + " days ,";
        else
            formattedString += "0" + days + " days, ";

        if (weeks >= 10)
            formattedString += weeks + " weeks, ";
        else
            formattedString += "0" + weeks + " weeks, ";

        if (months >= 10)
            formattedString += months + " months, ";
        else
            formattedString += "0" + months + " months, ";

        if (years >= 10)
            formattedString += years + " years";
        else
            formattedString += "0" + years + " years";

        PlayerPrefs.SetString("FormattedTime", formattedString);
        PlayerPrefs.SetInt("TribeHours", hours);
        PlayerPrefs.SetInt("TribeDays", days);
        PlayerPrefs.SetInt("TribeWeeks", weeks);
        PlayerPrefs.SetInt("TribeMonth", months);
        PlayerPrefs.SetInt("TribeYears", years);
        PlayerPrefs.SetString("TribeAgeUtc", System.DateTime.Now.ToString()); // system date, use to skip later.

        if (dusk_cycle == true)
        {
            if ((hours >= 20) || (hours < 5))
            {
                if (Fog.color.a < duskmax.a)
                {
                    Fog.color = new Color(1f, 1f, 1f, Fog.color.a + (duskspeed * Time.deltaTime));
                }
                if (Fog.color.a > duskmax.a)
                    Fog.color = duskmax;
            }
            else
            {
                if (Fog.color.a > duskmin.a)
                {
                    Fog.color = new Color(1f, 1f, 1f, Fog.color.a - (duskspeed * Time.deltaTime));
                }
                if (Fog.color.a < duskmin.a)
                    Fog.color = duskmin;
            }
        }


        if ((hours >= 21) || (hours < 6))
        {

            TribeSprite.sprite = TribeScript.CampOff;
        }
        else
        {
            TribeSprite.sprite = TribeScript.CampOn;
        }
        return PlayerPrefs.GetString("FormattedTime");
    }
}
