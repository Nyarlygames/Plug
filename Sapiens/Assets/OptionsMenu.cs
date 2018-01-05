using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button temp = GameObject.Find("BackMainButton").GetComponent<Button>();
        temp.onClick.AddListener(BackMainButton_Click);
    }
	
	// Update is called once per frame
	void Update () {

    }

    void BackMainButton_Click()
    {
        // goes back to main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
