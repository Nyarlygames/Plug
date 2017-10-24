using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 campos = new Vector3(0,0,0);
    public float speed = 3.0f;
    public float max_speed = 8.0f;
    public float acceleration_speed = 3.0f;
    public float camspeed = 3.0f;
    public float max_camspeed = 8.0f;
    public float acceleration_camspeed = 3.0f;
    public int jumpheight = 300;
    public float floating_force = 2.0f;
    private float init_speed = 3.0f;
    private float init_camspeed = 3.0f;
    public bool floating = false;
    // Use this for initialization
    void Start()
    {
        init_speed = speed;
        init_camspeed = speed;
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
            if (floating == true)
            {
                gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity / floating_force;
            }
            else
            {
                if (speed < max_speed)
                {
                    speed += acceleration_speed * Time.deltaTime;
                }
                else
                {
                    speed = max_speed;
                }

                if (camspeed < max_camspeed)
                {
                    camspeed += acceleration_camspeed * Time.deltaTime;
                }
                else
                {
                    camspeed = max_camspeed;
                }
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            if (floating == true)
            {
                gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity / floating_force;
            }
            else
            {
                if (speed < max_speed)
                {
                    speed += acceleration_speed * Time.deltaTime;
                }
                else
                {
                    speed = max_speed;
                }

                if (camspeed < max_camspeed)
                {
                    camspeed += acceleration_camspeed * Time.deltaTime;
                }
                else
                {
                    camspeed = max_camspeed;
                }
            }

        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            if (floating == true)
            {
                Debug.Log("endfloating");
                //gameObject.GetComponent<Rigidbody>().AddForce(0, -jumpheight - 50f, 0); // need to add speed factor
                // gameObject.GetComponent<Rigidbody>().AddForce(0, -jumpheight, 0);
                // start droping
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().AddForce(0, jumpheight + ((speed - 3) * 50) , 0); // need to add speed factor
                // start jumping
                speed = init_speed; // remove acceleration ? more like reduce it
                camspeed = init_camspeed; // remove acceleration ? more like reduce it
                floating = true;
            }

        }
        




        // HERE DO : MoveTO Or AddVelocity
        // gameObject.GetComponent<Transform>().Translate(speed * Time.deltaTime, 0f, 0f);
        gameObject.GetComponent<Transform>().Translate(transform.right * speed * Time.deltaTime);
        Camera.main.GetComponent<Transform>().Translate(transform.right * camspeed * Time.deltaTime);

    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            floating = false;
        }

    }
}
