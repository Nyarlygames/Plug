using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunController : MonoBehaviour
{
    public bool isPauseloaded = false;
    public bool isGameOverloaded = false;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPauseloaded == false)
            {
                isPauseloaded = true;
                SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
                Time.timeScale = 0.0f;
            }
        }
    }
}
 