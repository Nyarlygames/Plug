using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeMembersGO : MonoBehaviour {

    public GameObject radius;
    public GameObject fire;
    public Sprite[] firesprites;
    int firespritespeed = 5;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
       if (firesprites != null)
        {
            fire.GetComponent<SpriteRenderer>().sprite = firesprites[(int) (Time.time * firespritespeed % firesprites.Length)];
        }
    }
}
