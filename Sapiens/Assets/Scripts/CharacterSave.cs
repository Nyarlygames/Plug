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
    public AgeStruct age = new AgeStruct();

    public void SetAge()
    {
        age.days = (int) time / 24;
        age.years = (int) time / (24 * 365);
        age.hours = (int)time;
    }
    
   /* public void SetChar(string naming, int identifier)
    {
        name = naming;
        id = identifier;
    }
    public void SetParents(CharacterSave fat, CharacterSave moth)
    {
        father = fat;
        mother = moth;
    }
    */
    
}
