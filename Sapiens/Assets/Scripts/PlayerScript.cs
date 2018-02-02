using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerScript{

    public float time = 0;
    public int id = 0;
    public PlayerScript father;
    public PlayerScript mother;
    public string name = "";

    void Start()
    {
    }

    public void SetChar(string naming, int identifier)
    {
        name = naming;
        id = identifier;
    }

    public void SetParents(PlayerScript fat, PlayerScript moth)
    {
        father = fat;
        mother = moth;
    }

    // Update is called once per frame
    void Update () {
        time = Time.time;
	}
    
}
