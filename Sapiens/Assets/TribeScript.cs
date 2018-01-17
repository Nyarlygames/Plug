using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TribeScript : MonoBehaviour {

    public float speed;
    private Transform pt;
    private Transform ct;
    private CameraScript cs;
    private PlayerScript ps;
    private Rigidbody prb;
    private LogController Logger;
    private Vector3 targetHit = Vector3.zero;
    private GameObject Targetgo;
    private GameObject Mouse_go;
    private GameObject PlayerPrefab;
    private bool launch_explore = false;
    private Vector3 targetExplore = Vector3.zero;
    private Sprite Explore_cursor;
    private Sprite Mouse_cursor;

    // Use this for initialization
    void Start ()
    {
        Targetgo = GameObject.Find("Target");
        prb = gameObject.GetComponent<Rigidbody>();
        pt = gameObject.GetComponent<Transform>();
        ct = gameObject.GetComponent<Transform>();
        cs = Camera.main.GetComponent<CameraScript>();
        ps = gameObject.GetComponent<PlayerScript>();
        Logger = GameObject.Find("UI_Log").GetComponent<LogController>();
        GameObject.Find("Tribename").GetComponent<Text>().text = PlayerPrefs.GetString("Name");
        PlayerPrefab = Resources.Load("Play/Prefabs/Player_Prefab") as GameObject;
        Mouse_go = GameObject.Find("Cursor");
        Mouse_cursor = Mouse_go.GetComponent<SpriteRenderer>().sprite;
        var textures = Resources.LoadAll("Play", typeof(Sprite));
        foreach (var t in textures)
        {
            if (t.name == "cursor_explore")
            {
                Explore_cursor = t as Sprite;
            }

        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (launch_explore == false))
        {
            Mouse_go.GetComponent<SpriteRenderer>().sprite = Explore_cursor;
            launch_explore = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && (launch_explore == true))
        {
            Mouse_go.GetComponent<SpriteRenderer>().sprite = Mouse_cursor;
            launch_explore = false;
        }
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
            PlayerPrefab = (GameObject)Instantiate(GameObject.FindWithTag("Player"));
            PlayerPrefab.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            PlayerPrefab.GetComponent<PlayerScript>().targetHit = targetExplore;
            targetExplore = Vector3.zero;

        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if ((Input.GetMouseButtonDown(1)) && (targetHit== Vector3.zero))
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
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            Logger.Add_To_Log("Entered: " + ms.RegionName);
            speed = ms.speed;
            ms.VisitState = 1;
        }
    }
    void OnTriggerStay(Collider coll)
    {
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            speed = ms.speed;
            ms.VisitState = 2;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        speed = 0;
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            ms.VisitState = 1;
        }
    }

    // collision = non walkable, stops movement
    void OnCollisionEnter(Collision coll)
    {
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
