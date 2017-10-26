using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

    // NOTE FOR LATER :  I DEACTIVATED AUDIO LISTENER FOR THE RUNCONTROLLER ONE, SWITCH FOR PAUSE AND GAMEOVER MUSICS 
    public Button BackButton;
    // Use this for initialization
    void Start () {
        GameObject.Find("Pause Camera").GetComponent<Transform>().position = GameObject.Find("Main Camera").GetComponent<Transform>().position;
        GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(BackClick);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Pause"));
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void BackClick()
    {
        GameObject.Find("RunController").GetComponent<RunController>().isPauseloaded = false;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("2DRun"));
        Time.timeScale = 1.0f;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Pause"));
    }
}
