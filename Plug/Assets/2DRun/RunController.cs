using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunController : MonoBehaviour
{
    GameObject[] PauseScene;
    GameObject[] GameScene;
    private bool isloaded = false;

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
                GameScene = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject elem in GameScene)
                {
                    elem.GetComponent<PlayerController>().Paused = true;
                    Debug.Log(elem.name);
                }
                SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
                PauseScene = GameObject.FindGameObjectsWithTag("PauseMenu");
                isloaded = true;
            }
            else
            {

            }
            /* PauseMenu = GameObject.FindGameObjectsWithTag("PauseMenu");//  Find("PauseMenu")
             foreach (GameObject elem in PauseMenu)
             {
                 Debug.Log(elem.name);
             }*/
        }
    }
}
 