using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RatioFactory {

    public float exp_baby_range = 10.0f;
    public float exp_baby_value = 1.0f;

    public float exp_teen_range = 15f;
    public float exp_teen_value = 2.0f;

    public float exp_adult_range = 35f;
    public float exp_adult_value = 0.5f;

    public float exp_senior_range = 35f;
    public float exp_senior_value = 0.5f;

    public float level_basegrowing = 150;
    public float level_basetraining = 150;
    public float level_basemastering = 150;
    //public float level_growth = 10;
    //public float level_training = 20;

    public float ratio_growth = 1.2f;
    public float ratio_training = 1.1f;
    public float ratio_mastering = 1.0f;

    public float tribe_sightradius = 2.3f;

    public float exp_gatherer_basegain = 2.0f;
    public float exp_gatherer_baselvl = 400.0f;
    public float exp_gatherer_ratiogain = -0.1f;
    public float exp_gatherer_ratiolvl = 1.1f;

    public float exp_hunter_basegain = 2.0f;
    public float exp_hunter_baselvl = 400.0f;
    public float exp_hunter_ratiogain = -0.1f;
    public float exp_hunter_ratiolvl = 1.1f;

    public float exp_fisher_basegain = 2.0f;
    public float exp_fisher_baselvl = 400.0f;
    public float exp_fisher_ratiogain = -0.1f;
    public float exp_fisher_ratiolvl = 1.1f;

    public float exp_sourcer_basegain = 2.0f;
    public float exp_sourcer_baselvl = 400.0f;
    public float exp_sourcer_ratiogain = -0.1f;
    public float exp_sourcer_ratiolvl = 1.1f;

    public float exp_cook_basegain = 1.5f;
    public float exp_cook_baselvl = 250.0f;
    public float exp_cook_ratiogain = -0.1f;
    public float exp_cook_ratiolvl = 1.1f;

    public float exp_manager_basegain = 1.5f;
    public float exp_manager_baselvl = 250.0f;
    public float exp_manager_ratiogain = -0.1f;
    public float exp_manager_ratiolvl = 1.1f;

    public float exp_mentor_basegain = 1.5f;
    public float exp_mentor_baselvl = 250.0f;
    public float exp_mentor_ratiogain = -0.1f;
    public float exp_mentor_ratiolvl = 1.1f;

    public float exp_sage_basegain = 1.5f;
    public float exp_sage_baselvl = 300.0f;
    public float exp_sage_ratiogain = -0.1f;
    public float exp_sage_ratiolvl = 1.1f;

    public float exp_shaman_basegain = 1.5f;
    public float exp_shaman_baselvl = 450.0f;
    public float exp_shaman_ratiogain = -0.1f;
    public float exp_shaman_ratiolvl = 1.2f;

    public float exp_skincraft_basegain = 1.5f;
    public float exp_skincraft_baselvl = 400.0f;
    public float exp_skincraft_ratiogain = -0.1f;
    public float exp_skincraft_ratiolvl = 1.2f;

    public float exp_woodcraft_basegain = 1.5f;
    public float exp_woodcraft_baselvl = 400.0f;
    public float exp_woodcraft_ratiogain = -0.1f;
    public float exp_woodcraft_ratiolvl = 1.2f;

    public float exp_stonecraft_basegain = 1.5f;
    public float exp_stonecraft_baselvl = 400.0f;
    public float exp_stonecraft_ratiogain = -0.1f;
    public float exp_stonecraft_ratiolvl = 1.2f;

    public float exp_protector_basegain = 2.0f;
    public float exp_protector_baselvl = 400.0f;
    public float exp_protector_ratiogain = -0.1f;
    public float exp_protector_ratiolvl = 1.1f;

    public float exp_leader_basegain = 2.0f;
    public float exp_leader_baselvl = 450.0f;
    public float exp_leader_ratiogain = -0.1f;
    public float exp_leader_ratiolvl = 1.1f;

    public float exp_scout_basegain = 2.0f;
    public float exp_scout_baselvl = 400.0f;
    public float exp_scout_ratiogain = -0.1f;
    public float exp_scout_ratiolvl = 1.1f;

    public float exp_pregnant_basegain = 2.0f;

    public float ratio_food_strength = 0.8f;
    public float ratio_food_body = 0.8f;
    public float ratio_food_endu = 0.4f;

    public int prereq_source_endu = 60;
    public int prereq_source_percept = 60;

    public int prereq_mentor_spirit = 80;
    public int prereq_mentor_lang = 75;

    public int prereq_gatherHigh_endu = 70;
    public int prereq_gatherHigh_percept = 65;
    public int prereq_gatherHigh_auto = 65;

    public int prereq_fishHigh_dexte = 70;
    public int prereq_fishHigh_strength = 65;
    public int prereq_fishHigh_auto = 65;

    public int prereq_huntHigh_endu = 75;
    public int prereq_huntHigh_percept = 65;
    public int prereq_huntHigh_accu = 65;
    public int prereq_huntHigh_auto = 65;

    public int prereq_sourceHigh_percept = 75;
    public int prereq_sourceHigh_endu = 65;
    public int prereq_sourceHigh_auto = 65;

    public int prereq_cookHigh_social = 65;
    public int prereq_cookHigh_spirit = 65;
    public int prereq_cookHigh_dexte = 65;

    public int simulate_activity_successmax = 10;
    public int simulate_activity_successmin = 8;
    public int simulate_activity_secondary_successmax = 10;
    public int simulate_activity_secondary_successmin = 6;

    public int growing_point_min = 1;
    public int growing_point_max = 2;
    public int growing_stats_min = 8;
    public int growing_stats_max = 10;

    public int training_point_min = 2;
    public int training_point_max = 3;
    public int training_stats_min = 6;
    public int training_stats_max = 10;

    public int mastering_point_min = 2;
    public int mastering_point_max = 5;
    public int mastering_stats_min = 4;
    public int mastering_stats_max = 6;


    public int adult_age = 15;
}
