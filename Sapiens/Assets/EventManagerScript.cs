using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour {
    TribeScript tribe;
    public LogController Logger;
    List<GatherZoneScript> GatherZones = new List<GatherZoneScript>();
    public List<CharacterScript> Gatherers_Available = new List<CharacterScript>();
    public List<CharacterScript> Gatherers_Unavailable = new List<CharacterScript>();
    public List<EventStruct> EventsTracked = new List<EventStruct>();
    ControlsScript Cs;
    int lastday = 0;
    int lasthour = 0;
    
	void Start ()
    {
        Logger = GameObject.Find("UI_Log").GetComponent<LogController>();
        tribe = GameObject.Find("Tribe").GetComponent<TribeScript>();
        Cs = GameObject.Find("Controls").GetComponent<ControlsScript>();
        lastday = Cs.TS.savedays;
        lasthour = Cs.TS.hours;

    }
	
	void Update () {

        //--------------------------------------------------------------------- To debug : start inside collider makes the gatherzone blocked or maybe because sphere collider ???? AND debug : remove event when done, so as to not overload the list. ---------------------------------------------------//
        if (lastday < Cs.TS.savedays)
        {
            // trigger event if the time is right
            foreach (EventStruct ES in EventsTracked)
            {
                // go gather
                if ((ES.inUse == false) && (ES.Zone.capacity > 0) && (ES.isInRange == true) && (ES.nextStart <= lastday) && (ES.type == "Gather") && (Gatherers_Available.Count > 0))
                {
                    Debug.Log("start using");
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
                // go back to tribe
                else if ((ES.inUse) && (ES.isDone == true))
                {
                    ES.isDone = false;
                    int gather = (int)Random.Range(5.0f, 11.0f);
                    if (ES.Zone.capacity - gather < 0)
                        gather = ES.Zone.capacity;
                    ES.gathered = gather;
                    ES.Zone.capacity -= gather;
                    Vector3 tribeHome = tribe.GetComponent<Transform>().position;
                    tribeHome.y = Cs.CharacterPlane;
                    ES.Char.targetHome = tribeHome;
                    Logger.Add_To_Log("Task : " + ES.Zone.type + " at " + ES.Zone.name + " is done.");
                }
            }
            lastday = Cs.TS.savedays;

        }
        if (lasthour < Cs.TS.savehours)
        {
            foreach (EventStruct ES in EventsTracked)
            {
                if ((ES.inUse == true) && (ES.isCharBack == true))
                {
                    ES.isCharBack = false;
                    ES.inUse = false;
                    Gatherers_Unavailable.Remove(ES.Char);
                    Gatherers_Available.Add(ES.Char);
                    ES.nextStart = Cs.TS.savedays + ES.recurrent;
                    ES.Char.GetComponent<SpriteRenderer>().enabled = false;
                    ES.Char.available = true;
                    if (tribe.TrbFood + ES.gathered <= tribe.TrbMaxFood)
                        tribe.TrbFood += ES.gathered;
                    else
                        tribe.TrbFood = tribe.TrbMaxFood;
                    ES.gathered = 0;
                    Logger.Add_To_Log("Returned : " + ES.Char.pname + " after " + ES.Zone.type + " at " + ES.Zone.name);
                    ES.Char = null;
                    if (ES.Zone.capacity == 0)
                    {
                        ES.Zone.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
            //new hour
            lasthour = Cs.TS.savehours;
        }

    }

    public void RemoveZone(GatherZoneScript GatherZone)
    {
        EventsTracked.Remove(EventsTracked.Find(et => et.Zone == GatherZone));
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
