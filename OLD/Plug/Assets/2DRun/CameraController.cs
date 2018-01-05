using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    public bool ChangeSize = false;
    public float sizecam = 5;

    public bool ChangeCam = false;
    public float change_cam_speed = 3.0f;
    public float change_cam_maxspeed = 8.0f;
    public float change_cam_acceleration = 3.0f;
    
    public bool reverser = false;

    public bool ChangeDir = false;
    public Vector3 direction = Vector3.zero;



    [HideInInspector] public bool activated = false;
    public bool IsSleeping = false;
    public GameObject activator;

    public bool gameovertrigger = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (IsSleeping)
            {
                if (activator != null)
                {
                    if (activator.GetComponent<CameraController>().activated == true)
                    {
                        IsSleeping = false;
                        PlayerController psettings = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                        if (ChangeSize)
                            Camera.main.orthographicSize = sizecam;

                        if (ChangeCam)
                        {
                            psettings.camspeed = change_cam_speed;
                            psettings.max_camspeed = change_cam_maxspeed;
                            psettings.acceleration_camspeed = change_cam_acceleration;
                        }

                        if (ChangeDir)
                        {
                            psettings.cam_direction = direction;
                        }

                        if (reverser)
                        {
                            if (psettings.inverted == 1)
                                psettings.GetComponent<Transform>().rotation = new Quaternion(psettings.GetComponent<Transform>().rotation.x, 180.0f, psettings.GetComponent<Transform>().rotation.z, psettings.GetComponent<Transform>().rotation.w);
                            else
                                psettings.GetComponent<Transform>().rotation = new Quaternion(psettings.GetComponent<Transform>().rotation.x, 0.0f, psettings.GetComponent<Transform>().rotation.z, psettings.GetComponent<Transform>().rotation.w);
                            psettings.inverted *= -1;
                            psettings.speed *= -1;
                            psettings.acceleration_speed *= -1;
                            psettings.max_speed *= -1;
                            psettings.init_speed *= -1;

                            psettings.camspeed *= -1;
                            psettings.acceleration_camspeed *= -1;
                            psettings.max_camspeed *= -1;
                            psettings.init_camspeed *= -1;
                        }
                        if (gameovertrigger)
                        {
                            GameObject.Find("RunController").GetComponent<RunController>().isGameOverloaded = true;
                            SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
                            Time.timeScale = 0.0f;
                        }
                    }
                }
            }
            else
            {
                PlayerController psettings = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                if (ChangeSize)
                    Camera.main.orthographicSize = sizecam;

                if (ChangeCam)
                {
                    psettings.camspeed = change_cam_speed;
                    psettings.max_camspeed = change_cam_maxspeed;
                    psettings.acceleration_camspeed = change_cam_acceleration;
                }

                if (ChangeDir)
                {
                    psettings.cam_direction = direction;
                }

                if (reverser)
                {
                    if (psettings.inverted == 1)
                        psettings.GetComponent<Transform>().rotation = new Quaternion(psettings.GetComponent<Transform>().rotation.x, 180.0f, psettings.GetComponent<Transform>().rotation.z, psettings.GetComponent<Transform>().rotation.w);
                    else
                        psettings.GetComponent<Transform>().rotation = new Quaternion(psettings.GetComponent<Transform>().rotation.x, 0.0f, psettings.GetComponent<Transform>().rotation.z, psettings.GetComponent<Transform>().rotation.w);
                    psettings.inverted *= -1;
                    psettings.speed *= -1;
                    psettings.acceleration_speed *= -1;
                    psettings.max_speed *= -1;
                    psettings.init_speed *= -1;

                    psettings.camspeed *= -1;
                    psettings.acceleration_camspeed *= -1;
                    psettings.max_camspeed *= -1;
                    psettings.init_camspeed *= -1;
                }
                if (gameovertrigger)
                {
                    GameObject.Find("RunController").GetComponent<RunController>().isGameOverloaded = true;
                    SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
                    Time.timeScale = 0.0f;
                }
                activated = true;
            }
        }
    }
}
