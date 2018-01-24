using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanelScript : MonoBehaviour {

    ControlsScript Controls;

    // Use this for initialization
    void Start () {

        GameObject.Find("SwitchChar").GetComponent<Button>().onClick.AddListener(SwitchChar_Click);
        GameObject.Find("SwitchTribe").GetComponent<Button>().onClick.AddListener(SwitchTribe_Click);
        GameObject.Find("SwitchMap").GetComponent<Button>().onClick.AddListener(SwitchMap_Click);

        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void SwitchTribe_Click()
    {
        Controls.resources_panel = false;
        Controls.tribe_panel = true;
        Controls.UIResourcesPanel.SetActive(false);
        Controls.UITribePanel.SetActive(true);
    }
    void SwitchMap_Click()
    {
        Controls.resources_panel = false;
        Controls.map_panel = true;
        Controls.UIResourcesPanel.SetActive(false);
        Controls.UIMapPanel.SetActive(true);
    }
    void SwitchChar_Click()
    {
        Controls.resources_panel = false;
        Controls.char_panel = true;
        Controls.UIResourcesPanel.SetActive(false);
        Controls.UICharactersPanel.SetActive(true);
    }
}
