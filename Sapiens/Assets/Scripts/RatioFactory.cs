using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RatioFactory {

    public float exp_baby_range = 10.0f;
    public float exp_baby_value = 1f;
    public float exp_teen_range = 15f;
    public float exp_teen_value = 2f;
    public float exp_adult_range = 35f;
    public float exp_adult_value = 0.5f;

    public float level_baserequirement = 150;
    public float level_growth = 10;
    public float level_training = 20;
    public float ratio_growth = 1.2f;
    public float ratio_training = 1.1f;
    public float ratio_mastering = 1.0f;

    public float tribe_sightradius = 2.3f;

    public float exp_gather_lvl1 = 35f;
    public float exp_gather_levelup = 0.1f;
    public float exp_fish_lvl1 = 2.0f;
    public float exp_fish_levelup = 0.1f;

    public float ratio_food_strength = 0.8f;
    public float ratio_food_body = 0.8f;
    public float ratio_food_endu = 0.4f;

    public int adult_age = 15;
}
