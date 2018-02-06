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
    public float timeSinceReload = 0.0f;
    RatioFactory RF = new RatioFactory();

    void Start()
    {
        timeSinceReload = Time.timeSinceLevelLoad;
        timers = GameObject.Find("Timers").GetComponent<Text>();
        if (PlayerPrefs.GetString("savefile") != "")
        {
            sdata = SaveManager.LoadSave(PlayerPrefs.GetString("savefile")); // load savegame state
            if ((sdata != null))
            {
                TribeGO = SaveManager.LoadGO(sdata); // create tribe go
                GameObject.Find("UI_SaveName").GetComponent<Text>().text = "Save name : " + PlayerPrefs.GetString("savefile");
            }
            else
                Debug.Log("Failed loading : " + PlayerPrefs.GetString("savefile"));
        }
        else
        {
            //new game without save file
            CreateTribeGO("newgame");
            GameObject.Find("UI_SaveName").GetComponent<Text>().text = "New tribe";
            Debug.Log("Loaded : new game");
        }
        UIEscape = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_EscapePanel"), Vector3.zero, Quaternion.identity);
        UIChar = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_CharPanel"), Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        timeSinceReload += Time.deltaTime;
        timers.text = "Save time : " + (int) TribeGO.GetComponent<TribeGO>().curAge.hours + " hours " + (int) TribeGO.GetComponent<TribeGO>().curAge.days + " days.";
        timers.text += "\nSession time : " + (int)timeSinceReload + " hours " + (int)timeSinceReload / 24  + " days.";
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

                CreatePlayer(0, "father 30", 30, "Play/TribeChar/man", tribecomp);
                CreatePlayer(1, "mother 36", 36, "Play/TribeChar/woman", tribecomp);
                CreatePlayer(2, "woman 16", 16, "Play/TribeChar/Woman1", tribecomp);
                CreatePlayer(3, "baby 0", 0, "Play/TribeChar/Son", tribecomp);
                CreatePlayer(4, "daughter 8", 8, "Play/TribeChar/daughter", tribecomp);
                CreatePlayer(5, "son 11", 11, "Play/TribeChar/Son", tribecomp);

                sdata = new SaveData();
                sdata.tribesave = tribecomp.tribeCurrent;
                sdata.savefile = "noname";
                break;
            default:
                break;
        }
    }

    public void CreatePlayer(int id, string name, int age, string sprite, TribeGO tribecomp)
    {
        int countdown = age;
        float xpacc = 0;
        if (countdown > RF.exp_adult_range)
        {
            xpacc -= 0.5f * (((countdown - (int) RF.exp_adult_range) * 365) / 200) ;
            countdown = (int)RF.exp_adult_range;
        }
        if (countdown > RF.exp_teen_range)
        {
            xpacc += RF.exp_adult_value * (countdown - RF.exp_teen_range) * 365;
            countdown = (int)RF.exp_teen_range;
        }
        if (countdown > RF.exp_baby_range)
        {
            xpacc += RF.exp_teen_value * (countdown - RF.exp_baby_range) * 365;
            countdown = (int)RF.exp_baby_range;
        }
        if (countdown > 0)
        {
            xpacc += RF.exp_baby_value * countdown *365;
        }

        CharacterSave newman = new CharacterSave();
        newman.id = id;
        newman.name = name;
        newman.time = age * 24 * 365;
        newman.xp = xpacc;
        newman.SetAge();
        newman.next = 150.0f;
        newman.next = newman.SkipStats(newman.xp, newman.next);
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
