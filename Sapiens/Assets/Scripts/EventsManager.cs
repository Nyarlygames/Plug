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
        foreach (EventSave e in events.FindAll(e => e.cs.Count > 0))
        {
            if (e.obj.modifiers.ContainsKey("extract_daily"))
            {
                if (Convert.ToInt32(e.obj.modifiers["capacity"]) > Convert.ToInt32(e.obj.modifiers["extract_daily"]))
                {
                    GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.food += Convert.ToInt32(e.obj.modifiers["extract_daily"]);
                    newfoodgain += Convert.ToInt32(e.obj.modifiers["extract_daily"]);
                    e.obj.modifiers["capacity"] = (Convert.ToInt32(e.obj.modifiers["capacity"]) - Convert.ToInt32(e.obj.modifiers["extract_daily"])).ToString();
                }
                else if (Convert.ToInt32(e.obj.modifiers["capacity"]) > 0)
                {
                    GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.food += Convert.ToInt32(e.obj.modifiers["capacity"]);
                    newfoodgain += Convert.ToInt32(e.obj.modifiers["capacity"]);
                    e.obj.modifiers["capacity"] = "0";
                }
            }
            Debug.Log("lol");
        }
        GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.food_gain = newfoodgain;
        events.RemoveAll(e => e.obj.modifiers["capacity"] == "0");
    }
}
