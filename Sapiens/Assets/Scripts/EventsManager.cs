using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour {

    public List<EventSave> events = new List<EventSave>();
    public GameManager GM;
    
	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void newDay()
    {
        float newfoodgain = 0;
        float newwatergain = 0;
        foreach (EventSave e in events.FindAll(e => e.cs.Count > 0)) // make it depend on who does it
        {
            if (e.obj.modifiers.ContainsKey("extract_daily") && e.obj.modifiers.ContainsKey("resource"))
            {
                 foreach(CharacterSave cs in e.cs)
                 {
                    switch (e.obj.modifiers["resource"])
                    {
                        case "food":
                            if (Convert.ToInt32(e.obj.modifiers["capacity"]) > Convert.ToInt32(e.obj.modifiers["extract_daily"]))
                            {
                                GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.food += Convert.ToInt32(e.obj.modifiers["extract_daily"]);
                                newfoodgain += Convert.ToInt32(e.obj.modifiers["extract_daily"]);
                                if (Convert.ToInt32(e.obj.modifiers["capacity"]) != 99999)
                                    e.obj.modifiers["capacity"] = (Convert.ToInt32(e.obj.modifiers["capacity"]) - Convert.ToInt32(e.obj.modifiers["extract_daily"])).ToString();
                            }
                            else if (Convert.ToInt32(e.obj.modifiers["capacity"]) > 0)
                            {
                                GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.food += Convert.ToInt32(e.obj.modifiers["capacity"]);
                                newfoodgain += Convert.ToInt32(e.obj.modifiers["capacity"]);
                                e.obj.modifiers["capacity"] = "0";
                            }
                            break;
                        case "water":
                            if (Convert.ToInt32(e.obj.modifiers["capacity"]) > Convert.ToInt32(e.obj.modifiers["extract_daily"]))
                            {
                                GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.water += Convert.ToInt32(e.obj.modifiers["extract_daily"]);
                                newwatergain += Convert.ToInt32(e.obj.modifiers["extract_daily"]);
                                if (Convert.ToInt32(e.obj.modifiers["capacity"]) != 99999)
                                    e.obj.modifiers["capacity"] = (Convert.ToInt32(e.obj.modifiers["capacity"]) - Convert.ToInt32(e.obj.modifiers["extract_daily"])).ToString();
                            }
                            else if (Convert.ToInt32(e.obj.modifiers["capacity"]) > 0)
                            {
                                GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.water += Convert.ToInt32(e.obj.modifiers["capacity"]);
                                newwatergain += Convert.ToInt32(e.obj.modifiers["capacity"]);
                                e.obj.modifiers["capacity"] = "0";
                            }
                            break;
                        default:
                            // unrecognizable resource
                            break;
                    }
                    //cs.AddActivityGain(e.obj.modifiers["activity"]);
                 }
            }
            // else not a gain or not a resource
        }
        GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.food_gain = newfoodgain;
        GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.water_gain = newwatergain;
        events.RemoveAll(e => e.obj.modifiers["capacity"] == "0");
    }
}
