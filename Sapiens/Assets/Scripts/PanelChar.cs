using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelChar : MonoBehaviour {

    GameManager GM;
    GameObject curChar;
    Dropdown CharacterList;
    Button CharacterListPlus;
    Button CharacterListMinus;
    Image CharacterFace;
    Text Character_Strength;
    Text Character_Endurance;
    Text Character_Body;
    Text Character_Mental;
    Text Character_Dexterity;
    Text Character_Accuracy;
    Text Character_SpeedStat;
    Text Character_Perception;
    Text Character_Survival;
    Text Character_Intel;
    Text Character_Memory;
    Text Character_Charisma;
    Text Character_Social;
    Text Character_Language;
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
        Character_Mental = GameObject.Find("Character_Mental").GetComponent<Text>();
        Character_Dexterity = GameObject.Find("Character_Dexterity").GetComponent<Text>();
        Character_Accuracy = GameObject.Find("Character_Accuracy").GetComponent<Text>();
        Character_SpeedStat = GameObject.Find("Character_SpeedStat").GetComponent<Text>();
        Character_Perception = GameObject.Find("Character_Perception").GetComponent<Text>();
        Character_Survival = GameObject.Find("Character_Survival").GetComponent<Text>();
        Character_Intel = GameObject.Find("Character_Intel").GetComponent<Text>();
        Character_Memory = GameObject.Find("Character_Memory").GetComponent<Text>();
        Character_Charisma = GameObject.Find("Character_Charisma").GetComponent<Text>();
        Character_Social = GameObject.Find("Character_Social").GetComponent<Text>();
        Character_Language = GameObject.Find("Character_Language").GetComponent<Text>();
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
        curChar = GM.TribeGO.GetComponent<TribeGO>().CharsGO[CharacterList.value];
        CharacterFace.sprite = curChar.GetComponent<SpriteRenderer>().sprite;
        Character_Name.text = "Name : " + curChar.GetComponent<CharacterGO>().charCurrent.name;
        Character_Age.text = "Age : " + curChar.GetComponent<CharacterGO>().charCurrent.age.days;
        Character_Experience.text = "XP : " + curChar.GetComponent<CharacterGO>().charCurrent.xp;
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

    void Update () {
        Character_Experience.text = "XP : " + curChar.GetComponent<CharacterGO>().charCurrent.xp;
    }
}
