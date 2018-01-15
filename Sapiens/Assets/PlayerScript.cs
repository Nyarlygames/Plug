using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speed;
    public int height;
    Transform pt;
    Transform ct;
    CameraScript cs;
    PlayerScript ps;
    Rigidbody prb;
    Vector3 targetHit = Vector3.zero;

    void Start()
    {

        prb = gameObject.GetComponent<Rigidbody>();
        pt = gameObject.GetComponent<Transform>();
        ct = gameObject.GetComponent<Transform>();
        cs = Camera.main.GetComponent<CameraScript>();
        ps = gameObject.GetComponent<PlayerScript>();
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
        switch (height)
        {
            case 0:
                speed = 20;
                break;
            case 100:
                speed = 15;
                break;
            case 2000:
                speed = 5;
                break;

        }


        if (pt.position != target)
        {
            // if target not reached, move to target
            prb.MovePosition(Vector3.MoveTowards(pt.position, target, ps.speed * Time.deltaTime));
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
            height = ms.height;
        }
    }
    void OnTriggerStay(Collider coll)
    {
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            height = ms.height;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        height = 0;
    }

    // collision = non walkable, stops movement
    void OnCollisionEnter(Collision coll)
    {
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.height < 0)
            {
                targetHit = Vector3.zero;
            }
        }
    }
    void OnCollisionStay(Collision coll)
    {
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.height < 0)
            {
                targetHit = Vector3.zero;
            }
        }
    }
}