using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour {

    public List<GameObject> LinesText = new List<GameObject>();
    public List<string> Lines = new List<string>();

	// Use this for initialization
	void Start ()
    {
        // Set combat log objet
        LinesText.Add(GameObject.Find("UI_Log_Line1"));
        LinesText.Add(GameObject.Find("UI_Log_Line2"));
        LinesText.Add(GameObject.Find("UI_Log_Line3"));
        LinesText.Add(GameObject.Find("UI_Log_Line4"));
        LinesText.Add(GameObject.Find("UI_Log_Line5"));
        LinesText.Add(GameObject.Find("UI_Log_Line6"));
        LinesText.Add(GameObject.Find("UI_Log_Line7"));
        LinesText.Add(GameObject.Find("UI_Log_Line8"));
        LinesText.Add(GameObject.Find("UI_Log_Line9"));
        LinesText.Add(GameObject.Find("UI_Log_Line10"));
        LinesText.Add(GameObject.Find("UI_Log_Line11"));
        LinesText.Add(GameObject.Find("UI_Log_Line12"));
        LinesText.Add(GameObject.Find("UI_Log_Line13"));
        LinesText.Add(GameObject.Find("UI_Log_Line14"));
        LinesText.Add(GameObject.Find("UI_Log_Line15"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Add_To_Log(string key)
    {
        // add a string to the log (remove the first sent if over 15 lines
        if (Lines.Count == 15)
        {
            Lines.RemoveAt(14);
        }
        Lines.Insert(0, key);
        for (int i=0; i < Lines.Count; i++)
        {
            LinesText[i].GetComponent<Text>().text = Lines[i];
        }

    }
}
