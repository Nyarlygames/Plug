using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject.Find("Selector").GetComponent<ModeWords>().addWord("SOLO");
        GameObject.Find("Selector").GetComponent<ModeWords>().addWord("MULTI");
        GameObject.Find("Selector").GetComponent<ModeWords>().addWord("COOP");
        GameObject.Find("Selector").GetComponent<ModeWords>().addWord("COOP");
        GameObject.Find("Solo").GetComponent<Image>().enabled = false;
        GameObject.Find("SoloText").GetComponent<Text>().enabled = false;
        GameObject.Find("Multi").GetComponent<Image>().enabled = false;
        GameObject.Find("MultiText").GetComponent<Text>().enabled = false;
        GameObject.Find("Coop").GetComponent<Image>().enabled = false;
        GameObject.Find("CoopText").GetComponent<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
