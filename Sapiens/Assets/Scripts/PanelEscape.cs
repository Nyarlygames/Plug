using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelEscape : MonoBehaviour
{
    public GameObject UISave;
    public GameObject UILoad;
    GameManager GM;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Button temp = GameObject.Find("Escape_Menu").GetComponent<Button>();
        temp.onClick.AddListener(MenuClick);
        temp = GameObject.Find("Escape_Save").GetComponent<Button>();
        temp.onClick.AddListener(SaveClick);
        temp = GameObject.Find("Escape_Load").GetComponent<Button>();
        temp.onClick.AddListener(LoadClick);
        temp = GameObject.Find("Escape_Back").GetComponent<Button>();
        temp.onClick.AddListener(BackClick);
        UISave = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_SavePanel"), Vector3.zero, Quaternion.identity);
        UILoad = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_LoadPanel"), Vector3.zero, Quaternion.identity);
    }
	
	void Update () {

    }

    void MenuClick()
    {
        SceneManager.LoadScene("PlayMenu", LoadSceneMode.Single);
    }
    void SaveClick()
    {
        UISave.SetActive(true);
    }
    void BackClick()
    {
        Time.timeScale = GM.scaleBeforeEscape;
        gameObject.SetActive(false);
    }
    void LoadClick()
    {
        UILoad.SetActive(true);
        UILoad.GetComponent<PanelLoad>().SetExisting();
    }
}
