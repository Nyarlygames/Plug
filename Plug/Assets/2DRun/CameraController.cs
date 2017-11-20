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
            Debug.Log("hit");
            PlayerController psettings = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (ChangeSize)
                Camera.main.orthographicSize = sizecam;

            if (ChangeCam) { 
                psettings.camspeed = change_cam_speed;
                psettings.max_camspeed = change_cam_maxspeed;
                psettings.acceleration_camspeed = change_cam_acceleration;
            }

            if (reverser)
                psettings.cam_reverser = -1;
            else
                psettings.cam_reverser = 1;

            if (gameovertrigger)
            {
                GameObject.Find("RunController").GetComponent<RunController>().isGameOverloaded = true;
                SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
                Time.timeScale = 0.0f;
            }
        }
    }
}
