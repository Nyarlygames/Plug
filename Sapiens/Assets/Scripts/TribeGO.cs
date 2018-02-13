using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeGO : MonoBehaviour {
    
    public TribeSave tribeCurrent;
    public AgeStruct curAge = new AgeStruct();
    public List<GameObject> CharsGO = new List<GameObject>();
    public string profilename = "";
    GameManager GM;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        tribeCurrent.time += Time.deltaTime;
        SetAge(tribeCurrent.time);
        if (curAge.days > tribeCurrent.age.days)
        {
            tribeCurrent.SetAge();
            GM.EM.newDay();
        }
    }

    public void SetAge(float chartime)
    {
        curAge.days = (int)chartime / 24;
        curAge.years = (int)chartime / (24 * 365);
        curAge.hours = (int)chartime;
    }
}
