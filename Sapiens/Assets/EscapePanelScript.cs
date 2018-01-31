using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapePanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Button temp = GameObject.Find("Escape_Menu").GetComponent<Button>();
        temp.onClick.AddListener(Escape_Menu_Click);
    }
	
	// Update is called once per frame
	void Update () {

    }

    void Escape_Menu_Click()
    {
        SceneManager.LoadScene("PlayMenu", LoadSceneMode.Single);
    }
}
