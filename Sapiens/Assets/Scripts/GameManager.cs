using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public SaveManagerScript SaveManager = new SaveManagerScript();
    public GameObject TribeGO;
    public GameObject UIEscape;
    public GameObject UIChar;
    public SaveData sdata;
    public float scaleBeforeEscape = 0.0f;
    public Text timers;

    void Start()
    {
        Time.timeScale = 3.5f;
        timers = GameObject.Find("Timers").GetComponent<Text>();
        if (PlayerPrefs.GetString("savefile") != "")
        {
            sdata = SaveManager.LoadSave(PlayerPrefs.GetString("savefile")); // load savegame state
            if ((sdata != null))
            {
                TribeGO = SaveManager.LoadGO(sdata); // create tribe go
                Debug.Log("Loaded : " + PlayerPrefs.GetString("savefile"));
            }
            else
                Debug.Log("Failed loading : " + PlayerPrefs.GetString("savefile"));
        }
        else
        {
            //new game without save file
            CreateTribeGO("newgame");
            Debug.Log("Loaded : new game");
        }
        UIEscape = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_EscapePanel"), Vector3.zero, Quaternion.identity);
        UIChar = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_CharPanel"), Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        timers.text = "Save time : " + TribeGO.GetComponent<TribeGO>().tribeCurrent.age.days + " days.";
        timers.text += "\nSession time : " + (int) Time.timeSinceLevelLoad / 24  + " days.";
        if (Input.GetKeyDown(KeyCode.C)) // open char panel
        {
            if (!UIEscape.activeSelf)
            {
                UIChar.SetActive(!UIChar.activeSelf);
                if (UIChar.activeSelf)
                    UIChar.GetComponent<PanelChar>().SetExistingChars();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // open escape
        {
            if (!UIEscape.activeSelf)
            {
                scaleBeforeEscape = Time.timeScale;
                Time.timeScale = 0.0f;
                UIEscape.SetActive(!UIEscape.activeSelf);
            }
            else // escape already opened, check for sub panels
            {
                if ((!UIEscape.GetComponent<PanelEscape>().UISave.activeSelf) && (!UIEscape.GetComponent<PanelEscape>().UILoad.activeSelf))
                {
                    Time.timeScale = scaleBeforeEscape;
                    UIEscape.SetActive(!UIEscape.activeSelf);
                }
            }
        }
    }

    void CreateTribeGO(string hackkey)
    {
        switch (hackkey)
        {
            case "newgame" :
                TribeSave newtribe = new TribeSave();
                newtribe.tribename = "NewTribe";
                newtribe.SetAge();
                TribeGO = new GameObject(newtribe.tribename);
                TribeGO.AddComponent<TribeGO>();
                TribeGO tribecomp = TribeGO.GetComponent<TribeGO>();
                tribecomp.tribeCurrent = newtribe;
                tribecomp.profilename = "newgame";

                CreatePlayer(0, "new man", 30, "Play/TribeChar/man", tribecomp);
                CreatePlayer(1, "new woman", 25, "Play/TribeChar/woman", tribecomp);

                sdata = new SaveData();
                sdata.tribesave = tribecomp.tribeCurrent;
                sdata.savefile = "noname";
                break;
            default:
                break;
        }
    }

    void CreatePlayer(int id, string name, int age, string sprite, TribeGO tribecomp)
    {
        CharacterSave newman = new CharacterSave();
        newman.id = id;
        newman.name = name;
        newman.time = age * 24 * 365;
        newman.SetAge();
        newman.charSprite = sprite;
        tribecomp.tribeCurrent.members.Add(newman);
        GameObject CharGO = new GameObject(newman.name);
        CharGO.AddComponent<CharacterGO>();
        CharGO.GetComponent<CharacterGO>().charCurrent = newman;
        CharGO.AddComponent<SpriteRenderer>();
        CharGO.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(newman.charSprite);
        tribecomp.CharsGO.Add(CharGO);
    }
}
