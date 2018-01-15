using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Transform Ppos;
    Transform Cpos;

    void Start () {
        Ppos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Cpos = gameObject.GetComponent<Transform>();

    }
	
	void Update ()
    {
	}

    public void MoveToPlayer()
    {
        Cpos.position = new Vector3(Ppos.position.x, 20, Ppos.position.z);
    }
}
