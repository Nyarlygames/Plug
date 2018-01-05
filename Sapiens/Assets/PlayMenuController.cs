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
