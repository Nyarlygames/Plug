using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGo : MonoBehaviour {

    public float time = 0;
    public PlayerScript playerSave;

	// Use this for initialization
	void Start () {
        /*if (playerSave != null)
            time = playerSave.time;
        else
            Debug.Log("not loaded");*/

	}
	
	// Update is called once per frame
	void Update () {
        time += Time.time;
    }
}
