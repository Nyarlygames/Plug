using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Profiles PlayerProf;
    
	void Start () {
        PlayerProf = gameObject.AddComponent<Profiles>();
        GameObject.Find("GreetingText").GetComponent<Text>().text = "Greetings " + PlayerProf.profilename + " ! ";
    }
	
	void Update () {
		
	}
}
