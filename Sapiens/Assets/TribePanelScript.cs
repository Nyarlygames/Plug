using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TribePanelScript : MonoBehaviour {

    ControlsScript Controls;

    void Start ()
    {
        GameObject.Find("SwitchChar").GetComponent<Button>().onClick.AddListener(SwitchTribe_Click);
        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();

    }

    void SwitchTribe_Click()
    {
        Controls.char_panel = true;
        Controls.tribe_panel = false;
        SceneManager.LoadSceneAsync("CharacterPanel", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("TribePanel");

    }

    void Update () {
		
	}
}
