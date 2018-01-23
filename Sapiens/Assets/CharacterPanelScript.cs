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

    void Start ()
    {
        CharacterList = GameObject.Find("CharacterSelector").GetComponent<Dropdown>();
        Tribe = GameObject.Find("Tribe").GetComponent<TribeScript>();
        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();
        GameObject.Find("CharacterSelector+").GetComponent<Button>().onClick.AddListener(CharacterSelectorPlus_Click);
        GameObject.Find("CharacterSelector-").GetComponent<Button>().onClick.AddListener(CharacterSelectorMinus_Click);         
        GameObject.Find("SwitchTribe").GetComponent<Button>().onClick.AddListener(SwitchTribe_Click);

        List<Dropdown.OptionData> DDopt = new List<Dropdown.OptionData>();
        foreach (GameObject chars in Tribe.Characters)
        {
            Dropdown.OptionData opt = new Dropdown.OptionData();
            opt.text = chars.name;
            DDopt.Add(opt);
        }
        CharacterList.options = DDopt;
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

    }
    void CharacterSelectorMinus_Click()
    {

    }
}
