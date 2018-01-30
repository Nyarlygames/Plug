using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

    GameObject Fow;

	// Use this for initialization
	void Start () {
        Fow = GameObject.Find("FogOfWar");
        if (Fow.GetComponent<SpriteRenderer>().material != null) {
            /*Debug.Log(Fow.GetComponent<SpriteRenderer>().sprite.texture.width + " h " + Fow.GetComponent<SpriteRenderer>().sprite.texture.height);
            Debug.Log(Fow.GetComponent<SpriteRenderer>().sprite.rect + " r " + Fow.GetComponent<SpriteRenderer>().sprite.textureRect);
            Debug.Log(Fow.GetComponent<SpriteRenderer>().sprite.bounds.size + " border " + Fow.GetComponent<SpriteRenderer>().sprite.bounds.extents);
            */
            //Vector3 mid = new Vector3(Fow.GetComponent<SpriteRenderer>().sprite.bounds.extents.x * Fow.GetComponent<Transform>().localScale.x, 3.0f, Fow.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
           // Fow.GetComponent<Transform>().position = mid ;

            //Debug.Log(Fow.GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("Sight_Radius").GetComponent<SpriteRenderer>().bounds));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
