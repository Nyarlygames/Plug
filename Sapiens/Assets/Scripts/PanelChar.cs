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
    Image CharacterFace;
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
    Text Character_NextLevel;
    Image Character_NextLevelBar;

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

    void Start() {
        
        GameObject.Find("CharacterSelector+").GetComponent<Button>().onClick.AddListener(CharPlusClick);
        GameObject.Find("CharacterSelector-").GetComponent<Button>().onClick.AddListener(CharMinusClick);
        CharacterFace = GameObject.Find("CharacterFace").GetComponent<Image>();

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
        Character_NextLevel = GameObject.Find("Character_NextLevel").GetComponent<Text>();
        Character_NextLevelBar = GameObject.Find("Character_NextLevelBar").GetComponent<Image>();
        Character_NextLevelBar.fillMethod = Image.FillMethod.Horizontal;

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
        CharacterValueChanged(CharacterList);
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
        curChar = GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members[CharacterList.value];
        CharacterFace.sprite = GM.TribeGO.GetComponent<TribeGO>().CharsGO[CharacterList.value].GetComponent<SpriteRenderer>().sprite;
        Character_Name.text = "Name : " + curChar.name;
    }

    void CharPlusClick()
    {
        if (CharacterList.value >= CharacterList.options.Count -1)
            CharacterList.value = 0;
        else
            CharacterList.value++;
    }
    void CharMinusClick()
    {
        if (CharacterList.value <= 0)
            CharacterList.value = CharacterList.options.Count;
        else
            CharacterList.value--;
    }

    void Update ()
    {
        Character_Age.text = "Age : " + curChar.age.days / 365 + "years, " + curChar.age.days + " days.";
        Character_Experience.text = "XP : " + curChar.xp;
        Character_Level.text = curChar.level.ToString();
        Character_NextLevel.text = (curChar.level + 1).ToString();
        Character_NextXP.text = "Next level : " + ((int)curChar.last).ToString() + " / " + ((int)curChar.next).ToString();
        Character_NextLevelBar.fillAmount = curChar.last / curChar.next;
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
    }
}
