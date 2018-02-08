using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSave : MonoBehaviour {

    GameManager GM;
	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Button temp = GameObject.Find("SavePanel_Back").GetComponent<Button>();
        temp.onClick.AddListener(BackClick);
        temp = GameObject.Find("SavePanel_Save").GetComponent<Button>();
        temp.onClick.AddListener(SaveClick);
        GameObject.Find("SavePanel_Text").GetComponent<InputField>().text = GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.tribename;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void BackClick()
    {
        gameObject.SetActive(false);
    }

    void SaveClick()
    {
        GM.sdata.savefile = GameObject.Find("SavePanel_SaveName").GetComponent<Text>().text;
        //GM.TribeGO.GetComponent<TribeGO>().name = GM.sdata.savefile;
        GM.SaveManager.SaveGame(GM.sdata, GM.TribeGO, GM.map);
        //GameObject.Find("UI_SaveName").GetComponent<Text>().text = "Save name : " + GM.sdata.savefile;
        GameObject.Find("SavePanel_Status").GetComponent<Text>().text = "Saved : " + GM.sdata.savefile;

    }
}
