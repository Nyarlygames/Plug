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
    public int selectedChar = 0;
    public List<Transform> Charpics = new List<Transform>();
    public int timespeed = 0; // level 1/2/3 time speed

    void Start ()
    {
        PlayerPrefs.SetFloat("TribeAge", 0.0f); // keep previous experience when loading/saving is done
        TribeAge = GameObject.Find("TribeAge").GetComponent<Text>();
        TribeSprite = GameObject.Find("Tribe").GetComponent<SpriteRenderer>();
        TribeScript = GameObject.Find("Tribe").GetComponent<TribeScript>();
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



        string test = "";
        if (hours >= 10)
            test += hours + " hours, ";
        else
            test += "0" + hours + " hours, ";

        if (days >= 10)
            test += days + " days ,";
        else
            test += "0" + days + " days, ";

        if (weeks >= 10)
            test += weeks + " weeks, ";
        else
            test += "0" + weeks + " weeks, ";

        if (months >= 10)
            test += months + " months, ";
        else
            test += "0" + months + " months, ";

        if (years >= 10)
            test += years + " years";
        else
            test += "0" + years + " years";


        /*  if (temp_time > 86400)
          {
              days =(int) temp_time / 86400;
              temp_time /= 86400;
          }
          if (temp_time > 3600)
          {
              hours = (int)temp_time / 3600;
              temp_time /= 3600;
          }
          if (temp_time > 60)
          {
              minutes = (int)temp_time / 60;
              temp_time /= 60;
          }
          else if (temp_time < 60)
          {
              seconds = (int)temp_time;
          }*/

        PlayerPrefs.SetString("FormattedTime", test);


        PlayerPrefs.SetFloat("TribeAge", time);
        PlayerPrefs.SetString("TribeAgeUtc", System.DateTime.Now.ToString()); // system date, use to skip later.
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

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            switch (timespeed)
            {
                case 1:
                    timespeed = 0;
                    Time.timeScale = 1.0f; // - x1.0 speed
                    break;
                /*case 0:
                    Time.timeScale = 1.0f; // - x1.0 speed
                    break;
                case 2:
                    timespeed = 1;
                    Time.timeScale = 3.5f;
                    break;
                default:
                    Time.timeScale = 1.0f; // - x1.0 speed
                    break;*/
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            switch (timespeed)
            {
                case 0:
                    timespeed = 1;
                    Time.timeScale = 10.0f;
                    break;
                /*case 1:
                    timespeed = 2;
                    Time.timeScale =80.0f; // - x1.0 speed
                    break;
                default:
                    Time.timeScale = 1.0f; // - x1.0 speed
                    break;*/
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.C) && (GameObject.Find("CharacterPanel") == null))
        {
            SceneManager.LoadScene("CharacterPanel", LoadSceneMode.Additive);
        }
        
        
    }
    
}
