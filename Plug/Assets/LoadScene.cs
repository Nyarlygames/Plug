using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LoadScene : MonoBehaviour {

    public Button PlayButton;
    public string SceneToLoad; // given on editor

    void Start()
    {
        Button btn = PlayButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    
    void TaskOnClick()
    {
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }
}
