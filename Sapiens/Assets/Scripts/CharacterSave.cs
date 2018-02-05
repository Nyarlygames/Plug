using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CharacterSave
{

    public float time = 0;
    public int id = 0;
    public CharacterSave father;
    public CharacterSave mother;
    public string name = "";
    public string charSprite;

    void Start()
    {
    }

    public void SetChar(string naming, int identifier)
    {
        name = naming;
        id = identifier;
    }

    public void SetParents(CharacterSave fat, CharacterSave moth)
    {
        father = fat;
        mother = moth;
    }

    // Update is called once per frame
    void Update () {
        time = Time.time;
	}
    
}
