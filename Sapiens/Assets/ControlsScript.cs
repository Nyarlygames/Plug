using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsScript : MonoBehaviour
{
    Transform pt;
    Transform ct;
    CameraScript cs;
    PlayerScript ps;
    Rigidbody prb;
    Vector3 targetHit = Vector3.zero;
    Text TribeAge;
    SpriteRenderer TribeSprite;
    TribeScript TribeScript;
    SpriteRenderer Fog;
    GameObject UIDebug;
    bool debug_panel = true;
    public bool dusk_cycle = true;
    public int selectedChar = 0;
    public List<Transform> Charpics = new List<Transform>();
    public int timespeed = 0; // level 1/2/3 time speed
    //public float timescale = 0; // current timescale
    private Color duskmax = new Color(1f, 1f, 1f, 0.4f);
    private Color duskmin = new Color(1f, 1f, 1f, 0.0f);
    private float duskspeed = 0.2f;

    void Start ()
    {
        UIDebug = GameObject.Find("UI_Debug");
        UIDebug.SetActive(debug_panel);
        PlayerPrefs.SetFloat("TribeAge", 0.0f); // keep previous experience when loading/saving is done
        TribeAge = GameObject.Find("TribeAge").GetComponent<Text>();
        TribeSprite = GameObject.Find("Tribe").GetComponent<SpriteRenderer>();
        TribeScript = GameObject.Find("Tribe").GetComponent<TribeScript>();
        Time.timeScale = 1.0f;
        Fog = GameObject.Find("Fog").GetComponent<SpriteRenderer>();
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

        if (temp_time > 8760) { // years
            years = (int)temp_time / 8760;
            temp_time = temp_time % 8760;
        }
        if (temp_time > 720) {  // months
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
        PlayerPrefs.SetFloat("TribeAge", time);
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

    void FixedUpdate()
    {
        TribeAge.text = "Tribe's Age : " + format_Time((float)Time.time); // shows the tribe's age
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.C) && (GameObject.Find("CharacterPanel") == null))
        {
            SceneManager.LoadScene("CharacterPanel", LoadSceneMode.Additive);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            debug_panel = !debug_panel;
            UIDebug.SetActive(debug_panel);
        }

    }
    
}
