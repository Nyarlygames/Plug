using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenuController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button temp = GameObject.Find("NGButton").GetComponent<Button>();
        temp.onClick.AddListener(NG_Click);
        temp = GameObject.Find("LoadButton").GetComponent<Button>();
        temp.onClick.AddListener(LoadButton_Click);
        temp = GameObject.Find("DeleteButton").GetComponent<Button>();
        temp.onClick.AddListener(DeleteButton_Click);
        temp = GameObject.Find("BackMainButton").GetComponent<Button>();
        temp.onClick.AddListener(BackMainButton_Click);

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
           /* if (t.name == "Back_Play")
            {
                GameObject.Find("PlayMenu").GetComponent<Image>().sprite = t as Sprite;
            }*/
            if (t.name == "NewGameButton")
            {
                GameObject.Find("NGButton").GetComponent<Image>().sprite = t as Sprite;
            }
            if (t.name == "LoadButton")
            {
                GameObject.Find("LoadButton").GetComponent<Image>().sprite = t as Sprite;
            }
            if (t.name == "DeleteButton")
            {
                GameObject.Find("DeleteButton").GetComponent<Image>().sprite = t as Sprite;
            }
            if (t.name == "BackMainButton")
            {
                GameObject.Find("BackMainButton").GetComponent<Image>().sprite = t as Sprite;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    void NG_Click()
    {
        //new game
        SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
    }

    void LoadButton_Click()
    {
        //Load save menu
    }

    void DeleteButton_Click()
    {
        // delete save menu
    }
    void BackMainButton_Click()
    {
        // goes back to main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
