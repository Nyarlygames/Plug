using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStruct {

    public string type = "";
    public int recurrent = 0;
    public int nextStart = 0;
    public bool inUse = false;
    public GatherZoneScript Zone;
    public CharacterScript Char;
    public bool isDone = false;
    public bool isCharBack = false;
    public bool isInRange = true;

    void Start () {
		
	}
	
	void Update () {
		
	}
}
