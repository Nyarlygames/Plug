using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    // Use this for initialization
    void Start() {

        Button temp = GameObject.Find("PlayButton").GetComponent<Button>();
        temp.onClick.AddListener(PlayButton_Click);
        temp = GameObject.Find("OptionsButton").GetComponent<Button>();
        temp.onClick.AddListener(OptionsButton_Click);
        temp = GameObject.Find("ExitButton").GetComponent<Button>();
        temp.onClick.AddListener(ExitButton_Click);


        Object[] textures;
        //switch button texts or pic to correct language
        switch (PlayerPrefs.GetString("Lang"))
        {
            case "French":
                textures = Resources.LoadAll("Menu/FR", typeof(Sprite));
                break;
            case "English":
                textures = Resources.LoadAll("Menu/EN", typeof(Sprite));
                break;
            case "Spanish":
                textures = Resources.LoadAll("Menu/SPA", typeof(Sprite));
                // NYI
                break;
            default:
                //English if not set
                textures = Resources.LoadAll("Menu/EN", typeof(Sprite)); 
                break;
        }
        foreach (var t in textures)
        {
            if (t.name == "PlayButton")
            {
                GameObject.Find("PlayButton").GetComponent<Image>().sprite = t as Sprite;
            }
            if (t.name == "OptionsButton")
            {
                GameObject.Find("OptionsButton").GetComponent<Image>().sprite = t as Sprite;
            }
            if (t.name == "ExitButton")
            {
                GameObject.Find("ExitButton").GetComponent<Image>().sprite = t as Sprite;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayButton_Click()
    {
        SceneManager.LoadScene("PlayMenu", LoadSceneMode.Single);
    }

    void OptionsButton_Click()
    {
        SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Single);
    }

    void ExitButton_Click()
    {
        Application.Quit();
    }
}
