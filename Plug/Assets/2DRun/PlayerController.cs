using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    public float speed = 3.0f;
    public float max_speed = 8.0f;
    public float acceleration_speed = 3.0f;
    public float camspeed = 3.0f;
    public float max_camspeed = 8.0f;
    public float acceleration_camspeed = 3.0f;
    private float init_speed = 3.0f;
    private float init_camspeed = 3.0f;
    public bool isfalling = false;
    public bool isrunning = false;
    public bool isfloating = false;
    public bool Paused = false;
    public bool isloaded = false;
    public bool initgo = false;
    private Sprite[] anim_walk;
    private float deltaTime = 0;
    private float starttime = 0;
    private int reverser = 1;
    private int frame = 0;
    private Rigidbody prb;
    private Transform pt;
    private int collnum = 0;

    // Use this for initialization
    void Start()
    {
        init_speed = speed;
        init_camspeed = speed;
        anim_walk = Resources.LoadAll<Sprite>("img/Players/Player_Run");
        prb = gameObject.GetComponent<Rigidbody>();
        pt = gameObject.GetComponent<Transform>();
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
    void FixedUpdate ()
    {
       // Debug.Log(anim_walk.Length);
        run_anim(anim_walk, 0.1f); // handle duration here, not in function?

        if (Input.GetKeyDown(KeyCode.F))
        {
            if ((isfalling == false) && (isrunning == false) && (collnum > 0))
            {
                isrunning = true;
                if (speed < max_speed)
                {
                    speed += acceleration_speed * Time.deltaTime;
                    Debug.Log("run++");
                }
                else
                {
                    speed = max_speed;
                    Debug.Log("runmax");
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {

            if ((isfalling == false) && (isrunning == true) && (collnum > 0))
            {
                isfalling = true;
                isrunning = false;
                // gameObject.GetComponent<Rigidbody>().velocity = new Vector3(speed, 35, 0); // 35 low - 70 max
                prb.AddForce(new Vector3(0, 3500, 0));                    //gameObject.GetComponent<NavMeshAgent>().enabled = true;
                speed = init_speed;//----------------------------------------------------------------------------------------------------- decelerate and not reset --------------------------------------------------------------- //
                Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
                Debug.Log("jump");
            }

            /*  if (camspeed < max_camspeed)
              {
                  camspeed += acceleration_camspeed * Time.deltaTime;
              }
              else
              {
                  camspeed = max_camspeed;
              }*/
        }
        else if (Input.GetKey(KeyCode.F)) {
            if ((isrunning == true) && (collnum > 0))
            {
                if (speed < max_speed)
                {
                    speed += acceleration_speed * Time.deltaTime;
                    Debug.Log("run++");
                }
                else
                {
                    speed = max_speed;
                    Debug.Log("runmax");
                }
            }
        }
        else
        {
            if ((isfalling == false) && (collnum == 0))
            {
                // no ground, fall.
                prb.AddForce(new Vector3(0, -100, 0));
                isfalling = true;
                isrunning = false;
                Debug.Log("falling");
            }
            else if ((isfalling == true) && (collnum > 0))
            {
                // fell to ground
                isfalling = false;
                Debug.Log("grounded");

            }
            else if ((isfalling == true) && (collnum == 0))
            {
                // falling normally 
                prb.AddForce(new Vector3(0, -100, 0));
                Debug.Log("downing");
            }
            else
            {

            }
        }
        /*
         // HERE DO : MoveTO Or AddVelocity
         // gameObject.GetComponent<Transform>().Translate(speed * Time.deltaTime, 0f, 0f);*/
        // else
        float step = speed * Time.deltaTime;
        Vector3 target = new Vector3(pt.position.x + 0.5f, pt.position.y, pt.position.z);
        pt.position = Vector3.MoveTowards(pt.position, target, step);
        prb.velocity = new Vector3(speed, 0, 0);              
        Camera.main.GetComponent<Transform>().Translate(transform.right * camspeed * Time.deltaTime);


    }
    void OnCollisionEnter(Collision coll)
    {
        collnum++;
     /*   if (coll.contacts.Length > 0)
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
        }*/

    }
    void OnCollisionExit(Collision coll)
    {
        collnum--;
        /* if (string.Compare(coll.gameObject.name, climbcoll) == 0)
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
         }*/

    }
}
