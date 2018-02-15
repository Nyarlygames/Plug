using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EventSave {
    
    public ObjectSave obj;
    public List<CharacterSave> cs = new List<CharacterSave>();
    public int id = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
