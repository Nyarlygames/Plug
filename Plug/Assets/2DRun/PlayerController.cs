using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 campos = new Vector3(0,0,0);
    public float speed = 3.0f;
    public float acceleration_speed = 3.0f;
    public float max_speed = 8.0f;
    public int jumpheight = 300;
    private float init_speed = 3.0f;
    public bool floating = false;
    // Use this for initialization
    void Start()
    {
        init_speed = speed;
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
                // start floating
            }
            else
            {
                if (speed < max_speed)
                {
                    speed += acceleration_speed * Time.deltaTime;
                }
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            if (floating == true)
            {
                // continue floating
            }
            else
            {
                if (speed < max_speed)
                {
                    speed += acceleration_speed * Time.deltaTime;
                }
            }

        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (floating == true)
            {
                // start droping
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().AddForce(0, jumpheight, 0);
                // start jumping
                speed = init_speed; // remove acceleration ? more like reduce it
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
