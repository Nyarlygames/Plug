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
        GameObject Menus = GameObject.Find("UI_Panel");
        UISave = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_SavePanel"), Vector3.zero, Quaternion.identity);
        UISave.name = "UI_SavePanel";
        UISave.transform.SetParent(Menus.transform);
        UISave.SetActive(false);
        UILoad = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_LoadPanel"), Vector3.zero, Quaternion.identity);
        UILoad.name = "UI_LoadPanel";
        UILoad.transform.SetParent(Menus.transform);
    }
	
	void Update () {

    }

    void MenuClick()
    {
        SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
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
