using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour {
    TribeScript tribe;
    public LogController Logger;
    List<GatherZoneScript> GatherZones = new List<GatherZoneScript>();
    List<GameObject> Gatherers;
    public List<CharacterScript> Gatherers_Available = new List<CharacterScript>();
    public List<CharacterScript> Gatherers_Unavailable = new List<CharacterScript>();
    List<EventStruct> EventsTracked = new List<EventStruct>();
    ControlsScript Cs;
    int lastday = 0;
    int lasthour = 0;
    
	void Start ()
    {
        Logger = GameObject.Find("UI_Log").GetComponent<LogController>();
        tribe = GameObject.Find("Tribe").GetComponent<TribeScript>();
        Gatherers = tribe.TrbGather;
        Cs = GameObject.Find("Controls").GetComponent<ControlsScript>();
        lastday = Cs.TS.savedays;
        lasthour = Cs.TS.hours;

    }
	
	void Update () {


       /* foreach (GatherZoneScript GatherZone in GatherZones)
        {
            if (GatherZone.isUsed == false)
            {
                EventStruct test = new EventStruct;

            }
        }*/

        foreach (EventStruct ES in EventsTracked)
        {
            if ((ES.inUse == false) && (Gatherers_Available.Count > 0))
            {
                ES.inUse = true;
                ES.Zone.isUsed = true;
                ES.Char = Gatherers_Available[0];
                ES.Char.available = false;
                Vector3 target = ES.Zone.gameObject.GetComponent<Transform>().position;
                target.y = Cs.CharacterPlane;
                ES.Char.targetHit = target;
                ES.Char.GetComponent<SpriteRenderer>().enabled = true;
                Gatherers_Unavailable.Add(Gatherers_Available[0]);
                Gatherers_Available.RemoveAt(0);
                Logger.Add_To_Log("Sending : " + ES.Char.pname + " to " + ES.Zone.type + " at " + ES.Zone.name);
            }
        }


        if (lastday < Cs.TS.savedays)
        {
            // new day
            lastday = Cs.TS.savedays;

        }
        if (lasthour < Cs.TS.savehours)
        {
            //new hour
            lasthour = Cs.TS.savehours;
        }

    }

    public void AddZone(GameObject Gatherzone)
    {
        switch (Gatherzone.tag) {
            case "Gather":
                //AddEvent(Gzs.type, 1, Gatherzone);
                GatherZoneScript Gzs = Gatherzone.GetComponent<GatherZoneScript>();
                GatherZones.Add(Gzs);
                EventStruct ES = new EventStruct();
                ES.type = Gzs.type;
                ES.recurrent = Gzs.recurrent;
                ES.nextStart = 0;
                ES.inUse = Gzs.isUsed;
                ES.Zone = Gzs;
                EventsTracked.Add(ES);
                break;
            default:
                break;
        }
    }

    void AddEvent(string type, int duration, GameObject gz)
    {

    }
}
