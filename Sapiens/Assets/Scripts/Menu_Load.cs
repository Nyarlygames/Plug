using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu_Load : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int last = 1;
        foreach (string dir in Directory.GetDirectories("./Save/"))
        {
            if (last <11)
            {
                Button slot = GameObject.Find("Load_Slot" + last).GetComponent<Button>();
                switch (last)
                {
                    case 1:
                        slot.onClick.AddListener(Slot1Click);
                        break;
                    case 2:
                        slot.onClick.AddListener(Slot2Click);
                        break;
                    case 3:
                        slot.onClick.AddListener(Slot3Click);
                        break;
                    case 4:
                        slot.onClick.AddListener(Slot4Click);
                        break;
                    case 5:
                        slot.onClick.AddListener(Slot5Click);
                        break;
                    case 6:
                        slot.onClick.AddListener(Slot6Click);
                        break;
                    case 7:
                        slot.onClick.AddListener(Slot7Click);
                        break;
                    case 8:
                        slot.onClick.AddListener(Slot8Click);
                        break;
                    case 9:
                        slot.onClick.AddListener(Slot9Click);
                        break;
                    case 10:
                        slot.onClick.AddListener(Slot10Click);
                        break;
                    default:
                        break;
                }
                GameObject.Find("Load_Slot" + last + "Text").GetComponent<Text>().text = dir.Substring(7);
                last++;
            }
        }
        while (last < 11)
        {
            GameObject.Find("Load_Slot" + last).SetActive(false);
            last++;
        }
        GameObject.Find("Load_Back").GetComponent<Button>().onClick.AddListener(BackClick);
    }
    void BackClick()
    {
        SceneManager.LoadScene("PlayMenu", LoadSceneMode.Single);
    }

    void Slot1Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot1Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Slot2Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot2Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    void Slot3Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot3Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Slot4Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot4Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Slot5Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot5Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Slot6Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot6Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Slot7Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot7Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Slot8Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot8Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Slot9Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot9Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Slot10Click()
    {
        PlayerPrefs.SetString("savefile", GameObject.Find("Load_Slot10Text").GetComponent<Text>().text);
        SceneManager.LoadScene("Sapiens_Load", LoadSceneMode.Single);
    }
    
    void Update () {
		
	}
}
