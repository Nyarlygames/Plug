using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour
{
    Transform pt;
    Transform ct;
    CameraScript cs;
    PlayerScript ps;
    Rigidbody prb;
    Vector3 targetHit = Vector3.zero;

	void Start ()
    {
        prb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        pt = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ct = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cs = Camera.main.GetComponent<CameraScript>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
    
}
