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
        if ((GameObject.Find("Player").GetComponent<SpriteRenderer>().isVisible == false) && (GameObject.Find("Player").GetComponent<PlayerController>().initgo == true)) 
        {
            if (isGameOverloaded == false)
            {
                isGameOverloaded = true;
                SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
                Time.timeScale = 0.0f;
            }
        }
    }
}
 