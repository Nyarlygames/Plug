using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;


public class GameManager : MonoBehaviour
{
    public SaveManagerScript SaveManager = new SaveManagerScript();
    public GameObject TribeGO;
    public GameObject UIEscape;
    public GameObject UIChar;
    public List<GameObject> TilesGO = new List<GameObject>();
    public List<GameObject> ObjectsGO = new List<GameObject>();
    public SaveData sdata;
    public float scaleBeforeEscape = 0.0f;
    public Text timers;
    public float ZGround = 0.0f;
    public float ZObjects = -1.0f;
    public float ZCharacters=-2.0f;
    public float timeSinceReload = 0.0f;
    RatioFactory RF = new RatioFactory();
    public MapSave map = new MapSave();
    //public List<Sprite> spritelist = new List<Sprite>();

    void Start()
    {
        /* Tribe */
        timeSinceReload = Time.timeSinceLevelLoad;
        timers = GameObject.Find("Timers").GetComponent<Text>();
        if (PlayerPrefs.GetString("savefile") != "")
        {
            sdata = SaveManager.LoadSave(PlayerPrefs.GetString("savefile")); // load savegame state
            if ((sdata != null))
            {
                SaveManager.LoadMap(sdata.mapfile, map);
                SaveManager.LoadMapGO(map, TilesGO, ObjectsGO);
                TribeGO = SaveManager.LoadGO(sdata); // create tribe go
                GameObject.Find("UI_SaveName").GetComponent<Text>().text = sdata.tribesave.tribename;
            }
            else
                Debug.Log("Failed loading : " + PlayerPrefs.GetString("savefile"));
        }
        else
        {
            //new game without save file
            sdata = new SaveData();

            sdata.savefile = "";
            sdata.savefolder = "Save/";
            sdata.mapfile = PlayerPrefs.GetString("mapfile");
            SaveManager.LoadMap(sdata.mapfile, map); // create map from file
            SaveManager.LoadMapGO(map, TilesGO, ObjectsGO);

            GameObject NewTribeGO = GameObject.Find("newTribeGO");
            if (NewTribeGO != null)
            {
                TribeGO NewTribe = NewTribeGO.GetComponent<TribeGO>();
                CreateTribeGO(NewTribe);
                GameObject.Find("UI_SaveName").GetComponent<Text>().text = PlayerPrefs.GetString("NewName");
                TribeGO.GetComponent<TribeGO>().tribeCurrent.tribename = PlayerPrefs.GetString("NewName");

                Destroy(NewTribeGO);
            }
            else
            {
                TribeGO = new GameObject("Tribe_Members");
                TribeGO.AddComponent<TribeGO>();
                TribeGO.GetComponent<TribeGO>().tribeCurrent = new TribeSave();
            }
            Debug.Log("Loaded : new game");
        }
        GameObject Menus = new GameObject("Menus");
        UIEscape = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_EscapePanel"), Vector3.zero, Quaternion.identity);
        UIEscape.name = "UI_EscapePanel";
        UIEscape.transform.SetParent(Menus.transform);
        UIChar = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_CharPanel"), Vector3.zero, Quaternion.identity);
        UIChar.name = "UI_CharPanel";
        UIChar.transform.SetParent(Menus.transform);
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

    void CreateTribeGO(TribeGO NGTribe)
    {
        TribeSave newtribe = new TribeSave();
        newtribe.tribename = NGTribe.tribeCurrent.tribename;
        newtribe.SetAge();
        TribeGO = new GameObject(newtribe.tribename);
        TribeGO.AddComponent<TribeGO>();
        TribeGO tribecomp = TribeGO.GetComponent<TribeGO>();
        tribecomp.tribeCurrent = newtribe;
        tribecomp.profilename = "newgame"; // options ?
        GameObject Tribe_Members = new GameObject("Tribe_Members");
        for (int i = 0; i < NGTribe.tribeCurrent.members.Count; i++)
        {
            // save pic
            if (Directory.Exists("Assets/Resources/Save/newgame/") == false)
            {
                Directory.CreateDirectory("Assets/Resources/Save/newgame/");
            }
            byte[] bytes = NGTribe.customchars[i].EncodeToPNG();
            FileStream file = File.Open("Assets/Resources/Save/newgame/customchar_" + i + ".png", FileMode.OpenOrCreate);
            BinaryWriter binary = new BinaryWriter(file);
            binary.Write(bytes);
            file.Close();
            CreatePlayer(i, NGTribe.tribeCurrent.members[i].name, NGTribe.tribeCurrent.members[i], NGTribe.customchars[i], tribecomp, Tribe_Members);
        }
        /*CreatePlayer(0, "father 30", 30, "Play/TribeChar/man", tribecomp, Tribe_Members);
        CreatePlayer(1, "mother 36", 36, "Play/TribeChar/woman", tribecomp, Tribe_Members);
        CreatePlayer(2, "woman 16", 16, "Play/TribeChar/Woman1", tribecomp, Tribe_Members);
        CreatePlayer(3, "baby 0", 0, "Play/TribeChar/Son", tribecomp, Tribe_Members);
        CreatePlayer(4, "daughter 8", 8, "Play/TribeChar/daughter", tribecomp, Tribe_Members);
        CreatePlayer(5, "son 11", 11, "Play/TribeChar/Son", tribecomp, Tribe_Members);*/

        sdata.tribesave = tribecomp.tribeCurrent;
    }

    public void CreatePlayer(int id, string name, CharacterSave cs, Texture2D sprite, TribeGO tribecomp, GameObject Tribe_Members)
    {
        int countdown = cs.age.years;
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
        newman.time = cs.age.years * 24 * 365;
        newman.sexe = cs.sexe;
        newman.xp = xpacc;
        newman.SetAge();
        newman.next = 150.0f;
        newman.next = newman.SkipStats(newman.xp, newman.next);
        newman.charSprite = "Save/newgame/customchar_" + id;
        tribecomp.tribeCurrent.members.Add(newman);
        GameObject CharGO = new GameObject(newman.name);
        CharGO.AddComponent<CharacterGO>();
        CharGO.GetComponent<CharacterGO>().charCurrent = newman;
        CharGO.AddComponent<SpriteRenderer>();
        Sprite FaceSprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), new Vector2(0.5f, 0.5f));
        CharGO.GetComponent<SpriteRenderer>().sprite = FaceSprite;
        // CharGO.GetComponent<Transform>().position = new Vector3(map.tilesizex * map.sizex / 100, map.tilesizey * map.sizey / 100, 0.0f);
        newman.x = id*4;
        newman.y = 3;
        newman.z = ZCharacters;
        CharGO.GetComponent<Transform>().position = new Vector3(newman.x, newman.y, newman.z);
        
        CharGO.transform.SetParent(Tribe_Members.transform);
        tribecomp.CharsGO.Add(CharGO);
    }
}
