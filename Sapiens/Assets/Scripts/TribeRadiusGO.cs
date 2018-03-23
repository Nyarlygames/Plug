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


    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectGO objgo = collision.gameObject.GetComponent<ObjectGO>();
        if ((objgo != null) && (objgo.objectCur.visitState != 1))
        {
            objgo.SetVisited(1);
        }
        else
        {
            TileGO tilego = collision.gameObject.GetComponent<TileGO>();
            if ((tilego != null) && (tilego.tileCur.visitState != 1))
            {
                tilego.SetVisited(1);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ObjectGO objgo = collision.gameObject.GetComponent<ObjectGO>();
        if ((objgo != null) && (objgo.objectCur.visitState != 2))
        {
            objgo.SetVisited(2);
        }
        else
        {
            TileGO tilego = collision.gameObject.GetComponent<TileGO>();
            if ((tilego != null) && (tilego.tileCur.visitState != 2))
            {
                tilego.SetVisited(2);
            }
        }
    }
}
