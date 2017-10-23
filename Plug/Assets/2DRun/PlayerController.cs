using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 campos = new Vector3(0,0,0);
    public float speed = 2.0f;
	// Use this for initialization
	void Start () {
          gameObject.GetComponent<Transform>().position = new Vector3(-7, GameObject.Find("Ground").GetComponent<Transform>().position.y + GameObject.Find("Ground").GetComponent<SpriteRenderer>().sprite.bounds.size.y/2 + gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 0);
      //  Debug.Log(gameObject.GetComponent<Transform>().position + " base" + GameObject.Find("Ground").GetComponent<Transform>().position.y + " sc" + gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y + " sp" + GameObject.Find("Ground").GetComponent<SpriteRenderer>().sprite.bounds.size.y);
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x + 7, 0, -5);

         gameObject.GetComponent<Transform>().Translate(5f * Time.deltaTime, 0f, 0f);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.tag);

    }
}
