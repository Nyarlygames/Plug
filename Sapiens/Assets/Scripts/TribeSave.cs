using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class TribeSave
{
    public float time = 0;
    public List<CharacterSave> members = new List<CharacterSave>();
    public List<CharacterSave> groups;
    public CharacterSave leader;
    public List<CharacterSave> gatherers;
    public string tribename = "";
    
}
