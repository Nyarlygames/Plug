using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PanelLoad : MonoBehaviour {

    GameManager GM;

	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Button temp = GameObject.Find("LoadPanel_Back").GetComponent<Button>();
        temp.onClick.AddListener(BackClick);
        temp = GameObject.Find("LoadPanel_Load").GetComponent<Button>();
        temp.onClick.AddListener(LoadClick);
        temp = GameObject.Find("LoadPanel_Delete").GetComponent<Button>();
        temp.onClick.AddListener(DeleteClick);

    }

    public void SetExisting()
    {
        Text existing = GameObject.Find("LoadPanel_Existing").GetComponent<Text>();
        existing.text = "Existing saves:";
        foreach (string dir in Directory.GetDirectories("./Save/"))
        {
            existing.text += "\n" + dir.Substring(7);
        }
    }
	
	void Update () {

    }

    void DeleteClick()
    {
        string filename = GameObject.Find("LoadPanel_SaveName").GetComponent<Text>().text;

        if (File.Exists("Save/" + filename + "/" + filename))
        {
            File.Delete("Save/" + filename + "/" + filename);
            Directory.Delete("Save/" + filename + "/");
            SetExisting();
        }
        else
        {
            GameObject.Find("LoadPanel_Status").GetComponent<Text>().text = "Unable to delete save named : " + filename + ".";
        }
    }

    void BackClick()
    {
        gameObject.SetActive(false);
    }

    void LoadClick()
    {
        string filename = GameObject.Find("LoadPanel_SaveName").GetComponent<Text>().text;
        PlayerPrefs.SetString("savefile", GameObject.Find("LoadPanel_SaveName").GetComponent<Text>().text);

        if (File.Exists("Save/" + filename + "/" + filename))
        {
            SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
            
        }
        else
        {
            GameObject.Find("LoadPanel_Status").GetComponent<Text>().text = "Unable to find save data named : " + filename + ".";
        }
    }
}
