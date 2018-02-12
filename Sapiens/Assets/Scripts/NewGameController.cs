using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class NewGameController : MonoBehaviour {


    Sprite[] basesF;
    Sprite[] basesM;
    Sprite[] hairsF;
    Sprite[] hairsM;
    Sprite[] jewelsF;
    Sprite[] beardsM;
    Sprite[] paintsF;
    Sprite[] paintsM;
    Sprite[] clothesF;
    Sprite[] clothesM;
    
    GameObject newp;
    GameObject nbeards;
    GameObject pbeards;
    GameObject njewels;
    GameObject pjewels;
    GameObject nhairs;
    GameObject phairs;
    GameObject npaints;
    GameObject ppaints;
    GameObject nclothes;
    GameObject pclothes;
    GameObject nbase;
    GameObject pbase;
    GameObject newhairs;
    GameObject newclothes;
    GameObject newjewels;
    GameObject newpaints;
    GameObject newbase;
    GameObject newbeards;

    GameObject newTribeGO;
    TribeSave newTribe;
    GameObject newcharb;
    Text tribe_members;
    Text newtribename;
    Text newname;
    TribeGO tribecomp;

    Texture2D NewFace;
    Texture2D[] NewPicBase;

    Sprite ZoneOn;
    Sprite ZoneOff;
    Image ImgZone1;
    Image ImgZone2;
    Image ImgZone3;

    int selected_clothes = 0;
    int selected_beards = 0;
    int selected_hairs = 0;
    int selected_paints = 0;
    int selected_jewels = 0;
    int selected_bases = 0;

    int spawnzone = 1;

    int activesex = 0;
    
    void Start ()
    {

        NewPicBase = Resources.LoadAll<Texture2D>("Play/CharCustom/Females/Bases/");
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

        Object[] textures;
        //switch button texts or pic to correct language
        switch (PlayerPrefs.GetString("Lang"))
        {
            case "French":
                textures = Resources.LoadAll("NewGame/FR", typeof(Sprite));
                break;
            case "English":
                textures = Resources.LoadAll("NewGame/EN", typeof(Sprite));
                break;
            case "Spanish":
                textures = Resources.LoadAll("NewGame/SPA", typeof(Sprite));
                // NYI
                break;
            default:
                //English if not set
                textures = Resources.LoadAll("NewGame/EN", typeof(Sprite));
                break;
        }
        foreach (var t in textures)
        {
            /*if (t.name == "Back_NG")
            {
                GameObject.Find("Background").GetComponent<Image>().sprite = t as Sprite;
            }*/
            if (t.name == "Launch")
            {
                GameObject.Find("Launch").GetComponent<Image>().sprite = t as Sprite;
            }
            if (t.name == "BackPlay")
            {
                GameObject.Find("BackButton").GetComponent<Image>().sprite = t as Sprite;
            }
        }

        newcharb = GameObject.Find("NewChar_B");
        Button temp = GameObject.Find("Launch").GetComponent<Button>();
        temp.onClick.AddListener(LaunchButton_Click);
        temp = GameObject.Find("NewChar_B").GetComponent<Button>();
        temp.onClick.AddListener(NewChar_B_Click);
        temp = GameObject.Find("BackButton").GetComponent<Button>();
        temp.onClick.AddListener(BackButton_Click);
        temp = GameObject.Find("NewFemale_B").GetComponent<Button>();
        temp.onClick.AddListener(MakeFemale_Click);
        temp = GameObject.Find("NewMale_B").GetComponent<Button>();
        temp.onClick.AddListener(MakeMale_Click);
        temp = GameObject.Find("StartZone_Map1_B").GetComponent<Button>();
        temp.onClick.AddListener(StartZone1_Click);
        temp = GameObject.Find("StartZone_Map2_B").GetComponent<Button>();
        temp.onClick.AddListener(StartZone2_Click);
        temp = GameObject.Find("StartZone_Map3_B").GetComponent<Button>();
        temp.onClick.AddListener(StartZone3_Click);
        temp = GameObject.Find("Skip_B").GetComponent<Button>();
        temp.onClick.AddListener(Skip_B_Click);


        GameObject.Find("NextBeard_B").GetComponent<Button>().onClick.AddListener(nbeards_Click);
        GameObject.Find("PreviousBeard_B").GetComponent<Button>().onClick.AddListener(pbeards_Click);
        GameObject.Find("NextJewel_B").GetComponent<Button>().onClick.AddListener(njewels_Click);
        GameObject.Find("PreviousJewel_B").GetComponent<Button>().onClick.AddListener(pjewels_Click);
        GameObject.Find("NextHair_B").GetComponent<Button>().onClick.AddListener(nhairs_Click);
        GameObject.Find("PreviousHair_B").GetComponent<Button>().onClick.AddListener(phairs_Click);
        GameObject.Find("NextBase_B").GetComponent<Button>().onClick.AddListener(nbases_Click);
        GameObject.Find("PreviousBase_B").GetComponent<Button>().onClick.AddListener(pbases_Click);
        GameObject.Find("NextPaint_B").GetComponent<Button>().onClick.AddListener(npaints_Click);
        GameObject.Find("PreviousPaint_B").GetComponent<Button>().onClick.AddListener(ppaints_Click);
        GameObject.Find("PreviousClothes_B").GetComponent<Button>().onClick.AddListener(nclothes_Click);
        GameObject.Find("PreviousClothes_B").GetComponent<Button>().onClick.AddListener(pclothes_Click);

        newtribename = GameObject.Find("TribeName").GetComponent<Text>();
        newp = GameObject.Find("New_P");
        tribe_members = GameObject.Find("TribeMembers_T").GetComponent<Text>();
        newname = GameObject.Find("NewName_T").GetComponent<Text>();
        nbeards = GameObject.Find("NextBeard_B");
        pbeards = GameObject.Find("PreviousBeard_B");
        njewels = GameObject.Find("NextJewel_B");
        pjewels = GameObject.Find("PreviousJewel_B");
        nhairs = GameObject.Find("NextHair_B");
        phairs = GameObject.Find("PreviousHair_B");
        nbase = GameObject.Find("NextBase_B");
        pbase = GameObject.Find("PreviousBase_B");
        npaints = GameObject.Find("NextPaint_B");
        ppaints = GameObject.Find("PreviousPaint_B");
        nclothes = GameObject.Find("NextClothes_B");
        pclothes = GameObject.Find("PreviousClothes_B");
        newjewels = GameObject.Find("New_Jewels");
        newbeards = GameObject.Find("New_Beards");
        newbase = GameObject.Find("New_Base");
        newhairs = GameObject.Find("New_Hairs");
        newclothes = GameObject.Find("New_Clothes");
        newpaints = GameObject.Find("New_Paints");


        newTribe = new TribeSave();
        newTribe.tribename = newtribename.text;
        newTribeGO = new GameObject("newTribeGO");
        newTribeGO.AddComponent<TribeGO>();
        tribecomp = newTribeGO.GetComponent<TribeGO>();
        tribecomp.profilename = "newgame"; // options profile ?
        tribecomp.tribeCurrent = newTribe;
        DontDestroyOnLoad(newTribeGO);


        ZoneOn = Resources.Load<Sprite>("NewGame/Map_Select");
        ZoneOff = Resources.Load<Sprite>("NewGame/Map_NoSelect");

        ImgZone1 = GameObject.Find("StartZone_Map1_B").GetComponent<Image>();
        ImgZone2 = GameObject.Find("StartZone_Map2_B").GetComponent<Image>();
        ImgZone3 = GameObject.Find("StartZone_Map3_B").GetComponent<Image>();
        spawnzone = 1;
        newp.SetActive(false);
    }

    void StartZone1_Click()
    {
        ImgZone1.sprite = ZoneOn;
        ImgZone2.sprite = ZoneOff;
        ImgZone3.sprite = ZoneOff;
        spawnzone = 1;
    }
    void StartZone2_Click()
    {
        ImgZone1.sprite = ZoneOff;
        ImgZone2.sprite = ZoneOn;
        ImgZone3.sprite = ZoneOff;
        spawnzone = 2;
    }
    void StartZone3_Click()
    {
        ImgZone1.sprite = ZoneOff;
        ImgZone2.sprite = ZoneOff;
        ImgZone3.sprite = ZoneOn;
        spawnzone = 3;
    }

    void Skip_B_Click()
    {
        TribeSave newtribe = new TribeSave();
        Text skipX = GameObject.Find("Xchars_T").GetComponent<Text>();
        try
        {
            newtribe.tribename = "skip " + newtribename.text;
            for (int i = 0; i < System.Convert.ToInt32(skipX.text); i++)
            {
                CharacterSave newcs = new CharacterSave();
                newcs.name = i.ToString();
                newcs.age.years = 0;
                newcs.sexe = Random.Range(0,2);
                if (newcs.sexe == 0)
                {
                    newcs.Pic_base = Random.Range(0, basesF.Length);
                    newcs.Pic_clothes = Random.Range(0, clothesF.Length); ;
                    newcs.Pic_paints = Random.Range(0, paintsF.Length);
                    newcs.Pic_hairs = Random.Range(0, hairsF.Length);
                    newcs.Pic_jewels = Random.Range(0, jewelsF.Length); ;
                }
                else
                {
                    newcs.Pic_base = Random.Range(0, basesM.Length);
                    newcs.Pic_clothes = Random.Range(0, clothesM.Length);
                    newcs.Pic_paints = Random.Range(0, paintsM.Length);
                    newcs.Pic_hairs = Random.Range(0, hairsM.Length);
                    newcs.Pic_beard = Random.Range(0, beardsM.Length);
                }
                newtribe.members.Add(newcs);
            }
            newTribeGO.GetComponent<TribeGO>().tribeCurrent = newtribe;
        }
        catch (System.FormatException)
        {
            Debug.Log("X is not a number");
        }
        PlayerPrefs.SetString("NewName", newtribename.text);
        PlayerPrefs.SetString("mapfile", "Assets/Resources/Map/TestMapOrtho2.tmx");
        PlayerPrefs.SetString("savefile", "");
        PlayerPrefs.SetInt("spawnzone", 1);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    void Update()
    {
    }
    void NewChar_B_Click()
    {
        try
        {
            CharacterSave newchar = new CharacterSave();
            newchar.name = newname.text;
            if (GameObject.Find("NewAge_T").GetComponent<Text>().text != "")
                newchar.age.years = System.Convert.ToInt32(GameObject.Find("NewAge_T").GetComponent<Text>().text);
            else
                newchar.age.years = 0;
            newchar.sexe = activesex;
            newchar.Pic_base = selected_bases;
            newchar.Pic_clothes = selected_clothes;
            newchar.Pic_paints = selected_paints;
            newchar.Pic_hairs = selected_hairs;
            if (activesex == 0)
            {
                newchar.Pic_jewels = selected_jewels;
            }
            else
            {
                newchar.Pic_beard = selected_beards;
            }
            newTribe.members.Add(newchar);
            tribe_members.text += "\n" + newname.text;
        }
        catch (System.FormatException)
        {
            Debug.Log("Age is not a number");
        }
        
    }
    void nbeards_Click()
    {
        if (activesex == 1)
        {
            if (selected_beards < beardsM.Length - 1)
                selected_beards++;
            newbeards.GetComponent<Image>().sprite = beardsM[selected_beards];
        }
    }
    void pbeards_Click()
    {
        if (activesex == 1)
        {
            if (selected_beards > 0)
                selected_beards--;
            newbeards.GetComponent<Image>().sprite = beardsM[selected_beards];
        }
    }
    void njewels_Click()
    {
        if (activesex == 0)
        {
            if (selected_jewels < jewelsF.Length - 1)
                selected_jewels++;
            newjewels.GetComponent<Image>().sprite = jewelsF[selected_jewels];
        }
    }
    void pjewels_Click()
    {
        if (activesex == 0)
        {
            if (selected_jewels > 0)
                selected_jewels--;
            newjewels.GetComponent<Image>().sprite = jewelsF[selected_jewels];
        }
    }
    void nbases_Click()
    {
        if (activesex == 0)
        {
            if (selected_bases < basesF.Length - 1)
                selected_bases++;
            newbase.GetComponent<Image>().sprite = basesF[selected_bases];
        }
        else
        {
            if (selected_bases < basesM.Length - 1)
                selected_bases++;
            newbase.GetComponent<Image>().sprite = basesM[selected_bases];
        }
    }
    void pbases_Click()
    {
        if (selected_bases > 0)
            selected_bases--;
        if (activesex == 0)
            newbase.GetComponent<Image>().sprite = basesF[selected_bases];
        else
            newbase.GetComponent<Image>().sprite = basesM[selected_bases];
    }
    void nhairs_Click()
    {
        if (activesex == 0)
        {
            if (selected_hairs < hairsF.Length - 1)
                selected_hairs++;
            newhairs.GetComponent<Image>().sprite = hairsF[selected_hairs];
        }
        else
        {
            if (selected_hairs < hairsM.Length - 1)
                selected_hairs++;
            newhairs.GetComponent<Image>().sprite = hairsM[selected_hairs];
        }
    }
    void phairs_Click()
    {
        if (selected_hairs > 0)
            selected_hairs--;
        if (activesex == 0)
            newhairs.GetComponent<Image>().sprite = hairsF[selected_hairs];
        else
            newhairs.GetComponent<Image>().sprite = hairsM[selected_hairs];
    }

    void npaints_Click()
    {
        if (activesex == 0)
        {
            if (selected_paints < paintsF.Length - 1)
                selected_paints++;
            newpaints.GetComponent<Image>().sprite = paintsF[selected_paints];
        }
        else
        {
            if (selected_paints < paintsM.Length - 1)
                selected_paints++;
            newpaints.GetComponent<Image>().sprite = paintsM[selected_paints];
        }
    }
    void ppaints_Click()
    {
        if (selected_paints > 0)
            selected_paints--;
        if (activesex == 0)
            newpaints.GetComponent<Image>().sprite = paintsF[selected_paints];
        else
            newpaints.GetComponent<Image>().sprite = paintsM[selected_paints];
    }
    void nclothes_Click()
    {
        if (activesex == 0)
        {
            if (selected_clothes < clothesF.Length - 1)
                selected_clothes++;
            newclothes.GetComponent<Image>().sprite = clothesF[selected_clothes];
        }
        else
        {
            if (selected_clothes < clothesM.Length - 1)
                selected_clothes++;
            newclothes.GetComponent<Image>().sprite = clothesM[selected_clothes];
        }
    }
    void pclothes_Click()
    {
        if (selected_clothes > 0)
            selected_clothes--;
        if (activesex == 0)
            newclothes.GetComponent<Image>().sprite = clothesF[selected_clothes];
        else
            newclothes.GetComponent<Image>().sprite = clothesM[selected_clothes];
    }
    void MakeFemale_Click()
    {
        selected_clothes = 0;
        selected_beards = 0;
        selected_hairs = 0;
        selected_paints = 0;
        selected_jewels = 0;
        selected_bases = 0;
        activesex = 0;
        nbeards.SetActive(false);
        pbeards.SetActive(false);
        njewels.SetActive(true);
        pjewels.SetActive(true);
        newjewels.SetActive(true);
        newbeards.SetActive(false);
        if (basesF.Length > 0)
           newbase.GetComponent<Image>().sprite = basesF[selected_bases];
        if (hairsF.Length > 0)
            newhairs.GetComponent<Image>().sprite = hairsF[selected_hairs];
        if (clothesF.Length > 0)
            newclothes.GetComponent<Image>().sprite = clothesF[selected_clothes];
        if (paintsF.Length > 0)
            newpaints.GetComponent<Image>().sprite = paintsF[selected_paints];
        if (jewelsF.Length > 0)
           newjewels.GetComponent<Image>().sprite = jewelsF[selected_jewels];
        newp.SetActive(true);
    }
    void MakeMale_Click()
    {
        selected_clothes = 0;
        selected_beards = 0;
        selected_hairs = 0;
        selected_paints = 0;
        selected_jewels = 0;
        selected_bases = 0;
        activesex = 1;
        nbeards.SetActive(true);
        pbeards.SetActive(true);
        njewels.SetActive(false);
        pjewels.SetActive(false);
        newjewels.SetActive(false);
        newbeards.SetActive(true);
        if (basesM.Length > 0)
            newbase.GetComponent<Image>().sprite = basesM[selected_bases];
        if (hairsM.Length > 0)
            newhairs.GetComponent<Image>().sprite = hairsM[selected_hairs];
        if (clothesM.Length > 0)
            newclothes.GetComponent<Image>().sprite = clothesM[selected_clothes];
        if (paintsM.Length > 0)
            newpaints.GetComponent<Image>().sprite = paintsM[selected_paints];
        if (beardsM.Length > 0)
            newjewels.GetComponent<Image>().sprite = beardsM[selected_jewels];
        newp.SetActive(true);
    }
    void LaunchButton_Click()
    {
        PlayerPrefs.SetString("NewName", newtribename.text);
        newTribeGO.GetComponent<TribeGO>().tribeCurrent.tribename = newtribename.text;
        PlayerPrefs.SetString("mapfile", "Assets/Resources/Map/TestMapOrtho2.tmx");
        PlayerPrefs.SetString("savefile", "");
        PlayerPrefs.SetInt("spawnzone", spawnzone);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    void BackButton_Click()
    {
        Destroy(newTribeGO);
        SceneManager.LoadScene("PlayMenu", LoadSceneMode.Single);
    }
    
}
