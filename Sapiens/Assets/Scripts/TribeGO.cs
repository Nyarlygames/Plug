using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeGO : MonoBehaviour {
    
    /* public List<CharacterSave> members;
     public CharacterSave leader;
     public List<CharacterSave> gatherers;*/
    //public TribeSave tribeSave;
    public TribeSave tribeCurrent;
    public List<GameObject> CharsGO = new List<GameObject>();
    public string profilename = "";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        tribeCurrent.time += Time.deltaTime;
	}
}
