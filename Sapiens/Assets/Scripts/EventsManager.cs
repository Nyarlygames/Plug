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
        foreach (EventSave e in events)
        {
            if (e.obj.modifiers.ContainsKey("extract_daily"))
            {
                GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.food += Convert.ToInt32(e.obj.modifiers["extract_daily"]);
                e.obj.modifiers["capacity"] = (Convert.ToInt32(e.obj.modifiers["capacity"]) - Convert.ToInt32(e.obj.modifiers["extract_daily"])).ToString();
            }
        }
    }
}
