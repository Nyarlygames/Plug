using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {
    
    public GameObject[] bgs;
    public float SpeedLarge;
    public float SpeedMiddle;
    public float SpeedClose;

    // Use this for initialization
    void Start () {
        bgs = GameObject.FindGameObjectsWithTag("Parallax");
        foreach (GameObject bg in bgs)
        {
            if (bg.name == "Large")
            {
                bg.GetComponent<Transform>().Translate(-SpeedLarge * Time.deltaTime, 0f, 0f);
            }
            if (bg.name == "Middle") { 
                bg.GetComponent<Transform>().Translate(-SpeedMiddle * Time.deltaTime, 0f, 0f);
            }
            if (bg.name == "Close")
            {
                bg.GetComponent<Transform>().Translate(-SpeedClose * Time.deltaTime, 0f, 0f);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject bg in bgs)
        {
            if (bg.GetComponent<SpriteRenderer>().isVisible == true) {
                if (bg.name == "Large")
                {
                    bg.GetComponent<Transform>().Translate(-SpeedLarge * Time.deltaTime, 0f, 0f);
                }
                if (bg.name == "Middle")
                {
                    bg.GetComponent<Transform>().Translate(-SpeedMiddle * Time.deltaTime, 0f, 0f);
                }
                if (bg.name == "Close")
                {
                    bg.GetComponent<Transform>().Translate(-SpeedClose * Time.deltaTime, 0f, 0f);
                }
            }
        }
    }
}
