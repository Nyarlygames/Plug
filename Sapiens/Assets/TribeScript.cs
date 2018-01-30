using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TribeScript : MonoBehaviour {
    
    private Transform tt;
    private Rigidbody trb;
    public Vector3 targetHit = Vector3.zero;
    public List<GameObject> Characters = new List<GameObject>();
    private ControlsScript Controls;
    public Sprite CampOn;
    public Sprite CampOff;
    SpriteRenderer TribeSprite;
    EventManagerScript EMS;

    // Tribe stats
    public float TrbUnity = 0;
    public float TrbRank = 0;
    public int TrbAdults = 0;
    public int TrbYoungs = 0;
    public float TrbGenerations = 0;
    public float TrbFood = 0;
    public float TrbWater = 0;
    public float TrbConfort = 0;
    public float TrbSocial = 0;
    public float TrbTools = 0;
    public float TrbCrafts = 0;
    public float TrbHerbs = 0;
    public float TrbSpeed = 0;

    // areas
    //public List<GameObject> GatherZones = new List<GameObject>();

    // Activities
    public List<GameObject> TrbGather = new List<GameObject>();
    public List<GameObject> TrbFish = new List<GameObject>();
    public List<GameObject> TrbHunt = new List<GameObject>();
    public List<GameObject> TrbCook = new List<GameObject>();
    public List<GameObject> TrbSource = new List<GameObject>();
    public List<GameObject> TrbManage = new List<GameObject>();
    public List<GameObject> TrbMentor = new List<GameObject>();
    public List<GameObject> TrbSage = new List<GameObject>();
    public List<GameObject> TrbShaman = new List<GameObject>();
    public List<GameObject> TrbSkin = new List<GameObject>();
    public List<GameObject> TrbWood = new List<GameObject>();
    public List<GameObject> TrbStone = new List<GameObject>();
    public List<GameObject> TrbProtect = new List<GameObject>();
    public GameObject TrbLeader;
    public List<GameObject> TrbScout = new List<GameObject>();
    public List<GameObject> TrbRest = new List<GameObject>();
    public List<GameObject> TrbPregnant = new List<GameObject>();


    void Start()
    {
        // settings variables to reduce GetComponent calls
        trb = gameObject.GetComponent<Rigidbody>();
        tt = gameObject.GetComponent<Transform>();
        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();
        //load ressources
        GameObject.Find("Tribename").GetComponent<Text>().text = PlayerPrefs.GetString("Name");
        CampOn = Resources.Load<Sprite>("Play/TribeChar/camp");
        CampOff = Resources.Load<Sprite>("Play/TribeChar/camp_night");
        TribeSprite = gameObject.GetComponent<SpriteRenderer>();
        EMS = GameObject.Find("EventManager").GetComponent<EventManagerScript>();

        // manual player add
        if (PlayerPrefs.GetString("Seed") != null)
        {

            if (PlayerPrefs.GetString("Seed") == "Seed hack")
            {
                AddChar(0, "Leader", "Play/Prefabs/Leader");
                AddChar(1, "Man1", "Play/Prefabs/Man1");
                AddChar(2, "Man2", "Play/Prefabs/Man2");
                AddChar(3, "Woman1", "Play/Prefabs/Woman1");
                AddChar(4, "Woman2", "Play/Prefabs/Woman2");
                AddChar(5, "Son", "Play/Prefabs/Son");
                AddChar(6, "Daughter", "Play/Prefabs/Daughter");
                AddChar(7, "Elder", "Play/Prefabs/Elder");
            }
        }
        foreach (GameObject chars in Characters)
        {
            if (chars.GetComponent<CharacterScript>().speed < TrbSpeed)
            {
                TrbSpeed = chars.GetComponent<CharacterScript>().speed;
            }
        }
    }

    void AddChar(int id, string name, string PrefabUrl)
    {
        GameObject PlayerGo = Resources.Load(PrefabUrl) as GameObject;
        PlayerGo.GetComponent<Transform>().position = new Vector3(tt.position.x, tt.position.z, tt.position.y);
        Characters.Add(Instantiate(PlayerGo));
        Characters[id].name = name;
        CharacterScript ps = Characters[id].GetComponent<CharacterScript>();
        ps.name = name;
        if (ps.status != "Leader")
        {
            ps.status = "Member";
            Characters[id].GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            TrbLeader = Characters[id];
        }
    }
    

    void Update()
    {

    }
    
    void FixedUpdate ()
    {
        Vector3 targetChar = tt.position;
        targetChar.y = Controls.CharacterPlane;
        foreach (GameObject chars in Characters)
        {
            if ((chars.GetComponent<CharacterScript>().available == true) || (chars.GetComponent<CharacterScript>().status == "Leader"))
                chars.GetComponent<Transform>().position = targetChar;
        }

        if (targetHit != Vector3.zero)
        {
            // if target is set, move player
            MoveTribeTo(targetHit);
        }
        else
        {
            TribeSprite.enabled = true;
        }
    }

    
    void MoveTribeTo(Vector3 target)
    {
        if (tt.position != target) // move Tribe and available chars, hide camp in movement
        {
            TribeSprite.enabled = false;
            trb.MovePosition(Vector3.MoveTowards(tt.position, target, TrbSpeed * Time.deltaTime));
        }
        else
        {
            // target reached, reset target and reshow camp
            targetHit = Vector3.zero;
        }
    }

    
    // trigger = walkable, set height
    void OnTriggerEnter(Collider coll)
    {
        /* // entering a new region
         MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
         if (ms != null)
         {
             Logger.Add_To_Log("Entered: " + ms.RegionName);
            // speed = ms.speed;    //Tribe's speed is different from player speed. Need formula to implement.
             ms.VisitState = 1;
         }*/
        if (coll.CompareTag("Gather"))
        {
            if (EMS.EventsTracked.Find(et => et.Zone == coll.gameObject.GetComponent<GatherZoneScript>()) == null)
                EMS.AddZone(coll.gameObject);
            else
                EMS.EventsTracked.Find(et => et.Zone == coll.gameObject.GetComponent<GatherZoneScript>()).isInRange = true;
            // GatherZones.Add(coll.gameObject);
        }
    }
    void OnTriggerStay(Collider coll)
    {
        /*// inside a region
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            // speed = ms.speed;    //Tribe's speed is different from player speed. Need formula to implement.
            ms.VisitState = 2;
        }*/
       // Debug.Log("Stay : " + coll.name);
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Gather"))
        {
            if (EMS.EventsTracked.Find(et => et.Zone == coll.gameObject.GetComponent<GatherZoneScript>()) == null)
                // EMS.AddZone(coll.gameObject);
                Debug.Log("Gather event removed without having been entered => huge failure");
            else
                EMS.EventsTracked.Find(et => et.Zone == coll.gameObject.GetComponent<GatherZoneScript>()).isInRange = false;
            //EMS.RemoveZone(coll.gameObject);
            //GatherZones.Remove(coll.gameObject);
        }
        // Exiting a region
        //speed = 0;
        /*MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            ms.VisitState = 1;
        }*/
      //  Debug.Log("Exit : " + coll.name);
    }
    /*
    // collision = non walkable
    void OnCollisionEnter(Collision coll)
    {
        // Entering collision
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.speed < 0)
            {
                Logger.Add_To_Log("Blocked by: " + ms.RegionName);
                targetHit = Vector3.zero;
                Targetgo.GetComponent<SpriteRenderer>().enabled = false;
                ms.VisitState = 1;
            }
        }
    }
    void OnCollisionStay(Collision coll)
    {
        // Inside collision
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.speed < 0)
            {
                targetHit = Vector3.zero;
                Targetgo.GetComponent<SpriteRenderer>().enabled = false;
                ms.VisitState = 2;
            }
        }
    }
    void OnCollisionExit(Collision coll)
    {
        // Exiting collision
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.speed < 0)
            {
                ms.VisitState = 1;
            }
        }
    }*/
}
