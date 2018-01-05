using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Button temp = GameObject.Find("PlayButton").GetComponent<Button>();
        temp.onClick.AddListener(PlayButton_Click);
        temp = GameObject.Find("OptionsButton").GetComponent<Button>();
        temp.onClick.AddListener(OptionsButton_Click);
        temp = GameObject.Find("ExitButton").GetComponent<Button>();
        temp.onClick.AddListener(ExitButton_Click);
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
