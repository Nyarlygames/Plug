using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class TribeSave
{
    public float time = 0;
    public AgeStruct age = new AgeStruct();
    public List<CharacterSave> members = new List<CharacterSave>();
    public List<CharacterSave> groups;
    public CharacterSave leader;
    public List<CharacterSave> gatherers;
    public List<CharacterSave> adults;
    public List<CharacterSave> youngs;
    public string tribename = "";
    public float food = 0;
    public float food_ratio = 0;
    public float gatherer_level = 0;
    public int rank = 0;

    public void SetAge()
    {
        age.days = (int)time / 24;
        age.years = (int)time / (24 * 365);
        age.hours = (int)time;
    }
}
