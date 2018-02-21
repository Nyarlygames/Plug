using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelChar : MonoBehaviour {

    GameManager GM;
    CharacterSave curChar;
    Dropdown CharacterList;
    Button CharacterListPlus;
    Button CharacterListMinus;
    GameObject CharacterFace;
    Text Character_Strength;
    Text Character_Endurance;
    Text Character_Body;
    Text Character_SpeedStat;

    Text Character_Dexterity;
    Text Character_Perception;
    Text Character_Accuracy;
    Text Character_Autonomy;

    Text Character_Spirit;
    Text Character_Social;
    Text Character_Mental;
    Text Character_Language;

    Text Character_Level;
    Text Character_NextXP;

    Text Character_Age;
    Text Character_Experience;
    Text Character_Moral;
    Text Character_Fatigue;
    Text Character_Respect;
    Text Character_Life;
    Text Character_Speed;
    Text Character_Food;
    Text Character_ResistDisease;
    Text Character_HealDisease;
    Text Character_ResistWounds;
    Text Character_HealWounds;
    Text Character_Name;
    Text Character_Status;
    Text Character_Title;
    Text Tribe_Youngs;
    Text Tribe_Adults;
    Text Tribe_Name;
    Image Tribe_FoodBar;
    Text Tribe_FoodBarText;
    Image HealthBar;
    Text Healthcurrent;
    Text Healthmax;
    Image FatigueBar;
    Text Fatiguecurrent;
    Text Fatiguemax;
    Image MoraleBar;
    Text Moralecurrent;
    Text Moralemax;
    Image LevelBar;
    Text Levelcurrent;
    Text Levelmax;
    Text LevelRank;
    Image LevelTopBar;
    Text LevelTopcurrent;
    Text LevelTopmax;
    GameObject CharPanelStats;
    GameObject CharPanelAttributes;
    List<GameObject> CharPanels = new List<GameObject>();

    string panel ="";

    void Start() {
        // Character_Panel_Stats
        CharPanelStats = GameObject.Find("Character_Panel_Stats");
        CharPanelAttributes = GameObject.Find("Character_Panel_Attributes");
        CharPanels.Add(CharPanelAttributes);
        CharPanels.Add(CharPanelStats);
        foreach (GameObject panel in CharPanels)
        {
            panel.SetActive(false);
        }
        if (CharPanels.Count > 0)
        {
            CharPanels[0].SetActive(true);
            panel = "attributes";
        }
        GameObject.Find("CharacterSelector+").GetComponent<Button>().onClick.AddListener(CharPlusClick);
        GameObject.Find("CharacterSelector-").GetComponent<Button>().onClick.AddListener(CharMinusClick);
        GameObject.Find("Character_Panel_Cara_B").GetComponent<Button>().onClick.AddListener(AttributesPanelClick);
        GameObject.Find("Character_Panel_Stats_B").GetComponent<Button>().onClick.AddListener(StatsPanelClick);
        CharacterFace = GameObject.Find("CharacterFace");

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();


        Character_Strength = GameObject.Find("Character_Strength").GetComponent<Text>();
        Character_Endurance = GameObject.Find("Character_Endurance").GetComponent<Text>();
        Character_Body = GameObject.Find("Character_Body").GetComponent<Text>();
        Character_SpeedStat = GameObject.Find("Character_SpeedStat").GetComponent<Text>();

        Character_Dexterity = GameObject.Find("Character_Dexterity").GetComponent<Text>();
        Character_Perception = GameObject.Find("Character_Perception").GetComponent<Text>();
        Character_Accuracy = GameObject.Find("Character_Accuracy").GetComponent<Text>();
        Character_Autonomy = GameObject.Find("Character_Autonomy").GetComponent<Text>();

        Character_Spirit = GameObject.Find("Character_Spirit").GetComponent<Text>();
        Character_Social = GameObject.Find("Character_Social").GetComponent<Text>();
        Character_Mental = GameObject.Find("Character_Mental").GetComponent<Text>();
        Character_Language = GameObject.Find("Character_Language").GetComponent<Text>();
        Character_Level = GameObject.Find("Character_Level").GetComponent<Text>();
        Character_NextXP = GameObject.Find("Character_NextXP").GetComponent<Text>();
        //Character_NextLevel = GameObject.Find("Character_TopLevelNext").GetComponent<Text>();
        //Character_NextLevelBar = GameObject.Find("Character_TopLevelBar").GetComponent<Image>();
        //Character_NextLevelBar.fillMethod = Image.FillMethod.Horizontal;

        Character_Age = GameObject.Find("Character_Age").GetComponent<Text>();
        Character_Experience = GameObject.Find("Character_Experience").GetComponent<Text>();
        Character_Moral = GameObject.Find("Character_Moral").GetComponent<Text>();
        Character_Fatigue = GameObject.Find("Character_Fatigue").GetComponent<Text>();
        Character_Respect = GameObject.Find("Character_Respect").GetComponent<Text>();
        Character_Life = GameObject.Find("Character_Life").GetComponent<Text>();
        Character_Speed = GameObject.Find("Character_Speed").GetComponent<Text>();
        Character_Food = GameObject.Find("Character_Food").GetComponent<Text>();
        Character_ResistDisease = GameObject.Find("Character_ResistDisease").GetComponent<Text>();
        Character_HealDisease = GameObject.Find("Character_HealDisease").GetComponent<Text>();
        Character_ResistWounds = GameObject.Find("Character_ResistWounds").GetComponent<Text>();
        Character_HealWounds = GameObject.Find("Character_HealWounds").GetComponent<Text>();
        Character_Name = GameObject.Find("Character_Name").GetComponent<Text>();
        Character_Status = GameObject.Find("Character_Status").GetComponent<Text>();
        Character_Title = GameObject.Find("Character_Title").GetComponent<Text>();
        
        CharacterList = GameObject.Find("CharacterSelector").GetComponent<Dropdown>();
        CharacterList.onValueChanged.AddListener(delegate {
            CharacterValueChanged(CharacterList);
        });
        if (GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Count > 0)
        {
            CharacterValueChanged(CharacterList);
        }

        Tribe_Youngs = GameObject.Find("TribeP_Overview_YoungsCount").GetComponent<Text>();
        Tribe_Adults = GameObject.Find("TribeP_Overview_AdultsCount").GetComponent<Text>();
        Tribe_Name = GameObject.Find("TribeP_Name").GetComponent<Text>();
        Tribe_Name.text = GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.tribename;
        Tribe_FoodBar = GameObject.Find("TribeP_Overview_FoodBar").GetComponent<Image>();
        Tribe_FoodBarText = GameObject.Find("TribeP_Overview_FoodOverText").GetComponent<Text>();
        LevelTopBar = GameObject.Find("Character_TopLevelBar").GetComponent<Image>();
        LevelTopcurrent = GameObject.Find("Character_TopLevelCurrent").GetComponent<Text>();
        LevelTopmax = GameObject.Find("Character_TopLevelNext").GetComponent<Text>();
    }
    
    void AttributesPanelClick()
    {
        foreach(GameObject panel in CharPanels.FindAll(obj => obj.activeSelf == true))
        {
            panel.SetActive(false);
        }
        CharPanels.Find(obj => obj == CharPanelAttributes).SetActive(true);
        panel = "attributes";

    }
    void StatsPanelClick()
    {
        foreach (GameObject panel in CharPanels.FindAll(obj => obj.activeSelf == true))
        {
            panel.SetActive(false);
        }
        CharPanels.Find(obj => obj == CharPanelStats).SetActive(true);

        if (HealthBar == null) { 
            HealthBar = GameObject.Find("Character_Stats_HealthBar").GetComponent<Image>();
            Healthcurrent = GameObject.Find("Character_Stats_MinHealth").GetComponent<Text>();
            Healthmax = GameObject.Find("Character_Stats_MaxHealth").GetComponent<Text>();
        }
        if (FatigueBar == null)
        {
            FatigueBar = GameObject.Find("Character_Stats_FatigueBar").GetComponent<Image>();
            Fatiguecurrent = GameObject.Find("Character_Stats_MinFatigue").GetComponent<Text>();
            Fatiguemax = GameObject.Find("Character_Stats_MaxFatigue").GetComponent<Text>();
        }
        if (MoraleBar == null)
        {
            MoraleBar = GameObject.Find("Character_Stats_MoralBar").GetComponent<Image>();
            Moralecurrent = GameObject.Find("Character_Stats_MinMoral").GetComponent<Text>();
            Moralemax = GameObject.Find("Character_Stats_MaxMoral").GetComponent<Text>();
        }
        if (LevelBar == null)
        {
            LevelBar = GameObject.Find("Character_Stats_LevelBar").GetComponent<Image>();
            Levelcurrent = GameObject.Find("Character_Stats_CurrentLevel").GetComponent<Text>();
            Levelmax = GameObject.Find("Character_Stats_NextLevel").GetComponent<Text>();
            LevelRank = GameObject.Find("Character_Stats_LevelRank").GetComponent<Text>();
        }        
        
        panel = "stats";

    }

    public void SetExistingChars()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        CharacterList = GameObject.Find("CharacterSelector").GetComponent<Dropdown>();
        List<Dropdown.OptionData> DDopt = new List<Dropdown.OptionData>();
        foreach (CharacterSave chara in GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members)
        {
            Dropdown.OptionData opt = new Dropdown.OptionData();
            opt.text = chara.name;
            DDopt.Add(opt);
        }
        CharacterList.options = DDopt;
    }

    void CharacterValueChanged(Dropdown CharList)
    {
        foreach (Transform child in CharacterFace.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
        if (GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Count > 0)
        {
            curChar = GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members[CharacterList.value];
            Transform charT = CharacterFace.GetComponent<Transform>();
            GameObject body = new GameObject(curChar.name + "_Body");
            Transform bodyT = body.GetComponent<Transform>();
            bodyT.SetParent(CharacterFace.GetComponent<Transform>());
            bodyT.position = new Vector3(charT.position.x, charT.position.y, charT.position.z);
            body.AddComponent<Image>();
            Sprite FaceSprite;
            if (curChar.sexe == 0)
                FaceSprite = GM.basesF[curChar.Pic_base];
            else
                FaceSprite = GM.basesM[curChar.Pic_base];
            body.GetComponent<Image>().sprite = FaceSprite;

            GameObject paints = new GameObject(curChar.name + "_Paints");
            Transform paintsT = paints.GetComponent<Transform>();
            paintsT.SetParent(CharacterFace.GetComponent<Transform>());
            paintsT.position = new Vector3(charT.position.x, charT.position.y, charT.position.z);
            paints.AddComponent<Image>();
            Sprite PaintSprite;
            if (curChar.sexe == 0)
                PaintSprite = GM.paintsF[curChar.Pic_paints];
            else
                PaintSprite = GM.paintsM[curChar.Pic_paints];
            paints.GetComponent<Image>().sprite = PaintSprite;

            GameObject hairs = new GameObject(curChar.name + "_Hairs");
            Transform hairsT = hairs.GetComponent<Transform>();
            hairsT.SetParent(CharacterFace.GetComponent<Transform>());
            hairsT.position = new Vector3(charT.position.x, charT.position.y, charT.position.z);
            hairs.AddComponent<Image>();
            Sprite HairsSprite;
            if (curChar.sexe == 0)
                HairsSprite = GM.hairsF[curChar.Pic_hairs];
            else
                HairsSprite = GM.hairsM[curChar.Pic_hairs];
            hairs.GetComponent<Image>().sprite = HairsSprite;

            GameObject extra = new GameObject(curChar.name + "_Extra");
            Transform extraT = extra.GetComponent<Transform>();
            extraT.SetParent(CharacterFace.GetComponent<Transform>());
            extraT.position = new Vector3(charT.position.x, charT.position.y, charT.position.z);
            extra.AddComponent<Image>();
            Sprite ExtraSprite;
            if (curChar.sexe == 0)
                ExtraSprite = GM.jewelsF[curChar.Pic_jewels];
            else
                ExtraSprite = GM.beardsM[curChar.Pic_beard];
            extra.GetComponent<Image>().sprite = ExtraSprite;

            GameObject clothes = new GameObject(curChar.name + "_Clothes");
            Transform clothesT = clothes.GetComponent<Transform>();
            clothesT.SetParent(CharacterFace.GetComponent<Transform>());
            clothesT.position = new Vector3(charT.position.x, charT.position.y, charT.position.z);
            clothes.AddComponent<Image>();
            Sprite ClothesSprite;
            if (curChar.sexe == 0)
                ClothesSprite = GM.clothesF[curChar.Pic_clothes];
            else
                ClothesSprite = GM.clothesM[curChar.Pic_clothes];
            clothes.GetComponent<Image>().sprite = ClothesSprite;
            Character_Name.text = "Name : " + curChar.name;
        }
    }

    void CharPlusClick()
    {
        foreach (Transform child in CharacterFace.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
        if (CharacterList.value >= CharacterList.options.Count -1)
            CharacterList.value = 0;
        else
            CharacterList.value++;
    }
    void CharMinusClick()
    {
        foreach (Transform child in CharacterFace.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
        if (CharacterList.value <= 0)
            CharacterList.value = CharacterList.options.Count;
        else
            CharacterList.value--;
    }

    void Update ()
    {
        if (GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Count > 0)
        {
            if (curChar == null) // added first with debug while char panel open
            {
                curChar = GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members[0];
            }
            TribeSave tribe = GM.TribeGO.GetComponent<TribeGO>().tribeCurrent;

            // panel char bottom
            if (panel == "attributes")
            {
                Character_Strength.text = "Strength : " + curChar.strength;
                Character_Endurance.text = "Endurance : " + curChar.endu;
                Character_Body.text = "Body : " + curChar.body;
                Character_SpeedStat.text = "Speed : " + curChar.speed;
                Character_Dexterity.text = "Dexterity : " + curChar.dexte;
                Character_Perception.text = "Perception : " + curChar.percept;
                Character_Accuracy.text = "Accuracy : " + curChar.accu;
                Character_Autonomy.text = "Autonomy : " + curChar.autonomy;
                Character_Spirit.text = "Spirit : " + curChar.spirit;
                Character_Social.text = "Social : " + curChar.social;
                Character_Mental.text = "Mental : " + curChar.mental;
                Character_Language.text = "Language : " + curChar.lang;
                Character_Food.text = "Food : " + curChar.food;
            }
            
            if (panel == "stats")
            {

                curChar.moral = curChar.moralmax; // to remove, set elsewhere
                curChar.fatigue = curChar.fatiguemax; // to remove, set elsewhere
                curChar.life = curChar.lifemax;// to remove, set  elsewhere
                
                HealthBar.fillAmount = (curChar.life / curChar.lifemax);
                FatigueBar.fillAmount = (curChar.fatigue / curChar.fatiguemax);
                MoraleBar.fillAmount = (curChar.moral / curChar.moralmax);
                LevelBar.fillAmount = (curChar.last / curChar.next);
                


                Fatiguecurrent.text = curChar.fatigue.ToString();
                Fatiguemax.text = curChar.fatiguemax.ToString();
                Healthcurrent.text = ((int)curChar.life).ToString();
                Healthmax.text = ((int)curChar.lifemax).ToString();
                Moralecurrent.text = curChar.moral.ToString();
                Moralemax.text = curChar.moralmax.ToString();
                Levelcurrent.text = curChar.level.ToString();
                Levelmax.text = (curChar.level + 1).ToString();
                LevelRank.text = curChar.exp_rank;
            }

            // panel tribe
            Tribe_Youngs.text = tribe.youngs.Count.ToString();
            Tribe_Adults.text = tribe.adults.Count.ToString();
            if (tribe.food_consumption < tribe.food_gain)
                Tribe_FoodBar.fillAmount = tribe.food_consumption / tribe.food_gain;
            else
                Tribe_FoodBar.fillAmount = 0;
            Tribe_FoodBarText.text = tribe.food_consumption + " used / " + tribe.food_gain + " available each day.";

            // panel char top
            Character_Age.text = "Age : " + curChar.age.days / 365 + "years, " + curChar.age.days + " days.";
            Character_Experience.text = "XP : " + curChar.xp;
            Character_Level.text = curChar.level.ToString();
            Character_NextXP.text = "Next level : " + ((int)curChar.last).ToString() + " / " + ((int)curChar.next).ToString();
            LevelTopBar.fillAmount = (curChar.last / curChar.next);
            LevelTopcurrent.text = curChar.level.ToString();
            LevelTopmax.text = (curChar.level + 1).ToString();
            
        }
    }
}
