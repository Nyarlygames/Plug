using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TribeScript : MonoBehaviour {

    public float speed=0;
    private Transform pt;
    private CameraScript cs;
    private Rigidbody prb;
    private LogController Logger;
    public Vector3 targetHit = Vector3.zero;
    private GameObject Targetgo;
    private GameObject Mouse_go;
    private GameObject PlayerPrefab;
    private bool launch_explore = false;
    private Vector3 targetExplore = Vector3.zero;
    private Sprite Explore_cursor;
    private Sprite Mouse_cursor;
    public int member_count = 0;
    private List<GameObject> Players = new List<GameObject>();
    private ControlsScript Controls;
    public Sprite CampOn;// = new Sprite();
    public Sprite CampOff;// = new Sprite();
    public List<GameObject> StarterPack = new List<GameObject>();

    void Start()
    {
        // settings variables to reduce GetComponent calls
        Targetgo = GameObject.Find("Target");
        prb = gameObject.GetComponent<Rigidbody>();
        pt = gameObject.GetComponent<Transform>();
        cs = Camera.main.GetComponent<CameraScript>();
     //   ps = gameObject.GetComponent<PlayerScript>();
        Logger = GameObject.Find("UI_Log").GetComponent<LogController>();
        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();
        //load ressources
        GameObject.Find("Tribename").GetComponent<Text>().text = PlayerPrefs.GetString("Name");
        PlayerPrefab = Resources.Load("Play/Prefabs/Player_Prefab") as GameObject;
        CampOn = Resources.Load<Sprite>("Play/TribeChar/camp");
        CampOff = Resources.Load<Sprite>("Play/TribeChar/camp_night");
        Mouse_go = GameObject.Find("Cursor");
        Mouse_cursor = Mouse_go.GetComponent<SpriteRenderer>().sprite;
        var textures = Resources.LoadAll("Play", typeof(Sprite)); // must use load for cursor. need to check why it fails. or else actively use every sprite in that array
        foreach (var t in textures)
        {
            if (t.name == "cursor_explore")
            {
                Explore_cursor = t as Sprite;
            }

        }



        Time.timeScale = 3.5f;


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
                /*  stats = new int[15];
                  stats[0] = 0;
                  stats[1] = 1;
                  stats[2] = 2;
                  stats[3] = 3;
                  stats[4] = 4;
                  stats[5] = 5;
                  stats[6] = 6;
                  stats[7] = 1;
                  stats[8] = 8;
                  stats[9] = 9;
                  stats[10] = 10;
                  stats[11] = 11;
                  stats[12] = 12;
                  stats[13] = 13;
                  stats[14] = 14;*/
            }
        }

    }

    void AddChar(int id, string name, string PrefabUrl)
    {
        GameObject PlayerGo = Resources.Load(PrefabUrl) as GameObject;
        PlayerGo.GetComponent<Transform>().position = new Vector3(pt.position.x, pt.position.z, pt.position.y);
        Players.Add(Instantiate(PlayerGo));
        Players[id].name = name;
        PlayerScript ps = Players[id].GetComponent<PlayerScript>();
        ps.name = name;
        ps.tribe = gameObject;
       /* ps.plevel = stats[0];
        ps.strength = stats[1];
        ps.endu = stats[2];
        ps.body = stats[3];
        ps.mental = stats[4];
        ps.dexte = stats[5];
        ps.accu = stats[6];
        ps.speed = stats[7];
        ps.percept = stats[8];
        ps.surv = stats[9];
        ps.intel = stats[10];
        ps.mem = stats[11];
        ps.chari = stats[12];
        ps.social = stats[13];
        ps.lang = stats[14];*/
        member_count++;
    }
    

    void Update()
    {
        // change the cursor for exploration, and activate left click
        if (Input.GetKeyDown(KeyCode.E) && (launch_explore == false))
        {
            Mouse_go.GetComponent<SpriteRenderer>().sprite = Explore_cursor;
            launch_explore = true;
        }
        // cancel exploration
        else if (Input.GetKeyDown(KeyCode.E) && (launch_explore == true))
        {
            Mouse_go.GetComponent<SpriteRenderer>().sprite = Mouse_cursor;
            launch_explore = false;
        }
        // send a player to explore at mouse position
        if ((Input.GetMouseButtonDown(0)) && ((launch_explore == true) && (targetExplore == Vector3.zero)))
        {
            launch_explore = false;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                targetExplore = hit.point;
                targetExplore.y = 2.1f;
            }


            PlayerPrefab = Players[Controls.selectedChar];
            PlayerPrefab.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            PlayerPrefab.GetComponent<PlayerScript>().targetHit = targetExplore;
            targetExplore = Vector3.zero;
            Logger.Add_To_Log("Sent Player" + PlayerPrefab.name + " to explore");

        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // if mouse click, get hit coordinates on the map, and readjust y for 2D movements;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                targetHit = hit.point;
                targetHit.y = 2.1f;
                Targetgo.GetComponent<Transform>().position = targetHit;
                Targetgo.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        


        if (targetHit != Vector3.zero)
        {
            // if target is set, move player
            MoveTribeTo(targetHit);
        }
    }

    void MoveTribeTo(Vector3 target)
    {
        if (pt.position != target)
        {
            // if target not reached, move to target
            prb.MovePosition(Vector3.MoveTowards(pt.position, target, speed * Time.deltaTime));
            // also move camera if player is moving (no need when not moving)
            cs.MoveToTribe();
        }
        else
        {
            // target reached, reset target to zero
            targetHit = Vector3.zero;
            Targetgo.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


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
        speed = 0;
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
    }
}
