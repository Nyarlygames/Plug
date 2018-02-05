using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGO : MonoBehaviour {

    public float time = 0;
    public CharacterSave charCurrent;

    // Use this for initialization
    void Start () {
        /*if (playerSave != null)
            time = playerSave.time;
        else
            Debug.Log("not loaded");*/

	}
	
	// Update is called once per frame
	void Update () {
        charCurrent.time += Time.time;
       // time += Time.time;
    }
}
