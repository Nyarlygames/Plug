using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Profiles PlayerProf;
    
	void Start () {
        PlayerProf = gameObject.AddComponent<Profiles>();
        GameObject.Find("PlayButton").GetComponent<Image>().enabled = false;
        GameObject.Find("OptionsButton").GetComponent<Image>().enabled = false;
    }
	
	void Update () {
		
	}
}
