using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CharacterSave
{

    public float time = 0;
    public int id = 0;
    public float x;
    public float y;
    public float z;
    public int sexe;
    public CharacterSave father;
    public CharacterSave mother;
    public string name = "";
    public AgeStruct age = new AgeStruct();
    public float xp = 0;
    public int level = 0;
    public string exp_rank = "";
    public float next;
    public float last;
    public bool available = true;
    
    public int strength = 0;
    public int endu = 0;
    public int body = 0;
    public int speed = 0;

    public int dexte = 0;
    public int percept = 0;
    public int accu = 0;
    public int autonomy = 0;

    public int spirit = 0;
    public int social = 0;
    public int mental = 0;
    public int lang = 0;
    
    public int moral = 0;
    public int fatigue = 0;
    public int fatiguemax = 0;
    public int fatiguerecup = 0;
    public int moralmax = 0;
    public int moralrecup = 0;
    public int respect = 0;
    public float life = 0;
    public float lifemax = 0;
    public int move = 0;
    public float food = 0;
    public int resdis = 0;
    public int healdis = 0;
    public int reswoun = 0;
    public int healwoun = 0;

    public int Pic_base = 0;
    public int Pic_clothes = 0;
    public int Pic_hairs = 0;
    public int Pic_paints = 0;
    public int Pic_jewels = 0;
    public int Pic_beard = 0;

    public bool gatherer = true;
    public bool hunter = true;
    public bool fisher = true;
    public bool sourcer = false;
    public bool cook = true;
    public bool manager = true;
    public bool parent = true;
    public bool mentor = false;
    public bool shaman = false;
    public bool skinner = false;
    public bool carver = false;
    public bool stoner = false;
    public bool protector = false;
    public bool leader = false;
    public bool scout = false;
    public bool rest = false;
    public bool pregnant = false;

    public float gatherer_level = 0;
    public float gatherer_xp = 0;
    public float hunter_level = 0;
    public float hunter_xp = 0;
    public float sourcer_level = 0;
    public float sourcer_xp = 0;
    public float fisher_level = 0;
    public float fisher_xp = 0;

    bool availablelock = false;

    public RatioFactory RF = new RatioFactory();

    public void SetAge()
    {
        age.days = (int) time / 24;
        age.years = (int) time / (24 * 365);
        age.hours = (int) time;
        if (age.years <= RF.exp_baby_range)
        {
            available = false;
        }
        else if (availablelock == false)
        {
            availablelock = true;
            available = true;
        }
    }


    public void AddActivityGain(string activity)
    {
        switch (activity)
        {
            case "hunt":
                hunter_xp += RF.exp_hunter_lvlgain;
                AddActivityXP(RF.exp_hunter_lvlgain);
                if (hunter_xp >= RF.exp_hunter_nextlvl)
                {
                    hunter_xp -= RF.exp_hunter_nextlvl;
                    hunter_level++;
                    RF.exp_hunter_nextlvl *= RF.exp_hunter_nextlvlratio;
                    RF.exp_hunter_lvlgain -= RF.exp_hunter_levelupratio;
                }
                break;
            case "fish":
                fisher_xp += RF.exp_fisher_lvlgain;
                AddActivityXP(RF.exp_fisher_lvlgain);
                if (fisher_xp >= RF.exp_fisher_nextlvl)
                {
                    fisher_xp -= RF.exp_fisher_nextlvl;
                    fisher_level++;
                    RF.exp_fisher_nextlvl *= RF.exp_fisher_nextlvlratio;
                    RF.exp_fisher_lvlgain -= RF.exp_fisher_levelupratio;
                }
                break;
            case "gather":
                gatherer_xp += RF.exp_gatherer_lvlgain;
                AddActivityXP(RF.exp_gatherer_lvlgain);
                if (gatherer_xp >= RF.exp_gatherer_nextlvl)
                {
                    gatherer_xp -= RF.exp_gatherer_nextlvl;
                    gatherer_level++;
                    RF.exp_gatherer_nextlvl *= RF.exp_gatherer_nextlvlratio;
                    RF.exp_gatherer_lvlgain -= RF.exp_gatherer_levelupratio;
                }
                break;
            case "source":
                sourcer_xp += RF.exp_sourcer_lvlgain;
                AddActivityXP(RF.exp_sourcer_lvlgain);
                if (sourcer_xp >= RF.exp_sourcer_nextlvl)
                {
                    sourcer_xp -= RF.exp_sourcer_nextlvl;
                    sourcer_level++;
                    RF.exp_sourcer_nextlvl *= RF.exp_sourcer_nextlvlratio;
                    RF.exp_sourcer_lvlgain -= RF.exp_sourcer_levelupratio;
                }
                break;
            default:
                break;
        }
    }

    public void AddActivityXP(float gain)
    {
        xp += gain;
        last += gain;
        if (last >= next) // level up
        {
            // later, switch activity for special treats
            LevelUp(level);
            level++;
            last = 0;
            if (level > RF.level_training)
            {
                next = Mathf.Round(next * RF.ratio_mastering);
            }
            if (level > RF.level_growth)
            {
                next = Mathf.Round(next * RF.ratio_training);
            }
            if (level <= RF.level_growth)
            {
                next = Mathf.Round(next * RF.ratio_growth);
            }
        }
    }

    public void DailyXpUp()
    {
        if (age.years < RF.exp_baby_range)
        {
            xp += RF.exp_baby_value;
            last += RF.exp_baby_value;
            exp_rank = "Growth";
        }
        else if (age.years < RF.exp_teen_range)
        {
            xp += RF.exp_teen_value;
            last += RF.exp_teen_value;
            exp_rank = "Training";

        }
        else if (age.years < RF.exp_adult_range)
        {
            xp += RF.exp_adult_value;
            last += RF.exp_adult_value;
            exp_rank = "Mastering";
        }
        else // elder
        {
            xp -= 0.5f * ((age.years - RF.exp_adult_range) / 200.0f);
            last += 0.5f * ((age.years - RF.exp_adult_range) / 200.0f);
            if (last < 0) // remove when activities
                last = 0;
        }
        if (last >= next) // level up
        {
            LevelUp(level);
            level++;
            last = 0;
            if (level > RF.level_training)
            {
                next = Mathf.Round(next * RF.ratio_mastering);
            }
            if (level > RF.level_growth)
            {
                next = Mathf.Round(next * RF.ratio_training);
            }
            if (level <= RF.level_growth)
            {
                next = Mathf.Round(next * RF.ratio_growth);
            }
        }
    }

    public float SkipStats(float xp, float next_level)
    {
        float nextup = next_level;
        xp += last;
        while ((level < RF.level_growth) && (xp > nextup))
        {
            xp -= nextup;
            level++;
            LevelUp(level);
            if (level == RF.level_growth)
                nextup *= RF.ratio_training;
            else
                nextup *= RF.ratio_growth;
        }
        while ((level < RF.level_training) && (xp > nextup))
        {
            xp -= nextup;
            level++;
            LevelUp(level);
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
            LevelUp(level);
        }
        last = xp;
        return (Mathf.Round(nextup));
    }

    public float SkipYear(int years)
    {

        int yearselder = 0;
        int yearsadult = 0;
        int yearsteen = 0;
        int yearsbaby = 0;
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

    public void LevelUp(int level)
    {
        List<string> Attributes = new List<string>();
        Attributes.Add("strength");
        Attributes.Add("endu");
        Attributes.Add("body");
        Attributes.Add("speed");
        Attributes.Add("dexte");
        Attributes.Add("percept");
        Attributes.Add("accu");
        Attributes.Add("autonomy");
        Attributes.Add("spirit");
        Attributes.Add("social");
        Attributes.Add("mental");
        Attributes.Add("lang");
        if (level < RF.exp_teen_range)
        {
            int picking = UnityEngine.Random.Range(10, Attributes.Count+1);
            for (int i = 0; i <= picking - 1; i++)
            {
                int pick = UnityEngine.Random.Range(0, Attributes.Count);
                switch (Attributes[pick])
                {
                    case "strength":
                        strength += UnityEngine.Random.Range(1, 4);
                        break;
                    case "endu":
                        endu += UnityEngine.Random.Range(1, 4);
                        break;
                    case "body":
                        body += UnityEngine.Random.Range(1, 4);
                        break;
                    case "speed":
                        speed += UnityEngine.Random.Range(1, 4);
                        break;
                    case "dexte":
                        dexte += UnityEngine.Random.Range(1, 4);
                        break;
                    case "percept":
                        percept += UnityEngine.Random.Range(1, 4);
                        break;
                    case "accu":
                        accu += UnityEngine.Random.Range(1, 4);
                        break;
                    case "autonomy":
                        autonomy += UnityEngine.Random.Range(1, 4);
                        break;
                    case "spirit":
                        spirit += UnityEngine.Random.Range(1, 4);
                        break;
                    case "social":
                        social += UnityEngine.Random.Range(1, 4);
                        break;
                    case "mental":
                        mental += UnityEngine.Random.Range(1, 4);
                        break;
                    case "lang":
                        lang += UnityEngine.Random.Range(1, 4);
                        break;
                    default:
                        Debug.Log("LevelUp stats failed");
                        break;
                }
                Attributes.RemoveAt(pick);
            }
        }
        else
        {
            int picking = UnityEngine.Random.Range(3, 5);
            for (int i = 0; i <= picking - 1; i++)
            {
                int pick = UnityEngine.Random.Range(0, Attributes.Count);
                switch (Attributes[pick])
                {
                    case "strength":
                        strength += UnityEngine.Random.Range(1, 6);
                        break;
                    case "endu":
                        endu += UnityEngine.Random.Range(1, 6);
                        break;
                    case "body":
                        body += UnityEngine.Random.Range(1, 6);
                        break;
                    case "speed":
                        speed += UnityEngine.Random.Range(1, 6);
                        break;
                    case "dexte":
                        dexte += UnityEngine.Random.Range(1, 6);
                        break;
                    case "percept":
                        percept += UnityEngine.Random.Range(1, 6);
                        break;
                    case "accu":
                        accu += UnityEngine.Random.Range(1, 6);
                        break;
                    case "autonomy":
                        autonomy += UnityEngine.Random.Range(1, 6);
                        break;
                    case "spirit":
                        spirit += UnityEngine.Random.Range(1, 6);
                        break;
                    case "social":
                        social += UnityEngine.Random.Range(1, 6);
                        break;
                    case "mental":
                        mental += UnityEngine.Random.Range(1, 6);
                        break;
                    case "lang":
                        lang += UnityEngine.Random.Range(1, 6);
                        break;
                    default:
                        Debug.Log("LevelUp stats failed");
                        break;
                }
                Attributes.RemoveAt(pick);
            }
        }
        food = ((strength * RF.ratio_food_strength) + (body * RF.ratio_food_body) + (endu * RF.ratio_food_endu)) / 100;
        lifemax = body * x + strength;
        moralmax = mental * 2 + social * 2;
        moralmax = lang + spirit / 60;
        fatiguemax = endu * 2 + strength;
        fatiguerecup = body + mental / 40;
    }
    

    public void SetActivities()
    {
        if ((sourcer == false) && (percept >= RF.prereq_source_percept) && (endu >= RF.prereq_source_endu))
            sourcer = true;
        if ((mentor == false) && (spirit >= RF.prereq_mentor_intel) && (lang >= RF.prereq_mentor_lang))
            mentor = true;
    }
}
