﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    
    public float Init_speed = 0;
    public bool available = true;
    public string status;
    public string pname = "";
    public TimeStruct Age = new TimeStruct();
    public float age = 0;
    public int plevel = 0;
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

    public int exp = 0;
    public int moral = 0;
    public int fatigue = 0;
    public int respect = 0;
    public int life = 0;
    public int speed_attr = 0;
    public int food = 0;
    public int resdis = 0;
    public int healdis = 0;
    public int reswoun = 0;
    public int healwoun = 0;

    Transform pt;
    Rigidbody prb;
    TribeScript tribe;
    LogController Logger;
    public Vector3 targetHit = Vector3.zero;
    

    void Start()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, 2.2f, gameObject.GetComponent<Transform>().position.y);
        prb = gameObject.GetComponent<Rigidbody>();
        pt = gameObject.GetComponent<Transform>();
        Logger = GameObject.Find("UI_Log").GetComponent<LogController>();
        GameObject.Find("Tribename").GetComponent<Text>().text = PlayerPrefs.GetString("Name");
        tribe = GameObject.Find("Tribe").GetComponent<TribeScript>();
        if (age > 15)
            tribe.TrbAdults++;
        else
            tribe.TrbYoungs++;
        tribe.TrbUnity += moral;
        tribe.TrbRank += exp;
    }

    void FixedUpdate()
    {
        
       /* if (targetHit != Vector3.zero)
        {
            // if target is set, move player
            MovePlayerTo(targetHit);
        }*/
    }

    public void MovePlayerTo(Vector3 target, float speedMove)
    {
        if (pt.position != target)
        {
            // if target not reached, move to target
            prb.MovePosition(Vector3.MoveTowards(pt.position, target, speedMove * Time.deltaTime));
        }
        else
        {
            // target reached, reset target to zero
            targetHit = Vector3.zero;
        }
    }




    /*// trigger = walkable, set height
    void OnTriggerEnter(Collider coll)
    {
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            if (ms.VisitState == 0)
                Logger.Add_To_Log("Entered: " + ms.RegionName); // to add : region listing so to not respam.
           // speed = ms.speed;
            ms.VisitState = 1;
        }
    }
    void OnTriggerStay(Collider coll)
    {
        MapObjectScript ms = coll.gameObject.GetComponent<MapObjectScript>();
        if (ms != null)
        {
            //speed = ms.speed;
            ms.VisitState = 2;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        //speed = 0;
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
    }*/
}