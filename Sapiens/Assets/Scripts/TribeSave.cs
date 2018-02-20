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
    public float gatherer_level = 0;
    public float gatherer_xp = 0;
    public float hunter_level = 0;
    public float hunter_xp = 0;
    public float sourcer_level = 0;
    public float sourcer_xp = 0;
    public float fisher_level = 0;
    public float fisher_xp = 0;
    public int rank = 0;
    public RatioFactory RF = new RatioFactory();

    public void SetAge()
    {
        age.days = (int)time / 24;
        age.years = (int)time / (24 * 365);
        age.hours = (int)time;
    }

    public void AddActivityGain(string activity, CharacterSave cs)
    {
         switch (activity) {
             case "hunt":
                hunter_xp += RF.exp_hunter_lvlgain;
                cs.AddActivityGain(RF.exp_hunter_lvlgain);
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
                cs.AddActivityGain(RF.exp_fisher_lvlgain);
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
                cs.AddActivityGain(RF.exp_gatherer_lvlgain);
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
                cs.AddActivityGain(RF.exp_sourcer_lvlgain);
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
    

}
