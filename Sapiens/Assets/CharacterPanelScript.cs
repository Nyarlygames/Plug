using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterPanelScript : MonoBehaviour {

    TribeScript Tribe;
    Dropdown CharacterList;
    Button CharacterListPlus;
    Button CharacterListMinus;
    ControlsScript Controls;
    CharacterScript CurrentChar;

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

    void Start ()
    {
        CharacterList = GameObject.Find("CharacterSelector").GetComponent<Dropdown>();
        Tribe = GameObject.Find("Tribe").GetComponent<TribeScript>();
        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();
        GameObject.Find("CharacterSelector+").GetComponent<Button>().onClick.AddListener(CharacterSelectorPlus_Click);
        GameObject.Find("CharacterSelector-").GetComponent<Button>().onClick.AddListener(CharacterSelectorMinus_Click);         
        GameObject.Find("SwitchTribe").GetComponent<Button>().onClick.AddListener(SwitchTribe_Click);
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

        List<Dropdown.OptionData> DDopt = new List<Dropdown.OptionData>();
        foreach (GameObject chars in Tribe.Characters)
        {
            Dropdown.OptionData opt = new Dropdown.OptionData();
            opt.text = chars.name;
            DDopt.Add(opt);
        }
        CharacterList.options = DDopt;
        CharacterList.onValueChanged.AddListener(delegate {
            CharacterValueChanged(CharacterList);
        });
        CharacterValueChanged(CharacterList);
    }
	
	void Update () { 
    }

    void SwitchTribe_Click()
    {
        Controls.char_panel = false;
        Controls.tribe_panel = true;
        SceneManager.LoadSceneAsync("TribePanel", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("CharacterPanel");
    }

    void CharacterSelectorPlus_Click()
    {
        CharacterList.value++;
    }
    void CharacterSelectorMinus_Click()
    {
        CharacterList.value--;
    }

    void CharacterValueChanged(Dropdown CharacterList)
    {
        CurrentChar = Tribe.Characters[CharacterList.value].GetComponent<CharacterScript>();
        CharacterFace.sprite = CurrentChar.GetComponent<SpriteRenderer>().sprite;
        Character_Name.text = "Name : " + CurrentChar.name.ToString();
        Character_Age.text = "Age : " + CurrentChar.age.ToString();
        Character_Title.text = "Title : " + CurrentChar.status.ToString();
        if (CurrentChar.available)
            Character_Status.text = "Status : Available";
        else
            Character_Status.text = "Status : Unavailable";

        // Statss
        Character_Strength.text = "Strength : " + CurrentChar.strength.ToString();
        Character_Endurance.text = "Endurance : " + CurrentChar.endu.ToString();
        Character_Body.text = "Body : " + CurrentChar.body.ToString();
        Character_Mental.text = "Mental : " + CurrentChar.mental.ToString();
        Character_Dexterity.text = "Dexterity : " + CurrentChar.dexte.ToString();
        Character_Accuracy.text = "Accuracy : " + CurrentChar.accu.ToString();
        Character_SpeedStat.text = "Speed : " + CurrentChar.speed.ToString();
        Character_Perception.text = "Perception : " + CurrentChar.percept.ToString();
        Character_Survival.text = "Survival : " + CurrentChar.surv.ToString();
        Character_Intel.text = "Intel : " + CurrentChar.intel.ToString();
        Character_Memory.text = "Memory : " + CurrentChar.mem.ToString();
        Character_Charisma.text = "Charisma : " + CurrentChar.chari.ToString();
        Character_Social.text = "Social : " + CurrentChar.social.ToString();
        Character_Language.text = "Language : " + CurrentChar.lang.ToString();

        // Attributes
        Character_Experience.text = "Experience : " + CurrentChar.exp.ToString();
        Character_Moral.text = "Moral : " + CurrentChar.moral.ToString();
        Character_Fatigue.text = "Fatigue : " + CurrentChar.fatigue.ToString();
        Character_Respect.text = "Respect : " + CurrentChar.respect.ToString();
        Character_Life.text = "Life : " + CurrentChar.life.ToString();
        Character_Speed.text = "Speed : " + CurrentChar.speed_attr.ToString();
        Character_Food.text = "Food : " + CurrentChar.food.ToString();
        Character_ResistDisease.text = "Resist diseases : " + CurrentChar.resdis.ToString();
        Character_HealDisease.text = "Heal diseases : " + CurrentChar.healdis.ToString();
        Character_ResistWounds.text = "Resist wounds : " + CurrentChar.reswoun.ToString();
        Character_HealWounds.text = "Heal wounds : " + CurrentChar.healwoun.ToString();
    }
    
}
