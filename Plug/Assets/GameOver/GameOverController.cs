using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    // NOTE FOR LATER :  I DEACTIVATED AUDIO LISTENER FOR THE RUNCONTROLLER ONE, SWITCH FOR PAUSE AND GAMEOVER MUSICS 
    public Button RetryButton;
    // Use this for initialization
    void Start () {

        GameObject.Find("RetryButton").GetComponent<Button>().onClick.AddListener(RetryClick);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameOver"));
    }
	
	// Update is called once per frame
	void Update () {

    }
    void RetryClick()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("2DRun"));
        SceneManager.LoadSceneAsync("2DRun", LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("2DRun"));
        Time.timeScale = 1.0f;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("GameOver"));
    }
}
