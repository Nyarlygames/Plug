using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectScript : MonoBehaviour {

    public int speed = 0;
    public string Archetype = "";
    public string RegionName = "";
    public int VisitState = 0;
    Color Base;

    void Start () {
        if (gameObject.GetComponent<MeshRenderer>() != null)
            Base = gameObject.GetComponent<MeshRenderer>().material.color;
        if (gameObject.GetComponent<SpriteRenderer>() != null)
            Base = gameObject.GetComponent<SpriteRenderer>().material.color;
        if (VisitState == 0)
        {
            Color black = new Color(0.0f,0.0f,0.0f,0.0f);
            //black.a = 0.1f;
            if (gameObject.GetComponent<MeshRenderer>() != null)
                gameObject.GetComponent<MeshRenderer>().material.color = black;
            if (gameObject.GetComponent<SpriteRenderer>() != null)
                gameObject.GetComponent<SpriteRenderer>().material.color = black;
        }
	}
	

	void Update ()
    {
        if (VisitState == 1)
        {
            Color black = Base;
            black.a = 0.5f;
            if (gameObject.GetComponent<MeshRenderer>() != null)
                gameObject.GetComponent<MeshRenderer>().material.color = black;
            if (gameObject.GetComponent<SpriteRenderer>() != null)
                gameObject.GetComponent<SpriteRenderer>().material.color = black;
        }
        if (VisitState == 2)
        {
            Color black = Base;
            black.a = 1.0f;
            if (gameObject.GetComponent<MeshRenderer>() != null)
                gameObject.GetComponent<MeshRenderer>().material.color = black;
            if (gameObject.GetComponent<SpriteRenderer>() != null)
                gameObject.GetComponent<SpriteRenderer>().material.color = black;
        }

    }
}
