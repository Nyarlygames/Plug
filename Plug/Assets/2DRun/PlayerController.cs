using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
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
    public bool canclimb = false;
    public bool Paused = false;
    private bool floatinglock = false;
    public bool isloaded = false;
    public bool initgo = false;
    private Sprite[] anim_walk;
    private float deltaTime = 0;
    private float starttime = 0;
    private int reverser = 1;
    private int frame = 0;
    private string climbcoll = "";
    private string groundcoll = "";

    // Use this for initialization
    void Start()
    {
        init_speed = speed;
        init_camspeed = speed;
        anim_walk = Resources.LoadAll<Sprite>("img/Players/Player_Run");
    }

    void OnBecameInvisible()
    {
    }
    void OnBecameVisible()
    {
        initgo = true;  // after cam is set
        //or for later purposes : invisibility?
    }

    void run_anim(Sprite[] anim, float slicetime)
    {
        //Keep track of the time that has passed
        deltaTime += Time.deltaTime; // deleta varies between 0 and frameseconds.

        if (deltaTime >= starttime) // hit frameseconds => goto next frame
        {
            frame += reverser;
            starttime += slicetime;
            if (frame >= anim.Length)
            {
                frame = anim.Length - 2; // maximum sprite => reverse order (--)
                reverser = -1;
            }
            if (frame < 0)
            {
                frame = 1;
                reverser = 1; // minimum sprite => reverse order (++)
            }   
        }
        else
        {
            // keeps building up time

        }
        gameObject.GetComponent<SpriteRenderer>().sprite = anim_walk[frame];
    }

    // Update is called once per frame
    void Update ()
    {
       // Debug.Log(anim_walk.Length);
        run_anim(anim_walk, 0.1f); // handle duration here, not in function?
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (floating == true)
            {
                gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity / floating_force;
            }
            else if (canclimb == false)
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
            if (canclimb == true)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(0, 200, 0);
                Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);

            }

        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            if ((floating == true) && (floatinglock == false))
            {
                floatinglock = true;
                gameObject.GetComponent<Rigidbody>().AddForce(0, -jumpheight + ((speed - 3) * 50), 0);
                // start droping
            }
             else if ((floating == true) && (floatinglock == true))
            {
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().AddForce(0, jumpheight + ((speed - 3) * 50) , 0); // need to add speed factor
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
        if (coll.contacts.Length > 0)
        {

            ContactPoint contact = coll.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                if (coll.gameObject.tag != "Parallax")
                {
                    floating = false;
                    floatinglock = false;
                    groundcoll = coll.gameObject.name;
                }
            }
            if (Vector3.Dot(contact.normal, Vector3.left) == 1)
            {
                if (coll.gameObject.tag != "Parallax")
                {
                    canclimb = true;
                    climbcoll = coll.gameObject.name;
                }
            }
        }

    }
    void OnCollisionExit(Collision coll)
    {
        if (string.Compare(coll.gameObject.name, climbcoll) == 0)
        {
            canclimb = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            climbcoll = "";
        }
        if (string.Compare(coll.gameObject.name, groundcoll) == 0)
        {
            speed = init_speed; // remove acceleration ? more like reduce it
            camspeed = init_camspeed; // remove acceleration ? more like reduce it
            floating = true;
            groundcoll = "";
        }

    }
}
