using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("cam : " + GameObject.Find("Pause Camera").GetComponent<Transform>().position + " main cam : " + GameObject.Find("Main Camera").GetComponent<Transform>().position);
        GameObject.Find("Pause Camera").GetComponent<Transform>().position = GameObject.Find("Main Camera").GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
