using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 campos = new Vector3(0,0,0);
    public float speed;
    public bool floating = false;
    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {


        /*
         * Floating :
         * - Input pressed : float
         * - Input released : drop
         */
        /*
         * Ground :
         * - Input pressed : run
         * - Input released : jump
         */
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (floating == false)
            {
                // start floating
            }
            else
            {
              //  speed += 1.0f;
            }
        }




        // HERE DO : MoveTO Or AddVelocity
        // gameObject.GetComponent<Transform>().Translate(speed * Time.deltaTime, 0f, 0f);
        Vector3 advance = new Vector3(gameObject.GetComponent<Transform>().position.x + speed, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
        gameObject.GetComponent<Transform>().Translate(transform.right * speed * Time.deltaTime);
        Camera.main.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x + 7, 0, -5);
        
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.tag);

    }
}
