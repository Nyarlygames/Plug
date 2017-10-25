using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunController : MonoBehaviour
{
    GameObject[] PauseScene;
    GameObject[] GameScene;
    public bool isloaded = false;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isloaded == false)
            {
                isloaded = true;
                SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
                Time.timeScale = 0.0f;
            }
        }
    }
}
 