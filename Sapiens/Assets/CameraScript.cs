using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Transform Ppos;
    Transform Cpos;
    float init_zoom = 0;

    void Start () {
        Ppos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Cpos = gameObject.GetComponent<Transform>();
        init_zoom = Cpos.position.y;

    }
	
	void Update ()
    {
	}

    public void MoveToPlayer()
    {
        Cpos.position = new Vector3(Ppos.position.x, init_zoom, Ppos.position.z);
    }
}
