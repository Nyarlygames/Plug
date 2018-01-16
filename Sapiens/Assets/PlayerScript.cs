using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float speed;
    Transform pt;
    Transform ct;
    CameraScript cs;
    PlayerScript ps;
    Rigidbody prb;
    LogController Logger;
    Vector3 targetHit = Vector3.zero;
    

    void Start()
    {

        prb = gameObject.GetComponent<Rigidbody>();
        pt = gameObject.GetComponent<Transform>();
        ct = gameObject.GetComponent<Transform>();
        cs = Camera.main.GetComponent<CameraScript>();
        ps = gameObject.GetComponent<PlayerScript>();
        Logger = GameObject.Find("UI_Log").GetComponent<LogController>();
        Debug.Log("1 " + PlayerPrefs.GetString("Name") + " 2 " + GameObject.Find("Tribename").GetComponent<Text>().text);
        GameObject.Find("Tribename").GetComponent<Text>().text = PlayerPrefs.GetString("Name");
    }

    /*void Update()
    {
    }*/

    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(1))
        {
            // if mouse click, get hit coordinates on the map, and readjust y for 2D movements;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                targetHit = hit.point;
                targetHit.y = 2;
            }
        }

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
            cs.MoveToPlayer();
        }
        else
        {
            // target reached, reset target to zero
            targetHit = Vector3.zero;
        }
    }

    // trigger = walkable, set height
    void OnTriggerEnter(Collider coll)
    {
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            Logger.Add_To_Log(ms.RegionName);
            speed = ms.speed;
        }
    }
    void OnTriggerStay(Collider coll)
    {
        Debug.Log("stay " + coll.gameObject.name);
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            speed = ms.speed;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        Debug.Log("exited " + coll.gameObject.name);
        speed = 0;
    }

    // collision = non walkable, stops movement
    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("entered " + coll.gameObject.name);
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.speed < 0)
            {
                targetHit = Vector3.zero;
            }
        }
    }
    void OnCollisionStay(Collision coll)
    {
        Debug.Log("exited " + coll.gameObject.name);
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.speed < 0)
            {
                targetHit = Vector3.zero;
            }
        }
    }
}