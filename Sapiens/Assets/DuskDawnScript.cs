﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuskDawnScript : MonoBehaviour {

    Transform BaseT;
    SpriteRenderer Base;
    // Use this for initialization
    void Start()
    {
        Base = GameObject.Find("DuskDawn").GetComponent<SpriteRenderer>();
        Base.color = new Color(1f, 1f, 1f, 0.0f);

        BaseT = GameObject.Find("DuskDawn").GetComponent<Transform>();
        GameObject.Find("DuskDawn").GetComponent<Transform>().position = new Vector3(BaseT.position.x, 2.5f, BaseT.position.y); // re-up fog on start-up (or do it in editor variable one day maybe)
    }

    // Update is called once per frame
    void Update () {
    }
}
