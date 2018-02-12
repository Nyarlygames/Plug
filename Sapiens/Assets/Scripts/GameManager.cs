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
    public float ZObjects = 10.0f;
    public float ZCharacters = 0.0f;
    public float timeSinceReload = 0.0f;
    RatioFactory RF = new RatioFactory();
    public MapSave map = new MapSave();
    public Sprite[] basesF;
    public Sprite[] basesM;
    public Sprite[] hairsF;
    public Sprite[] hairsM;
    public Sprite[] jewelsF;
    public Sprite[] beardsM;
    public Sprite[] paintsF;
    public Sprite[] paintsM;
    public Sprite[] clothesF;
    public Sprite[] clothesM;

    void Start()
    {
        ZGround = 20.0f;
        ZObjects = 10.0f;
        ZCharacters = 0.0f;
        basesF = Resources.LoadAll<Sprite>("Play/CharCustom/Females/Bases/");
        basesM = Resources.LoadAll<Sprite>("Play/CharCustom/Males/Bases/");
        hairsF = Resources.LoadAll<Sprite>("Play/CharCustom/Females/Hairs/");
        hairsM = Resources.LoadAll<Sprite>("Play/CharCustom/Males/Hairs/");
        jewelsF = Resources.LoadAll<Sprite>("Play/CharCustom/Females/Jewels/");
        beardsM = Resources.LoadAll<Sprite>("Play/CharCustom/Males/Beards/");
        paintsF = Resources.LoadAll<Sprite>("Play/CharCustom/Females/Paints/");
        paintsM = Resources.LoadAll<Sprite>("Play/CharCustom/Males/Paints/");
        clothesF = Resources.LoadAll<Sprite>("Play/CharCustom/Females/Clothes/");
        clothesM = Resources.LoadAll<Sprite>("Play/CharCustom/Males/Clothes/");

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
                {
                    UIChar.GetComponent<PanelChar>().SetExistingChars();
                }
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
            CreatePlayer(i, NGTribe.tribeCurrent.members[i].name, NGTribe.tribeCurrent.members[i], tribecomp, Tribe_Members);
        }
        sdata.tribesave = tribecomp.tribeCurrent;
    }

    public void CreatePlayer(int id, string name, CharacterSave cs, TribeGO tribecomp, GameObject Tribe_Members)
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
        newman.Pic_base = cs.Pic_base;
        newman.Pic_beard = cs.Pic_beard;
        newman.Pic_clothes = cs.Pic_clothes;
        newman.Pic_hairs = cs.Pic_hairs;
        newman.Pic_jewels = cs.Pic_jewels;
        newman.Pic_paints = cs.Pic_paints;
        newman.SetAge();
        newman.next = 150.0f;
        newman.next = newman.SkipStats(newman.xp, newman.next);
        tribecomp.tribeCurrent.members.Add(newman);
        GameObject CharGO = new GameObject(newman.name);
        CharGO.AddComponent<CharacterGO>();
        CharGO.GetComponent<CharacterGO>().charCurrent = newman;


        GameObject body = new GameObject(newman.name + "_Body");
        Transform bodyT = body.GetComponent<Transform>();
        bodyT.SetParent(CharGO.GetComponent<Transform>());
        bodyT.position = new Vector3(bodyT.position.x, bodyT.position.y, ZCharacters + 5);
        body.AddComponent<SpriteRenderer>();
        Sprite FaceSprite;
        if (cs.sexe == 0)
            FaceSprite = basesF[cs.Pic_base];
        else
            FaceSprite = basesM[cs.Pic_base];
        body.GetComponent<SpriteRenderer>().sprite = FaceSprite;

        GameObject paints = new GameObject(newman.name + "_Paints");
        Transform paintsT = paints.GetComponent<Transform>();
        paintsT.SetParent(CharGO.GetComponent<Transform>());
        paintsT.position = new Vector3(paintsT.position.x, paintsT.position.y, ZCharacters + 4);
        paints.AddComponent<SpriteRenderer>();
        Sprite PaintSprite;
        if (cs.sexe == 0)
            PaintSprite = paintsF[cs.Pic_paints];
        else
            PaintSprite = paintsM[cs.Pic_paints];
        paints.GetComponent<SpriteRenderer>().sprite = PaintSprite;

        GameObject hairs = new GameObject(newman.name + "_Hairs");
        Transform hairsT = hairs.GetComponent<Transform>();
        hairsT.SetParent(CharGO.GetComponent<Transform>());
        hairsT.position = new Vector3(hairsT.position.x, hairsT.position.y, ZCharacters + 3);
        hairs.AddComponent<SpriteRenderer>();
        Sprite HairsSprite;
        if (cs.sexe == 0)
            HairsSprite = hairsF[cs.Pic_hairs];
        else
            HairsSprite = hairsM[cs.Pic_hairs];
        hairs.GetComponent<SpriteRenderer>().sprite = HairsSprite;

        GameObject extra = new GameObject(newman.name + "_Extra");
        Transform extraT = extra.GetComponent<Transform>();
        extraT.SetParent(CharGO.GetComponent<Transform>());
        extraT.position = new Vector3(extraT.position.x, extraT.position.y, ZCharacters + 2);
        extra.AddComponent<SpriteRenderer>();
        Sprite ExtraSprite;
        if (cs.sexe == 0)
            ExtraSprite = jewelsF[cs.Pic_jewels];
        else
            ExtraSprite = beardsM[cs.Pic_beard];
        extra.GetComponent<SpriteRenderer>().sprite = ExtraSprite;

        GameObject clothes = new GameObject(newman.name + "_Clothes");
        Transform clothesT = clothes.GetComponent<Transform>();
        clothesT.SetParent(CharGO.GetComponent<Transform>());
        clothesT.position = new Vector3(clothesT.position.x, clothesT.position.y, ZCharacters + 1);
        clothes.AddComponent<SpriteRenderer>();
        Sprite ClothesSprite;
        if (cs.sexe == 0)
            ClothesSprite = clothesF[cs.Pic_clothes];
        else
            ClothesSprite = clothesM[cs.Pic_clothes];
        clothes.GetComponent<SpriteRenderer>().sprite = ClothesSprite;

        paints.GetComponent<SpriteRenderer>().enabled = false;
        extra.GetComponent<SpriteRenderer>().enabled = false;
        clothes.GetComponent<SpriteRenderer>().enabled = false;
        hairs.GetComponent<SpriteRenderer>().enabled = false;
        body.GetComponent<SpriteRenderer>().enabled = false;


        // CharGO.GetComponent<Transform>().position = new Vector3(map.tilesizex * map.sizex / 100, map.tilesizey * map.sizey / 100, 0.0f);

        if (PlayerPrefs.GetInt("spawnzone") > 0)
        {
            foreach (ObjectSave obj in map.objects)
            {
                if (obj.modifiers.ContainsKey("spawner"))
                {
                    if (Convert.ToInt32(obj.modifiers["spawner"]) == PlayerPrefs.GetInt("spawnzone"))
                    {
                        if (obj.y % 2 == 1)
                            Tribe_Members.GetComponent<Transform>().position = new Vector3((obj.x + obj.offsetx + obj.width / 2) / 100.0f, ((map.sizey * map.tilesizey / 2) - ((obj.y + obj.offsety - obj.height / 2.0f))) / 100.0f, ZCharacters);
                        else
                            Tribe_Members.GetComponent<Transform>().position = new Vector3((obj.x + obj.offsetx + obj.width / 2) / 100.0f, ((map.sizey * map.tilesizey / 2) - ((obj.y + obj.offsety - obj.height / 2.0f))) / 100.0f + (map.tilesizey / 2.0f / 100.0f), ZCharacters);
                    }
                }
            }
            PlayerPrefs.SetInt("spawnzone", 0);

        }
        newman.x = Tribe_Members.GetComponent<Transform>().position.x /*+ id * 4*/;
        newman.y = Tribe_Members.GetComponent<Transform>().position.y;
        newman.z = ZCharacters;
        CharGO.GetComponent<Transform>().position = new Vector3(newman.x, newman.y, newman.z);
        CharGO.transform.SetParent(Tribe_Members.transform);
        
        //CharGO.GetComponent<SpriteRenderer>().enabled = false;
        tribecomp.CharsGO.Add(CharGO);
    }
}
