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
    public bool run = false;
    public bool slowfall = false;
    public bool drop = false;
    public bool climb = false;
    public bool grounded = false;
    public float jumpForce = 2800.0f;
    public float bigjumpForce = 1100.0f;


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
        RaycastHit hit;
        if (Physics.Raycast(pt.position, Vector3.down, out hit, 0.5f, 1 << LayerMask.NameToLayer("ground")) || (collnum > 0))
        {
            grounded = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (grounded == true)
            {
                run = true;
                jump = false;
            }
            else
            {
                slowfall = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if ((run == true) && (grounded == true))
            {
                run = false;
                jump = true;
                grounded = false;
            }
            else if ((slowfall == true) && (grounded == false))
            {
                slowfall = false;
                drop = true;

            }
        }

    }
    
    void FixedUpdate ()
    {
        if (jump)
        {
            prb.AddForce(new Vector2(0f, jumpForce + (((speed - init_speed) / (max_speed - init_speed)) * 1100)));
            jump = false;
        }
        if (run)
        {
            if (speed < max_speed)
            {
                speed += acceleration_speed * Time.deltaTime;
            }
            else
                speed = max_speed;

            if (camspeed < max_camspeed)
            {
                camspeed += acceleration_camspeed * Time.deltaTime;
            }
            else
                camspeed = max_camspeed;
        }
        prb.MovePosition(new Vector3(pt.position.x + speed * Time.deltaTime, pt.position.y, pt.position.z));
        Camera.main.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    
    void OnCollisionEnter(Collision coll)
    {
        collnum++;
    }
    void OnCollisionExit(Collision coll)
    {
        collnum--;
    }
}
