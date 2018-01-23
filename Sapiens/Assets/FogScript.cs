using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour {

    float last_update = 0.0f;
    SpriteRenderer Base;
    // Use this for initialization
    void Start ()
    {
        Base = GameObject.Find("Fog").GetComponent<SpriteRenderer>();
        Base.color = new Color(1f, 1f, 1f, 0.0f);
    }

    // Update is called once per frame
    void Update () {
    }
}
