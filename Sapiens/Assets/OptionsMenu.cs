using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Button temp = GameObject.Find("BackMainButton").GetComponent<Button>();
        temp.onClick.AddListener(BackMainButton_Click);
        temp = GameObject.Find("ApplyButton").GetComponent<Button>();
        temp.onClick.AddListener(ApplyButton_Click);
        Object[] textures;
        //switch button texts or pic to correct language
        switch (PlayerPrefs.GetString("Lang"))
        {
            case "French":
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[0].text = "Anglais";
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[1].text = "Français";
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[2].text = "Espagnol";
                textures = Resources.LoadAll("Menu/FR", typeof(Sprite));
                GameObject.Find("LangageDD").GetComponent<Dropdown>().value = 1;
                break;
            case "English":
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[0].text = "English";
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[1].text = "French";
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[2].text = "Spanish";
                textures = Resources.LoadAll("Menu/EN", typeof(Sprite));
                GameObject.Find("LangageDD").GetComponent<Dropdown>().value = 0;
                break;
            case "Spanish":
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[0].text = "Ingles";
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[1].text = "Frànces";
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[2].text = "Espanol";
                textures = Resources.LoadAll("Menu/SPA", typeof(Sprite)); //NYI
                GameObject.Find("LangageDD").GetComponent<Dropdown>().value = 2;
                break;
            default:
                //English
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[0].text = "English";
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[1].text = "French";
                GameObject.Find("LangageDD").GetComponent<Dropdown>().options[2].text = "Spanish";
                textures = Resources.LoadAll("Menu/EN", typeof(Sprite));
                GameObject.Find("LangageDD").GetComponent<Dropdown>().value = 0;
                break;
        }
        foreach (var t in textures)
        {
            if (t.name == "Back")
            {
                GameObject.Find("BackMainButton").GetComponent<Image>().sprite = t as Sprite;
            }
            if (t.name == "Apply")
            {
                GameObject.Find("ApplyButton").GetComponent<Image>().sprite = t as Sprite;
            }
        }

        

    }
	
	// Update is called once per frame
	void Update () {

    }

    void BackMainButton_Click()
    {
        // goes back to main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    void ApplyButton_Click()
    {
        // goes back to main menu
        switch (GameObject.Find("LangageDD").GetComponent<Dropdown>().value)
        {
            case 1:
                PlayerPrefs.SetString("Lang", "French");
                break;
            case 2:
                PlayerPrefs.SetString("Lang", "Spanish");
                break;
            case 0:
                PlayerPrefs.SetString("Lang", "English");
                break;
            default:
                PlayerPrefs.SetString("Lang", "English");
                break;
        }
    }
}
