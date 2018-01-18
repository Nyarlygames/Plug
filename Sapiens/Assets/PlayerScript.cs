using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float Init_speed =0;
    public string pname = "";
    public int plevel =0;
    public int strength = 0;
    public int endu = 0;
    public int body = 0;
    public int mental = 0;
    public int dexte = 0;
    public int accu = 0;
    public int speed = 0;
    public int percept = 0;
    public int surv = 0;
    public int intel = 0;
    public int mem = 0;
    public int chari = 0;
    public int social = 0;
    public int lang = 0;
    Transform pt;
    Transform ct;
    CameraScript cs;
    PlayerScript ps;
    Rigidbody prb;
    LogController Logger;
    public Vector3 targetHit = Vector3.zero;
    GameObject Targetgo;
    

    void Start()
    {
        Targetgo = GameObject.Find("Target");
        prb = gameObject.GetComponent<Rigidbody>();
        pt = gameObject.GetComponent<Transform>();
        ct = gameObject.GetComponent<Transform>();
        cs = Camera.main.GetComponent<CameraScript>();
        ps = gameObject.GetComponent<PlayerScript>();
        Logger = GameObject.Find("UI_Log").GetComponent<LogController>();
        GameObject.Find("Tribename").GetComponent<Text>().text = PlayerPrefs.GetString("Name");
    }

    /*void Update()
    {
    }*/

    void FixedUpdate()
    {

        /* if (Input.GetMouseButtonDown(1))
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
             MovePlayerTo(targetHit);
         }*/


        if (targetHit != Vector3.zero)
        {
            // if target is set, move player
            MovePlayerTo(targetHit);
        }
    }

    void MovePlayerTo(Vector3 target)
    {
        if (pt.position != target)
        {
            // if target not reached, move to target
            prb.MovePosition(Vector3.MoveTowards(pt.position, target, speed * Time.deltaTime));
            // also move camera if player is moving (no need when not moving)
            //cs.MoveToPlayer();
        }
        else
        {
            // target reached, reset target to zero
            targetHit = Vector3.zero;
            //Targetgo.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // trigger = walkable, set height
    void OnTriggerEnter(Collider coll)
    {
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.VisitState == 0)
                Logger.Add_To_Log("Entered: " + ms.RegionName); // to add : region listing so to not respam.
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