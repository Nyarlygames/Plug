using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    public float speed = 3.0f;
    private float init_speed = 3.0f;
    public float max_speed = 8.0f;
    public float acceleration_speed = 3.0f;
    public float camspeed = 3.0f;
    private float init_camspeed = 3.0f;
    public float max_camspeed = 8.0f;
    public float acceleration_camspeed = 3.0f;
    public bool Paused = false;
    public bool initgo = false;
    private Sprite[] anim_walk;
    private float deltaTime = 0;
    private float starttime = 0;
    private int reverser = 1;
    private int frame = 0;
    private Rigidbody prb;
    private Transform pt;
    private int collnum = 0;


    public bool jump = false;
    public float jumpForce = 2800.0f;
    

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
        deltaTime += Time.deltaTime; 

        if (deltaTime >= starttime) 
        {
            frame += reverser;
            starttime += slicetime;
            if (frame >= anim.Length)
            {
                frame = anim.Length - 2;
                reverser = -1;
            }
            if (frame < 0)
            {
                frame = 1;
                reverser = 1;
            }   
        }
        else
        {

        }
        gameObject.GetComponent<SpriteRenderer>().sprite = anim_walk[frame];
    }

    void Update()
    {
        // grounded = Physics2D.Linecast(transform.position, Vector3.down, LayerMask.NameToLayer("ground"));
        if (Input.GetKeyDown(KeyCode.F) && (collnum > 0))
        {
            jump = true;
        }
        prb.MovePosition(new Vector3(pt.position.x + speed*Time.deltaTime, pt.position.y, pt.position.z));
        Camera.main.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    
    void FixedUpdate ()
    {
        if (jump)
        {
            prb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
           // grounded = false;
        }
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
