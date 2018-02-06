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
    public int next;
    public int strength = 0;
    public int endu = 0;
    public int body = 0;
    public int mental = 0;
    public int dexte = 0;
    public int accu = 0;
    public int speed = 0;
    public int percept = 0;
    public int surv = 0;
    public int intel = 0;
    public int mem = 0;
    public int chari = 0;
    public int social = 0;
    public int lang = 0;
    
    public int moral = 0;
    public int fatigue = 0;
    public int respect = 0;
    public int life = 0;
    public int speed_attr = 0;
    public int food = 0;
    public int resdis = 0;
    public int healdis = 0;
    public int reswoun = 0;
    public int healwoun = 0;
    public RatioFactory RF = new RatioFactory();

    public void SetAge()
    {
        age.days = (int) time / 24;
        age.years = (int) time / (24 * 365);
        age.hours = (int) time;
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
            xp -= 0.5f * ((age.years - RF.exp_adult_range) / 200.0f);
        }
    }

    public int SkipStats(float xp, int next_level)
    {
        float nextup = next_level;
        while ((level < RF.level_growth) && (xp > nextup))
        {
            xp -= nextup;
            level++;
            if (level == RF.level_growth)
                nextup *= RF.ratio_training;
            else
                nextup *= RF.ratio_growth;
        }
        while ((level < RF.level_training) && (xp > nextup))
        {
            xp -= nextup;
            level++;
            if (level == RF.ratio_training)
                nextup *= RF.ratio_mastering;
            else
                nextup *= RF.ratio_training;
        }
        while (xp > nextup)
        {
            xp -= nextup;
            nextup *= RF.ratio_mastering;
            level++;
        }
        return ((int) nextup);
    }

    public float SkipYear(int years)
    {

        int yearselder = 0;
        int yearsadult = 0;
        int yearsteen = 0;
        int yearsbaby = 0;
        int yearsskip = age.years + years;
        float xpacc = 0;
        
        for (int countdown = age.years; countdown < age.years + years; countdown++)
        {
            if (countdown < (int)RF.exp_baby_range)
                yearsbaby++;
            if ((countdown >= (int)RF.exp_baby_range) && (countdown < (int)RF.exp_teen_range))
                yearsteen++;
            if ((countdown >= (int)RF.exp_teen_range) && (countdown < (int)RF.exp_adult_range))
                yearsadult++;
            if (countdown >= (int)RF.exp_adult_range)
                yearselder++;
        }
        if (yearselder > 0)
        {
            xpacc -= (0.5f * (yearselder / 200.0f)) * 365;
            time += yearselder * 365 * 24;
        }

        if (yearsadult > 0)
        {
            xpacc += RF.exp_adult_value * 365; 
            time += yearsadult * 365 * 24;
        }

        if (yearsteen > 0)
        {
            xpacc += RF.exp_teen_value * 365;
            time += yearsteen * 365 * 24;
        }

        if (yearsbaby > 0)
        {
            xpacc += RF.exp_baby_value * 365;
            time += yearsbaby * 365 * 24;
        }
        SetAge();
        xp += xpacc;
        return (xpacc);
        
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
