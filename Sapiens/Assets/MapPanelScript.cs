using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanelScript : MonoBehaviour {

    ControlsScript Controls;

    // Use this for initialization
    void Start ()
    {
        GameObject.Find("SwitchChar").GetComponent<Button>().onClick.AddListener(SwitchChar_Click);
        GameObject.Find("SwitchTribe").GetComponent<Button>().onClick.AddListener(SwitchTribe_Click);
        GameObject.Find("SwitchResources").GetComponent<Button>().onClick.AddListener(SwitchResources_Click);

        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void SwitchTribe_Click()
    {
        Controls.map_panel = false;
        Controls.tribe_panel = true;
        Controls.UIMapPanel.SetActive(false);
        Controls.UITribePanel.SetActive(true);
    }
    void SwitchResources_Click()
    {
        Controls.map_panel = false;
        Controls.resources_panel = true;
        Controls.UIMapPanel.SetActive(false);
        Controls.UIResourcesPanel.SetActive(true);
    }
    void SwitchChar_Click()
    {
        Controls.map_panel = false;
        Controls.char_panel = true;
        Controls.UIMapPanel.SetActive(false);
        Controls.UICharactersPanel.SetActive(true);
    }
}
