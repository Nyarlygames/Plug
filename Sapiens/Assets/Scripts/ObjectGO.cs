using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectGO : MonoBehaviour {

    public ObjectSave objectCur;
    GameManager GM;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.tag == "trigger")
        {
            foreach (GameObject radius in GameObject.FindGameObjectsWithTag("radius"))
            {
                if ((radius.GetComponent<Transform>().position.x + radius.GetComponent<CircleCollider2D>().radius > gameObject.GetComponent<BoxCollider2D>().bounds.center.x - gameObject.GetComponent<BoxCollider2D>().bounds.extents.x) &&
                    (radius.GetComponent<Transform>().position.x - radius.GetComponent<CircleCollider2D>().radius < gameObject.GetComponent<BoxCollider2D>().bounds.center.x + gameObject.GetComponent<BoxCollider2D>().bounds.extents.x) &&
                    (radius.GetComponent<Transform>().position.y + radius.GetComponent<CircleCollider2D>().radius > gameObject.GetComponent<BoxCollider2D>().bounds.center.y - gameObject.GetComponent<BoxCollider2D>().bounds.extents.y) &&
                    (radius.GetComponent<Transform>().position.y - radius.GetComponent<CircleCollider2D>().radius < gameObject.GetComponent<BoxCollider2D>().bounds.center.y + gameObject.GetComponent<BoxCollider2D>().bounds.extents.y))
                {
                    EventSave es = new EventSave();
                    es.obj = objectCur;
                    es.id = GM.EM.events.Count;
                    if (GM.EM.events.Exists(e => e.obj.id == objectCur.id) == false)
                    {
                        GM.EM.events.Add(es);
                    }
                }
                else
                    GM.EM.events.RemoveAll(e => e.obj.id == objectCur.id);
            }
        }
    }
}
