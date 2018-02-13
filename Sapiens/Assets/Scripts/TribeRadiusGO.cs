using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeRadiusGO : MonoBehaviour {
    GameObject[] triggers;

	// Use this for initialization
	void Start () {
        triggers = GameObject.FindGameObjectsWithTag("trigger");
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*foreach (GameObject trigger in triggers)
        {
            if ((gameObject.GetComponent<Transform>().position.x + gameObject.GetComponent<CircleCollider2D>().radius > trigger.GetComponent<BoxCollider2D>().bounds.center.x - trigger.GetComponent<BoxCollider2D>().bounds.extents.x) &&
                (gameObject.GetComponent<Transform>().position.x - gameObject.GetComponent<CircleCollider2D>().radius < trigger.GetComponent<BoxCollider2D>().bounds.center.x + trigger.GetComponent<BoxCollider2D>().bounds.extents.x) &&
                (gameObject.GetComponent<Transform>().position.y + gameObject.GetComponent<CircleCollider2D>().radius > trigger.GetComponent<BoxCollider2D>().bounds.center.y - trigger.GetComponent<BoxCollider2D>().bounds.extents.y) &&
                (gameObject.GetComponent<Transform>().position.y - gameObject.GetComponent<CircleCollider2D>().radius < trigger.GetComponent<BoxCollider2D>().bounds.center.y + trigger.GetComponent<BoxCollider2D>().bounds.extents.y))
            {
                Debug.Log("touching " + trigger.name);
            }
        }*/


    }
    
}
