using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    int selected_clothes = 0;
    int selected_beards = 0;
    int selected_hairs = 0;
    int selected_paints = 0;
    int selected_jewels = 0;
    int selected_bases = 0;

    int activesex = 0;


    void Start ()
    {
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
        
        Button temp = GameObject.Find("Launch").GetComponent<Button>();
        temp.onClick.AddListener(LaunchButton_Click);
        temp = GameObject.Find("BackButton").GetComponent<Button>();
        temp.onClick.AddListener(BackButton_Click);
        temp = GameObject.Find("NewFemale_B").GetComponent<Button>();
        temp.onClick.AddListener(MakeFemale_Click);
        temp = GameObject.Find("NewMale_B").GetComponent<Button>();
        temp.onClick.AddListener(MakeMale_Click);


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

        newp = GameObject.Find("New_P");
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
        newp.SetActive(false);
    }

    void Update()
    {
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
        newp.SetActive(true);
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
        newp.SetActive(true);
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
    }
    void LaunchButton_Click()
    {
        PlayerPrefs.SetString("NewName", GameObject.Find("TribeName").GetComponent<Text>().text);
        PlayerPrefs.SetString("mapfile", "Assets/Resources/Map/TestMapOrtho2.tmx");
        PlayerPrefs.SetString("savefile", "");
        GameObject testGO = new GameObject("testgo");
        DontDestroyOnLoad(testGO);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    void BackButton_Click()
    {
        SceneManager.LoadScene("PlayMenu", LoadSceneMode.Single);
    }
    
}
