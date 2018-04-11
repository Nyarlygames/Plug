using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public SaveManagerScript SaveManager = new SaveManagerScript();
    public LoadChunk LoadManager = new LoadChunk();
    public GameObject TribeGO;
    public TribeGO tribe;
    public GameObject UIEscape;
    public GameObject UIChar;
    public List<GameObject> TilesGO = new List<GameObject>();
    public List<GameObject> ObjectsGO = new List<GameObject>();
    public TribeMembersGO TMGO;
    public SaveData sdata;
    public float scaleBeforeEscape = 0.0f;
    public Text timers;
    public float ZGround = 0.0f;
    public float ZObjects = 10.0f;
    public float ZCharacters = 0.0f;
    public float timeSinceReload = 0.0f;
    public EventsManager EM;//= new EventsManager();
    public RatioFactory RF;
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

    Sprite[] resource_type;
    public Dictionary<string, Sprite> resource_types = new Dictionary<string, Sprite>();
    public Dictionary<string, int> prefabs_name = new Dictionary<string, int>();

    public Sprite MouseCursor;
    public Sprite MouseCursorTarget;
    public GameObject MouseCursorGO;
    public GameObject MouseCursorTargetGO;

    public List<ObjectGO> visitedTrig = new List<ObjectGO>();
    Vector3 MouseCoords = Vector3.zero;

    public List<GameObject> Prefabs = new List<GameObject>();

    void Start()
    {
        MouseCursorGO = new GameObject("MouseCursor");
        MouseCursor = Resources.Load<Sprite>("Play/cursor");
        MouseCursorGO.AddComponent<SpriteRenderer>();
        MouseCursorGO.GetComponent<SpriteRenderer>().sprite = MouseCursor;

        MouseCursorTargetGO = new GameObject("MouseCursorTarget");
        MouseCursorTarget = Resources.Load<Sprite>("Play/cursor_dropped");
        MouseCursorTargetGO.AddComponent<SpriteRenderer>();
        MouseCursorTargetGO.GetComponent<SpriteRenderer>().sprite = MouseCursorTarget;
        MouseCursorTargetGO.SetActive(false);
        
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

        GameObject Desert = Resources.Load<GameObject>("Play/Prefabs/Desert_Prefab");
        GameObject Water = Resources.Load<GameObject>("Play/Prefabs/Water_Prefab");
        GameObject Grass00 = Resources.Load<GameObject>("Play/Prefabs/Grass00_Prefab");
        GameObject Grass01 = Resources.Load<GameObject>("Play/Prefabs/Grass01_Prefab");
        GameObject Grass02 = Resources.Load<GameObject>("Play/Prefabs/Grass02_Prefab");
        GameObject Grass03 = Resources.Load<GameObject>("Play/Prefabs/Grass03_Prefab");
        GameObject Grass04 = Resources.Load<GameObject>("Play/Prefabs/Grass04_Prefab");
        GameObject Grass05 = Resources.Load<GameObject>("Play/Prefabs/Grass05_Prefab");
        GameObject Grass06 = Resources.Load<GameObject>("Play/Prefabs/Grass06_Prefab");
        GameObject Grass07 = Resources.Load<GameObject>("Play/Prefabs/Grass07_Prefab");
        GameObject Grass08 = Resources.Load<GameObject>("Play/Prefabs/Grass08_Prefab");
        GameObject Grass09 = Resources.Load<GameObject>("Play/Prefabs/Grass09_Prefab");
        GameObject Grass10 = Resources.Load<GameObject>("Play/Prefabs/Grass10_Prefab");
        GameObject Grass11 = Resources.Load<GameObject>("Play/Prefabs/Grass11_Prefab");
        GameObject Grass12 = Resources.Load<GameObject>("Play/Prefabs/Grass12_Prefab");
        GameObject Trees = Resources.Load<GameObject>("Play/Prefabs/Trees_Prefab");
        GameObject Rock1 = Resources.Load<GameObject>("Play/Prefabs/Rock1_Prefab");
        GameObject Rock2 = Resources.Load<GameObject>("Play/Prefabs/Rock2_Prefab");
        GameObject Water02 = Resources.Load<GameObject>("Play/Prefabs/Water02_Prefab");
        GameObject Herbs = Resources.Load<GameObject>("Play/Prefabs/Herbs_Prefab");
        GameObject Clam = Resources.Load<GameObject>("Play/Prefabs/Clam_Prefab");
        GameObject Fish = Resources.Load<GameObject>("Play/Prefabs/Fish_Prefab");
        GameObject Insect = Resources.Load<GameObject>("Play/Prefabs/Insect_Prefab");
        GameObject Meat = Resources.Load<GameObject>("Play/Prefabs/Meat_Prefab");
        GameObject Mushroom = Resources.Load<GameObject>("Play/Prefabs/Mushroom_Prefab");
        GameObject Potato = Resources.Load<GameObject>("Play/Prefabs/Potato_Prefab");
        GameObject Puma = Resources.Load<GameObject>("Play/Prefabs/Puma_Prefab");
        GameObject Source = Resources.Load<GameObject>("Play/Prefabs/Source_Prefab");
        GameObject Spawner = Resources.Load<GameObject>("Play/Prefabs/Spawner_Prefab");
        GameObject Loader = Resources.Load<GameObject>("Play/Prefabs/Loader_Prefab");
        Prefabs.Add(Instantiate(Desert, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Water, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass00, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass01, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass02, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass03, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass04, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass05, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass06, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass07, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass08, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass09, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass10, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass11, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Grass12, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Trees, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Rock1, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Rock2, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Water02, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Herbs, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Clam, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Fish, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Insect, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Meat, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Mushroom, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Potato, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Puma, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Source, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Spawner, Vector3.zero, Quaternion.identity) as GameObject);
        Prefabs.Add(Instantiate(Loader, Vector3.zero, Quaternion.identity) as GameObject);

        prefabs_name.Add("Desert", 0);
        prefabs_name.Add("Water", 1);
        prefabs_name.Add("Grass00", 2);
        prefabs_name.Add("Grass01", 3);
        prefabs_name.Add("Grass02", 4);
        prefabs_name.Add("Grass03", 5);
        prefabs_name.Add("Grass04", 6);
        prefabs_name.Add("Grass05", 7);
        prefabs_name.Add("Grass06", 8);
        prefabs_name.Add("Grass07", 9);
        prefabs_name.Add("Grass08", 10);
        prefabs_name.Add("Grass09", 11);
        prefabs_name.Add("Grass10", 12);
        prefabs_name.Add("Grass11", 13);
        prefabs_name.Add("Grass12", 14);
        prefabs_name.Add("Trees", 15);
        prefabs_name.Add("Rock1", 16);
        prefabs_name.Add("Rock2", 17);
        prefabs_name.Add("Water02", 18);
        prefabs_name.Add("Herbs", 19);
        prefabs_name.Add("Clam", 20);
        prefabs_name.Add("Fish", 21);
        prefabs_name.Add("Insect", 22);
        prefabs_name.Add("Meat", 23);
        prefabs_name.Add("Mushroom", 24);
        prefabs_name.Add("Potato", 25);
        prefabs_name.Add("Puma", 26);
        prefabs_name.Add("Source", 27);
        prefabs_name.Add("Spawner", 28);
        prefabs_name.Add("Loader", 29);

        //Prefabs[prefabs_name["Desert"]].SetActive(false);
        //Prefabs[prefabs_name["Grass"]].SetActive(false);
        //Prefabs[prefabs_name["Water"]].SetActive(false);


        resource_type = Resources.LoadAll<Sprite>("Play/resources_type/");
        for (int i = 0; i < resource_type.Length; i++)
        {
            resource_types[resource_type[i].name] = resource_type[i];
        }

        /* Tribe */
        timeSinceReload = Time.timeSinceLevelLoad;
        timers = GameObject.Find("Timers").GetComponent<Text>();
        if (PlayerPrefs.GetString("savefile") != "")
        {
            sdata = SaveManager.LoadSave(PlayerPrefs.GetString("savefile")); // load savegame state
            if ((sdata != null))
            {
                SaveManager.LoadMap(sdata.mapfile, map);
                SaveManager.LoadMapGO(map, TilesGO, ObjectsGO, sdata.mapsave.objects, Prefabs);
                if (EM == null)
                {
                    EM = new EventsManager();
                    EM.GM = this;
                    EM.events = sdata.eventsave;
                }
                TribeGO = SaveManager.LoadGO(sdata); // create tribe go
                RF = TribeGO.GetComponent<TribeGO>().tribeCurrent.RF;
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
            SaveManager.LoadMapGO(map, TilesGO, ObjectsGO, sdata.mapsave.objects, Prefabs);
            EM = new EventsManager();
            EM.GM = this;
            GameObject NewTribeGO = GameObject.Find("newTribeGO");
            if (NewTribeGO != null)
            {
                newTribeGO NewTribe = NewTribeGO.GetComponent<newTribeGO>();
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
        GameObject Menus = GameObject.Find("UI_Panel");
        UIChar = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_CharPanel"), Vector3.zero, Quaternion.identity);
        UIChar.name = "UI_CharPanel";
        UIChar.transform.SetParent(Menus.transform);
        UIEscape = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_EscapePanel"), Vector3.zero, Quaternion.identity);
        UIEscape.name = "UI_EscapePanel";
        UIEscape.transform.SetParent(Menus.transform);
        tribe = TribeGO.GetComponent<TribeGO>();
        TMGO = GameObject.Find("Tribe_Members").GetComponent<TribeMembersGO>();

        if (tribe != null)
        {
            if (tribe.tribeCurrent.nomadism == false)
            {
                GameObject.Find("UI_Ingame_Nomadism_T").GetComponent<Text>().text = "Nomadism : Off";
            }
            else
            {
                GameObject.Find("UI_Ingame_Nomadism_T").GetComponent<Text>().text = "Nomadism : On";
            }
        }

        //StartCoroutine("doLogic");

        //SceneManager.LoadSceneAsync("Load_Chunk", LoadSceneMode.Additive);
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Load_Chunk", LoadSceneMode.Additive);
        //asyncLoad.priority = 0;
        //  StartCoroutine(LoadYourAsyncScene());
    }

    

    public void doLogic()
    {
       /* Debug.Log("started");
        MapSave chunk2 = new MapSave();
        SaveData sdatachunk2 = new SaveData();
        SaveManager.LoadMap("Assets/Resources/Map/TestChunk2.tmx", chunk2); // create map from file
        List<GameObject> TilesChunk2 = new List<GameObject>();
        List<GameObject> ObjectsChunk2 = new List<GameObject>();
        SaveManager.LoadMapGO(map, TilesChunk2, ObjectsChunk2, sdatachunk2.mapsave.objects);
        Debug.Log("done");

        Debug.Log("started3");
        MapSave chunk3 = new MapSave();
        SaveData sdatachunk3 = new SaveData();
        SaveManager.LoadMap("Assets/Resources/Map/TestChunk3.tmx", chunk3); // create map from file
        List<GameObject> TilesChunk3 = new List<GameObject>();
        List<GameObject> ObjectsChunk3 = new List<GameObject>();
        SaveManager.LoadMapGO(map, TilesChunk3, ObjectsChunk3, sdatachunk3.mapsave.objects);
        Debug.Log("done");

        Debug.Log("started4");
        MapSave chunk4 = new MapSave();
        SaveData sdatachunk4 = new SaveData();
        SaveManager.LoadMap("Assets/Resources/Map/TestChunk4.tmx", chunk4); // create map from file
        List<GameObject> TilesChunk4 = new List<GameObject>();
        List<GameObject> ObjectsChunk4 = new List<GameObject>();
        SaveManager.LoadMapGO(map, TilesChunk4, ObjectsChunk4, sdatachunk4.mapsave.objects);
        Debug.Log("done");*/
    }
   /* IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
           // Color c = renderer.material.color;
            c.a = f;
            //renderer.material.color = c;
            yield return null;
        }
    }*/


    void Update()
    {
        timeSinceReload += Time.deltaTime;
        timers.text = "Save time : " + (int)tribe.curAge.hours + " hours " + (int)tribe.curAge.days + " days.";
        timers.text += "\nSession time : " + (int)timeSinceReload + " hours " + (int)timeSinceReload / 24  + " days.";
        MouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseCoords.z = ZCharacters;
        MouseCursorGO.GetComponent<Transform>().position = MouseCoords;
        if (Input.GetKeyDown(KeyCode.C)) // open char panel
        {
            if (!UIEscape.activeSelf)
            {
                UIChar.SetActive(!UIChar.activeSelf);
                if (UIChar.activeSelf)
                {
                    GameObject TrigInfo = GameObject.Find("Trigger_Info");
                    if (TrigInfo != null)
                        Destroy(TrigInfo);
                    UIChar.GetComponent<PanelChar>().SetExistingChars();
                }
            }
        }
        if (Input.GetKey(KeyCode.H)) // open escape
        {
            foreach(ObjectGO go in visitedTrig.FindAll(go => (go.trigger_type != null) && (go.trigger_type.activeSelf == false)))
            {
                go.trigger_type.SetActive(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            foreach (ObjectGO go in visitedTrig.FindAll(go => (go.trigger_type != null)))
            {
                go.trigger_type.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // open escape
        {
            if (!UIEscape.activeSelf)
            {
                scaleBeforeEscape = Time.timeScale;
                Time.timeScale = 0.0f;
                GameObject TrigInfo = GameObject.Find("Trigger_Info");
                if (TrigInfo != null)
                    Destroy(TrigInfo);
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
        if (tribe.tribeCurrent.nomadism == true)
        {
            if (Input.GetMouseButtonDown(1)) // right click to move tribe
            {
                MouseCursorTargetGO.SetActive(true);
                MouseCursorTargetGO.GetComponent<Transform>().position = MouseCoords;
                TMGO.MoveTo(MouseCoords);
            }
            if (Input.GetMouseButtonDown(0) || (TMGO.MoveToTarget == Vector3.zero)) // move tribe on click
            {
                TMGO.MoveTo(Vector3.zero);
                MouseCursorTargetGO.SetActive(false);
            }
            if ((TMGO.TribeTransform != null) && (TMGO.MoveToTarget != Vector3.zero))
            {
                tribe.tribeCurrent.TribePosX = TMGO.TribeTransform.position.x;
                tribe.tribeCurrent.TribePosY = TMGO.TribeTransform.position.y;
            }
        }
        else
        {
            TMGO.MoveTo(Vector3.zero);
            MouseCursorTargetGO.SetActive(false);
        }
    }
    

    void CreateTribeGO(newTribeGO NGTribe)
    {
        TribeSave newtribe = new TribeSave();
        newtribe.tribename = NGTribe.tribeCurrent.tribename;
        newtribe.SetAge();
        TribeGO = new GameObject(newtribe.tribename);
        TribeGO.AddComponent<TribeGO>();
        TribeGO tribecomp = TribeGO.GetComponent<TribeGO>();
        tribecomp.tribeCurrent = newtribe;
        tribecomp.profilename = "newgame"; // options ?
        GameObject Tribe_Fire = new GameObject("Tribe_Fire");
        Tribe_Fire.AddComponent<SpriteRenderer>();
        Sprite[] Fire = Resources.LoadAll<Sprite>("Play/Feu_Spritesheet");
        Tribe_Fire.GetComponent<SpriteRenderer>().sprite = Fire[0];
        GameObject Tribe_Members = new GameObject("Tribe_Members");
        Tribe_Members.AddComponent<SpriteRenderer>();
        Tribe_Members.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Play/Sprite_Camp");
        Tribe_Members.AddComponent<BoxCollider2D>();
        Tribe_Members.AddComponent<TribeMembersGO>();
        GameObject Tribe_Radius = new GameObject("Tribe_Radius");
        Tribe_Radius.AddComponent<CircleCollider2D>();
        RatioFactory RF = new RatioFactory();
        Tribe_Radius.GetComponent<CircleCollider2D>().radius = RF.tribe_sightradius;
        Tribe_Radius.AddComponent<TribeRadiusGO>();
        Tribe_Radius.GetComponent<CircleCollider2D>().isTrigger = true;
        Tribe_Radius.AddComponent<SpriteRenderer>();
        Sprite Radius = Resources.Load<Sprite>("Play/radius");
        Tribe_Radius.GetComponent<SpriteRenderer>().sprite = Radius;
        Tribe_Radius.GetComponent<Transform>().SetParent(Tribe_Members.GetComponent<Transform>());
        Tribe_Radius.GetComponent<Transform>().position = new Vector3(Tribe_Radius.GetComponent<Transform>().position.x, Tribe_Radius.GetComponent<Transform>().position.y, ZGround);
        Tribe_Radius.GetComponent<Transform>().localScale = new Vector3(RF.tribe_sightradius, RF.tribe_sightradius, 1.0f);
        Tribe_Radius.tag = "radius";
        Tribe_Members.GetComponent<TribeMembersGO>().fire = Tribe_Fire;
        Tribe_Members.GetComponent<TribeMembersGO>().firesprites = Fire;
        Tribe_Members.GetComponent<TribeMembersGO>().radius = Tribe_Radius;
        Vector3 frontfire = new Vector3(Tribe_Members.GetComponent<Transform>().position.x, Tribe_Members.GetComponent<Transform>().position.y, Tribe_Members.GetComponent<Transform>().position.z - 0.5f);
        Tribe_Fire.GetComponent<Transform>().position = frontfire;
        Tribe_Fire.GetComponent<Transform>().SetParent(Tribe_Members.GetComponent<Transform>());
        for (int i = 0; i < NGTribe.tribeCurrent.members.Count; i++)
        {
            CreatePlayer(i, NGTribe.tribeCurrent.members[i].name, NGTribe.tribeCurrent.members[i], tribecomp, Tribe_Members);
        }
        sdata.tribesave = tribecomp.tribeCurrent;
        
    }

    public void CreatePlayer(int id, string name, CharacterSave cs, TribeGO tribecomp, GameObject Tribe_Members)
    {
      /*  int countdown = cs.age.years;
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
        */

        CharacterSave newman = new CharacterSave();
        newman.id = id;
        newman.name = name;
        newman.sexe = cs.sexe;
        newman.Pic_base = cs.Pic_base;
        newman.Pic_beard = cs.Pic_beard;
        newman.Pic_clothes = cs.Pic_clothes;
        newman.Pic_hairs = cs.Pic_hairs;
        newman.Pic_jewels = cs.Pic_jewels;
        newman.Pic_paints = cs.Pic_paints;
        newman.SetActivitiesList();
        newman.SetAbilities();
        newman.next = RF.level_basegrowing;
        newman.GainTime(cs.age, true);
        string debugline = "";
        foreach (KeyValuePair<string, int> stat in newman.abilities.abilities)
        {
            debugline += "\nchar " + name + " => stats: " + stat.Key + " / " + stat.Value;
        }
        debugline += "\n";
        foreach (KeyValuePair<string, int> stat in newman.abilities.affinities)
        {
            debugline += "\nchar " + name + " => manualpoints: " + stat.Key + " / " + stat.Value;
        }
        debugline += "\n";
        foreach (KeyValuePair<string, int> statact in newman.activity_history)
        {
            debugline += "\nchar " + name + " => activity: " + statact.Key + " / history number " + statact.Value;
        }
        debugline += "\n";
        for (int i = 0; i < newman.abilities.abilities_history.Count; i++)
        {
            foreach (KeyValuePair<string, IDictionary<string, int>> stat in newman.abilities.abilities_history[i])
            {
                foreach (KeyValuePair<string, int> statact in stat.Value)
                {
                    debugline += "\nchar " + name + " => stat: " + stat.Key + " / activity :  " + statact.Key + " / value : " + statact.Value;
                }
            }
        }
        Debug.Log(debugline);
        newman.food = ((newman.strength * RF.ratio_food_strength) + (newman.body * RF.ratio_food_body) + (newman.endu * RF.ratio_food_endu)) / 100;
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
        tribecomp.tribeCurrent.TribePosX = Tribe_Members.GetComponent<Transform>().position.x;
        tribecomp.tribeCurrent.TribePosY = Tribe_Members.GetComponent<Transform>().position.y;
        newman.x = Tribe_Members.GetComponent<Transform>().position.x /*+ id * 4*/;
        newman.y = Tribe_Members.GetComponent<Transform>().position.y;
        newman.z = ZCharacters;
        CharGO.GetComponent<Transform>().position = new Vector3(newman.x, newman.y, newman.z);
        CharGO.transform.SetParent(Tribe_Members.transform);
        
        //CharGO.GetComponent<SpriteRenderer>().enabled = false;
        tribecomp.CharsGO.Add(CharGO);
    }
    /*
    CharacterSave simulateActivity(CharacterSave cs)
    {
        List<string> Activities = new List<string>();
        Activities.Add("hunt");
        Activities.Add("source");
        Activities.Add("fish");
        Activities.Add("gather");
        if (cs.age.years > RF.adult_age)
        {
            int simulateddays = (cs.age.years - RF.adult_age) * 365;
            for (int i=0; i < simulateddays; i++)
            {
                int activity = UnityEngine.Random.Range(0, Activities.Count);
                int success = UnityEngine.Random.Range(0, 10);
                if (success <= 7)
                {
                    cs.AddActivityGain(Activities[activity]);
                }
                cs.SetActivities();
            }
        }
        return cs;
    }*/
}
