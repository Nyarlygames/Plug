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
    public float next = 0;
    public float last = 0;
    public bool available = true;
    public int attributesToSpend = 0;
    public int skillsToSpend = 0;

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
    
    bool availablelock = false;
    public List<ActivityStruct> activities = new List<ActivityStruct>();
    public ActivityStruct mainActivity = new ActivityStruct();
    public ActivityStruct secondaryActivity = new ActivityStruct();
    public RatioFactory RF = new RatioFactory();

    public float xpactivity = 0.0f;
    public float xpauto = 0.0f;

    public void SetActivitiesList()
    {
        ActivityStruct temp = new ActivityStruct();
        temp.name = "gather";
        temp.status = true;
        temp.statusPexHigh = false;
        temp.xpUp = RF.exp_gatherer_basegain;
        temp.nextLevel = RF.exp_gatherer_baselvl;
        temp.ratiogain = RF.exp_gatherer_ratiogain;
        temp.ratiolvl = RF.exp_gatherer_ratiolvl;
        temp.primaryActivity = true;
        temp.affinities.Add("endu", 2);
        temp.affinities.Add("percept", 1);
        temp.affinities.Add("autonomy", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "fish";
        temp.status = true;
        temp.statusPexHigh = false;
        temp.xpUp = RF.exp_fisher_basegain;
        temp.nextLevel = RF.exp_fisher_baselvl;
        temp.ratiogain = RF.exp_fisher_ratiogain;
        temp.ratiolvl = RF.exp_fisher_ratiolvl;
        temp.affinities.Add("dexte", 2);
        temp.affinities.Add("strength", 1);
        temp.affinities.Add("autonomy", 1);
        temp.primaryActivity = true;
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "hunt";
        temp.status = true;
        temp.statusPexHigh = false;
        temp.xpUp = RF.exp_hunter_basegain;
        temp.nextLevel = RF.exp_hunter_baselvl;
        temp.ratiogain = RF.exp_hunter_ratiogain;
        temp.ratiolvl = RF.exp_hunter_ratiolvl;
        temp.primaryActivity = true;
        temp.affinities.Add("endu", 3);
        temp.affinities.Add("percept", 2);
        temp.affinities.Add("accu", 1);
        temp.affinities.Add("strength", 1);
        temp.affinities.Add("autonomy", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "source";
        temp.status = false;
        temp.statusPexHigh = false;
        temp.xpUp = RF.exp_sourcer_basegain;
        temp.nextLevel = RF.exp_sourcer_baselvl;
        temp.ratiogain = RF.exp_sourcer_ratiogain;
        temp.ratiolvl = RF.exp_sourcer_ratiolvl;
        temp.primaryActivity = true;
        temp.affinities.Add("percept", 3);
        temp.affinities.Add("endu", 1);
        temp.affinities.Add("autonomy", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "cook";
        temp.status = true;
        temp.statusPexHigh = false;
        temp.xpUp = RF.exp_cook_basegain;
        temp.nextLevel = RF.exp_cook_baselvl;
        temp.ratiogain = RF.exp_cook_ratiogain;
        temp.ratiolvl = RF.exp_cook_ratiolvl;
        temp.affinities.Add("social", 2);
        temp.affinities.Add("spirit", 1);
        temp.affinities.Add("dexte", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "manage";
        temp.status = true;
        temp.statusPexHigh = true;
        temp.xpUp = RF.exp_manager_basegain;
        temp.nextLevel = RF.exp_manager_baselvl;
        temp.ratiogain = RF.exp_manager_ratiogain;
        temp.ratiolvl = RF.exp_manager_ratiolvl;
        temp.affinities.Add("social", 2);
        temp.affinities.Add("lang", 2);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "mentor";
        temp.status = true;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_mentor_basegain;
        temp.nextLevel = RF.exp_mentor_baselvl;
        temp.ratiogain = RF.exp_mentor_ratiogain;
        temp.ratiolvl = RF.exp_mentor_ratiolvl;
        temp.affinities.Add("mental", 2);
        temp.affinities.Add("social", 2);
        temp.affinities.Add("endu", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "sage";
        temp.status = false;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_sage_basegain;
        temp.nextLevel = RF.exp_sage_baselvl;
        temp.ratiogain = RF.exp_sage_ratiogain;
        temp.ratiolvl = RF.exp_sage_ratiolvl;
        temp.affinities.Add("spirit", 2);
        temp.affinities.Add("social", 2);
        temp.affinities.Add("lang", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "shaman";
        temp.status = true;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_shaman_basegain;
        temp.nextLevel = RF.exp_shaman_baselvl;
        temp.ratiogain = RF.exp_shaman_ratiogain;
        temp.ratiolvl = RF.exp_shaman_ratiolvl;
        temp.affinities.Add("spirit", 3);
        temp.affinities.Add("lang", 2);
        temp.affinities.Add("mental", 2);
        temp.affinities.Add("dexte", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "skincraft";
        temp.status = true;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_skincraft_basegain;
        temp.nextLevel = RF.exp_skincraft_baselvl;
        temp.ratiogain = RF.exp_skincraft_ratiogain;
        temp.ratiolvl = RF.exp_skincraft_ratiolvl;
        temp.affinities.Add("dexte", 2);
        temp.affinities.Add("accu", 1);
        temp.affinities.Add("spirit", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "woodcraft";
        temp.status = true;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_woodcraft_basegain;
        temp.nextLevel = RF.exp_woodcraft_baselvl;
        temp.ratiogain = RF.exp_woodcraft_ratiogain;
        temp.ratiolvl = RF.exp_woodcraft_ratiolvl;
        temp.affinities.Add("dexte", 2);
        temp.affinities.Add("accu", 1);
        temp.affinities.Add("spirit", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "stonecraft";
        temp.status = true;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_stonecraft_basegain;
        temp.nextLevel = RF.exp_stonecraft_baselvl;
        temp.ratiogain = RF.exp_stonecraft_ratiogain;
        temp.ratiolvl = RF.exp_stonecraft_ratiolvl;
        temp.affinities.Add("dexte", 2);
        temp.affinities.Add("accu", 1);
        temp.affinities.Add("spirit", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "protect";
        temp.status = true;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_protector_basegain;
        temp.nextLevel = RF.exp_protector_baselvl;
        temp.ratiogain = RF.exp_protector_ratiogain;
        temp.ratiolvl = RF.exp_protector_ratiolvl;
        temp.affinities.Add("body", 2);
        temp.affinities.Add("strength", 1);
        temp.affinities.Add("dexte", 1);
        temp.affinities.Add("accu", 1);
        temp.primaryActivity = true;
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "lead";
        temp.status = true;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_leader_basegain;
        temp.nextLevel = RF.exp_leader_baselvl;
        temp.ratiogain = RF.exp_leader_ratiogain;
        temp.ratiolvl = RF.exp_leader_ratiolvl;
        temp.affinities.Add("social", 2);
        temp.affinities.Add("strength", 2);
        temp.affinities.Add("endu", 2);
        temp.affinities.Add("lang", 2);
        temp.affinities.Add("spirit", 1);
        temp.primaryActivity = true;
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "scout";
        temp.status = true;
        temp.statusPexHigh = true; // to remove if pre-requesite, and add elsewhere
        temp.xpUp = RF.exp_scout_basegain;
        temp.nextLevel = RF.exp_scout_baselvl;
        temp.ratiogain = RF.exp_scout_ratiogain;
        temp.ratiolvl = RF.exp_scout_ratiolvl;
        temp.affinities.Add("percept", 2);
        temp.affinities.Add("autonomy", 1);
        temp.primaryActivity = true;
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "rest";
        temp.status = false;
        temp.statusPexHigh = false; // to remove if pre-requesite, and add elsewhere
        temp.affinities.Add("body", 1);
        activities.Add(temp);
        temp = new ActivityStruct();
        temp.name = "pregnant";
        temp.status = false;
        temp.statusPexHigh = false; // to remove if pre-requesite, and add elsewhere
        temp.affinities.Add("body", 2);
        temp.affinities.Add("mental", 2);
        temp.xpUp = RF.exp_pregnant_basegain;
        activities.Add(temp);
    }

    public void GainTime(AgeStruct addedTime, bool simulatedActivities)
    {
        AgeStruct curtime = new AgeStruct();
        int newdays = GetCumulDay(addedTime);

        if ((int)Time.timeSinceLevelLoad / 24 + addedTime.hours >= 24) // TODO : if age tribe + hours > day
            newdays++;
        // TODO-bis : do if sup year ? hmm.. nope ?
        time += addedTime.hours;
        while (newdays > 0)
        {
            if (simulatedActivities == true)
                GainActivitySimulated();
            else
                GainActivityManual();
            GainXP();
            time += 24;
            SetAge();
            newdays--;
        }
    }
    
    public void GainXP()
    {
        if (age.years < RF.exp_baby_range)
        {
            last += RF.exp_baby_value;
            xp += RF.exp_baby_value;
            xpauto += RF.exp_baby_value;
            if (last >= next)
            {
                last -= next;
                next *= RF.ratio_growth;
                levelUpXP("growing");
            }
        }
        else if (age.years < RF.exp_teen_range)
        {
            last += RF.exp_teen_value;
            xp += RF.exp_teen_value;
            xpauto += RF.exp_teen_value;
            if (last >= next)
            {
                last -= next;
                next *= RF.ratio_training;
                levelUpXP("training");
            }
        }
        else if (age.years < RF.exp_adult_range)
        {
            last += RF.exp_adult_value;
            xp += RF.exp_adult_value;
            xpauto += RF.exp_adult_value;
            if (last >= next)
            {
                last -= next;
                next *= RF.ratio_mastering;
                levelUpXP("mastering");
            }
        }
        else if (age.years >= RF.exp_senior_range)
        {
            // new day as senior
        }
    }

    public void GainActivityManual()
    {
        // gain time with manual activity
    }

    public void GainActivitySimulated()
    {
        if (available == true)
        {
            // simulate primary
            int pickedAct = UnityEngine.Random.Range(0, activities.FindAll(act => ((act.status == true) && (act.primaryActivity == true))).Count);
            int successRate = UnityEngine.Random.Range(0, RF.simulate_activity_successmax);

            if (successRate <= RF.simulate_activity_successmin)
            {
                ActivityStruct primaryPick = activities.FindAll(act => ((act.status == true) && (act.primaryActivity == true)))[pickedAct];
                GainActivityXP(primaryPick);
                GainXPActivity(primaryPick);
            }
            SetActivityLocks();
            // simulate secondary
            int pickedActSecondary = UnityEngine.Random.Range(0, activities.FindAll(act => ((act.status == true) && (act.primaryActivity == false))).Count);
            int successRateSecondary = UnityEngine.Random.Range(0, RF.simulate_activity_secondary_successmax);

            if (successRate <= RF.simulate_activity_secondary_successmin)
            {
                ActivityStruct secondaryPick = activities.FindAll(act => ((act.status == true) && (act.primaryActivity == false)))[pickedAct];
                GainActivityXP(secondaryPick);
                GainXPActivity(secondaryPick);
            }
            SetActivityLocks();
        }
    }

    public void GainActivityXP(ActivityStruct activity)
    {
        activity.xp += activity.xpUp;
        if (activity.xpCurrent + activity.xpUp >= activity.nextLevel)
        {
            activity.xpCurrent = activity.nextLevel - (activity.xpCurrent + activity.xpUp);
            levelUpActivity(activity);
        }
        else
            activity.xpCurrent += activity.xpUp;
    }

    public void levelUpActivity(ActivityStruct activity)
    {
        activity.level++;
        activity.nextLevel *= activity.ratiolvl;
        activity.xpUp += activity.ratiogain;
    }


    public void GainXPActivity(ActivityStruct activity)
    {
        last += activity.xpUp;
        xp += activity.xpUp;
        xpactivity += activity.xpUp;
        if (last >= next)
        {
            last -= next;
            if (age.years < RF.exp_baby_range)
            {
                next *= RF.ratio_growth;
                levelUpXP("growing");
            }
            else if (age.years < RF.exp_teen_range)
            {
                next *= RF.ratio_training;
                levelUpXP("training");
            }
            else if (age.years < RF.exp_adult_range)
            {
                next *= RF.ratio_mastering;
                levelUpXP("mastering");
            }
            else if (age.years >= RF.exp_senior_range)
            {
            }
        }
    }
    
    public void levelUpXP(string rank)
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
        int addedPoints = 0;
        int addedChars = 0;
        switch (rank)
        {
            case "growing":
                addedPoints = UnityEngine.Random.Range(RF.growing_point_min, RF.growing_point_max + 1);
                addedChars = UnityEngine.Random.Range(RF.growing_stats_min, RF.growing_stats_max + 1);
                for (int i=0; i < addedChars; i++)
                {
                    int pick = UnityEngine.Random.Range(0, Attributes.Count);
                    switch (Attributes[pick])
                    {
                        case "strength":
                            strength += addedPoints;
                            break;
                        case "endu":
                            endu += addedPoints;
                            break;
                        case "body":
                            body += addedPoints;
                            break;
                        case "speed":
                            speed += addedPoints;
                            break;
                        case "dexte":
                            dexte += addedPoints;
                            break;
                        case "percept":
                            percept += addedPoints;
                            break;
                        case "accu":
                            accu += addedPoints;
                            break;
                        case "autonomy":
                            autonomy += addedPoints;
                            break;
                        case "spirit":
                            spirit += addedPoints;
                            break;
                        case "social":
                            social += addedPoints;
                            break;
                        case "mental":
                            mental += addedPoints;
                            break;
                        case "lang":
                            lang += addedPoints;
                            break;
                        default:
                            Debug.Log("LevelUp stats failed");
                            break;
                    }
                    Attributes.RemoveAt(pick);
                }
                break;
            case "training":
                addedPoints = UnityEngine.Random.Range(RF.training_point_min, RF.training_point_max + 1);
                addedChars = UnityEngine.Random.Range(RF.training_stats_min, RF.training_stats_max + 1);
                for (int i = 0; i < addedChars; i++)
                {
                    int pick = UnityEngine.Random.Range(0, Attributes.Count);
                    switch (Attributes[pick])
                    {
                        case "strength":
                            strength += addedPoints;
                            break;
                        case "endu":
                            endu += addedPoints;
                            break;
                        case "body":
                            body += addedPoints;
                            break;
                        case "speed":
                            speed += addedPoints;
                            break;
                        case "dexte":
                            dexte += addedPoints;
                            break;
                        case "percept":
                            percept += addedPoints;
                            break;
                        case "accu":
                            accu += addedPoints;
                            break;
                        case "autonomy":
                            autonomy += addedPoints;
                            break;
                        case "spirit":
                            spirit += addedPoints;
                            break;
                        case "social":
                            social += addedPoints;
                            break;
                        case "mental":
                            mental += addedPoints;
                            break;
                        case "lang":
                            lang += addedPoints;
                            break;
                        default:
                            Debug.Log("LevelUp stats failed");
                            break;
                    }
                    Attributes.RemoveAt(pick);
                }
                break;
            case "mastering":
                addedChars = UnityEngine.Random.Range(RF.mastering_point_min, RF.mastering_stats_max+1);
                foreach(ActivityStruct act in activities)
                {
                    if ((act.primaryActivity == true) && (act.xp >= mainActivity.xp))
                    {
                        mainActivity = act;
                    }
                    if ((act.primaryActivity == false) && (act.xp >= secondaryActivity.xp))
                    {
                        secondaryActivity = act;
                    }
                }
                if (mainActivity.name != "")
                {
                    foreach (KeyValuePair<string, int> entry in mainActivity.affinities)
                    {
                        switch (entry.Key)
                        {
                            case "strength":
                                strength += entry.Value;
                                break;
                            case "endu":
                                endu += entry.Value;
                                break;
                            case "body":
                                body += entry.Value;
                                break;
                            case "speed":
                                speed += entry.Value;
                                break;
                            case "dexte":
                                dexte += entry.Value;
                                break;
                            case "percept":
                                percept += entry.Value;
                                break;
                            case "accu":
                                accu += entry.Value;
                                break;
                            case "autonomy":
                                autonomy += entry.Value;
                                break;
                            case "spirit":
                                spirit += entry.Value;
                                break;
                            case "social":
                                social += entry.Value;
                                break;
                            case "mental":
                                mental += entry.Value;
                                break;
                            case "lang":
                                lang += entry.Value;
                                break;
                            default:
                                Debug.Log("secondary affinity gain failed : " + entry.Key);
                                break;
                        }
                    }
                }

                if (secondaryActivity.name != "")
                {
                    int increaseSecondary = addedChars - mainActivity.affinities.Count;
                    foreach(KeyValuePair<string, int> entry in secondaryActivity.affinities) {
                        if (increaseSecondary > 0)
                        {
                            switch (entry.Key)
                            {
                                case "strength":
                                    strength += entry.Value;
                                    break;
                                case "endu":
                                    endu += entry.Value;
                                    break;
                                case "body":
                                    body += entry.Value;
                                    break;
                                case "speed":
                                    speed += entry.Value;
                                    break;
                                case "dexte":
                                    dexte += entry.Value;
                                    break;
                                case "percept":
                                    percept += entry.Value;
                                    break;
                                case "accu":
                                    accu += entry.Value;
                                    break;
                                case "autonomy":
                                    autonomy += entry.Value;
                                    break;
                                case "spirit":
                                    spirit += entry.Value;
                                    break;
                                case "social":
                                    social += entry.Value;
                                    break;
                                case "mental":
                                    mental += entry.Value;
                                    break;
                                case "lang":
                                    lang += entry.Value;
                                    break;
                                default:
                                    Debug.Log("secondary affinity gain failed");
                                    break;
                            }
                            increaseSecondary--;
                        }
                        else
                            break;
                    }
                }

                attributesToSpend += 2;
                skillsToSpend++;
                break;
            default:
                // should not happen
                break;
        }
        level++;
    }

    int GetCumulDay(AgeStruct addedTime)
    {
        int newdays = 0;
        newdays += addedTime.days;
        if (addedTime.years >= 0)
            newdays += 365 * addedTime.years;
        return (newdays);
    }
    
    public void SetActivityLocks()
    {
        if ((endu >= RF.prereq_source_endu) && (percept >= RF.prereq_source_percept))
            activities.Find(act => act.name == "source").status = true; 
        if ((spirit >= RF.prereq_mentor_spirit) && (lang >= RF.prereq_mentor_lang))
            activities.Find(act => act.name == "sage").status = true;
        if ((endu >= RF.prereq_gatherHigh_endu) && (percept >= RF.prereq_gatherHigh_percept) && (autonomy >= RF.prereq_gatherHigh_auto))
            activities.Find(act => act.name == "gather").statusPexHigh = true;
        if ((dexte >= RF.prereq_fishHigh_dexte) && (strength >= RF.prereq_fishHigh_strength) && (autonomy >= RF.prereq_fishHigh_auto))
            activities.Find(act => act.name == "fish").statusPexHigh = true;
        if ((endu >= RF.prereq_huntHigh_endu) && (percept >= RF.prereq_huntHigh_percept) && (accu >= 65) && (autonomy >= RF.prereq_huntHigh_auto))
            activities.Find(act => act.name == "hunt").statusPexHigh = true;
        if ((percept >= RF.prereq_sourceHigh_percept) && (endu >= RF.prereq_sourceHigh_endu) && (autonomy >= RF.prereq_sourceHigh_auto))
            activities.Find(act => act.name == "source").statusPexHigh = true;
        if ((social >= RF.prereq_cookHigh_social) && (spirit >= RF.prereq_cookHigh_spirit) && (dexte >= RF.prereq_cookHigh_dexte))
            activities.Find(act => act.name == "cook").statusPexHigh = true;

        foreach (ActivityStruct act in activities.FindAll(act => ((act.status == true) && (act.xp + act.xpUp >= act.nextLevel) && ((act.level == 2) && (act.statusPexHigh == false)))))
        {
            act.status = false;
        }

    }

    public void SetBaseStats()
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
        foreach (string attr in Attributes)
        {
            switch (attr)
            {
                case "strength":
                    strength += UnityEngine.Random.Range(20, 31);
                    break;
                case "endu":
                    endu += UnityEngine.Random.Range(20, 31);
                    break;
                case "body":
                    body += UnityEngine.Random.Range(20, 31);
                    break;
                case "speed":
                    speed += UnityEngine.Random.Range(20, 31);
                    break;
                case "dexte":
                    dexte += UnityEngine.Random.Range(20, 31);
                    break;
                case "percept":
                    percept += UnityEngine.Random.Range(20, 31);
                    break;
                case "accu":
                    accu += UnityEngine.Random.Range(20, 31);
                    break;
                case "autonomy":
                    autonomy += UnityEngine.Random.Range(20, 31);
                    break;
                case "spirit":
                    spirit += UnityEngine.Random.Range(20, 31);
                    break;
                case "social":
                    social += UnityEngine.Random.Range(20, 31);
                    break;
                case "mental":
                    mental += UnityEngine.Random.Range(20, 31);
                    break;
                case "lang":
                    lang += UnityEngine.Random.Range(20, 31);
                    break;
                default:
                    Debug.Log("Failed base stats gen");
                    break;
            }
        }
    }

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
    /*
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
    }*/
}
