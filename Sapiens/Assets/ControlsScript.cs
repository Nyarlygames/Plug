using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour
{
    Transform pt;
    Transform ct;
    CameraScript cs;
    Rigidbody prb;
    Vector3 targetHit = Vector3.zero;

	void Start ()
    {
        prb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        pt = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ct = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cs = Camera.main.GetComponent<CameraScript>();
    }
	
	// Update is called once per frame
	void Update () {

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
            pt.position = Vector3.MoveTowards(pt.position, target, 10 * Time.deltaTime);
            // also move camera if player is moving (no need when not moving)
            cs.MoveToPlayer();

        }
        else
        {
            // target reached, reset target to zero
            target = Vector3.zero;
        }
        
    }
}
