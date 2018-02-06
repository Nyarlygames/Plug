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
    public float xp = 0;
    public int level = 0;
    public RatioFactory RF = new RatioFactory();

    public void SetAge()
    {
        age.days = (int) time / 24;
        age.years = (int) time / (24 * 365);
        age.hours = (int)time;
    }

    public void DailyXpUp()
    {
        if (age.years < RF.exp_baby_range)
        {
            xp += RF.exp_baby_value;
        }
        else if (age.years < RF.exp_teen_range)
        {
            xp += RF.exp_teen_value;

        }
        else if (age.years < RF.exp_adult_range)
        {
            xp += RF.exp_adult_value;
        }
        else // elder
        {
            xp -= 0.5f * ((age.years - RF.exp_adult_range) / 200);
        }
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
