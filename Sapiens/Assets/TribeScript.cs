using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TribeScript : MonoBehaviour {
    
    private Transform tt;
    private Rigidbody trb;
    private LogController Logger;
    public Vector3 targetHit = Vector3.zero;
    public List<GameObject> Characters = new List<GameObject>();
    private ControlsScript Controls;
    public Sprite CampOn;
    public Sprite CampOff;
    SpriteRenderer TribeSprite;
    public GameObject Leader;


    // Tribe stats
    public float TrbUnity=0;
    public float TrbRank=0;
    public int TrbAdults=0;
    public int TrbYoungs=0;
    public float TrbGenerations=0;
    public float TrbFood=0;
    public float TrbWater = 0;
    public float TrbConfort = 0;
    public float TrbSocial = 0;
    public float TrbTools = 0;
    public float TrbCrafts = 0;
    public float TrbHerbs = 0;
    public float TrbSpeed = 0;
    

    void Start()
    {
        // settings variables to reduce GetComponent calls
        trb = gameObject.GetComponent<Rigidbody>();
        tt = gameObject.GetComponent<Transform>();
        Logger = GameObject.Find("UI_Log").GetComponent<LogController>();
        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();
        //load ressources
        GameObject.Find("Tribename").GetComponent<Text>().text = PlayerPrefs.GetString("Name");
        CampOn = Resources.Load<Sprite>("Play/TribeChar/camp");
        CampOff = Resources.Load<Sprite>("Play/TribeChar/camp_night");
        TribeSprite = gameObject.GetComponent<SpriteRenderer>();


        // manual player add
        if (PlayerPrefs.GetString("Seed") != null)
        {

            if (PlayerPrefs.GetString("Seed") == "Seed hack")
            {
                AddChar(0, "Leader", "Play/Prefabs/Leader");
                AddChar(1, "Man2", "Play/Prefabs/Man1");
                AddChar(2, "Man2", "Play/Prefabs/Man2");
                AddChar(3, "Woman1", "Play/Prefabs/Woman1");
                AddChar(4, "Woman2", "Play/Prefabs/Woman2");
                AddChar(5, "Son", "Play/Prefabs/Son");
                AddChar(6, "Daughter", "Play/Prefabs/Daughter");
                AddChar(7, "Elder", "Play/Prefabs/Man2");
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
            Leader = Characters[id];
        }
    }
    

    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (targetHit != Vector3.zero)
        {
            // if target is set, move player
            MoveTribeTo(targetHit);
        }

        //Players
    }

    
    void MoveTribeTo(Vector3 target)
    {
        if (tt.position != target) // move Tribe and available chars, hide camp in movement
        {
            TribeSprite.enabled = false;
            trb.MovePosition(Vector3.MoveTowards(tt.position, target, TrbSpeed * Time.deltaTime));
            target.y = Controls.CharacterPlane;
            foreach (GameObject chars in Characters)
            {
                chars.GetComponent<CharacterScript>().MovePlayerTo(target, TrbSpeed);
            }
        }
        else
        {
            // target reached, reset target and reshow camp
            TribeSprite.enabled = true;
            targetHit = Vector3.zero;
        }
    }

    /*
    // trigger = walkable, set height
    void OnTriggerEnter(Collider coll)
    {
        // entering a new region
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            Logger.Add_To_Log("Entered: " + ms.RegionName);
           // speed = ms.speed;    //Tribe's speed is different from player speed. Need formula to implement.
            ms.VisitState = 1;
        }
    }
    void OnTriggerStay(Collider coll)
    {
        // inside a region
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            // speed = ms.speed;    //Tribe's speed is different from player speed. Need formula to implement.
            ms.VisitState = 2;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        // Exiting a region
        //speed = 0;
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            ms.VisitState = 1;
        }
    }

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
