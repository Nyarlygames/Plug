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
    public List<CharacterSave> groups = new List<CharacterSave>();
    public CharacterSave leader;
    public List<CharacterSave> gatherers = new List<CharacterSave>();
    public List<CharacterSave> fishers = new List<CharacterSave>();
    public List<CharacterSave> hunters = new List<CharacterSave>();
    public List<CharacterSave> sourcers = new List<CharacterSave>();
    public List<CharacterSave> adults = new List<CharacterSave>();
    public List<CharacterSave> youngs = new List<CharacterSave>();
    public string tribename = "";
    public float food = 0;
    public float food_gain = 0;
    public float food_consumption = 0;
    public float water = 0;
    public float water_gain = 0;
    public float water_consumption = 0;
    public int rank = 0;
    public bool nomadism = true;
    public float TribePosX = 0;
    public float TribePosY = 0;
    public RatioFactory RF = new RatioFactory();

    public void SetAge()
    {
        age.days = (int)time / 24;
        age.years = (int)time / (24 * 365);
        age.hours = (int)time;
    }
    
}
