using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach(EventSave e in events)
        {
            GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.food += 2;
        }
    }
}
