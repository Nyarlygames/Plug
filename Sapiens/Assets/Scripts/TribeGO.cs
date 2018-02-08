using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeGO : MonoBehaviour {
    
    public TribeSave tribeCurrent;
    public AgeStruct curAge = new AgeStruct();
    public List<GameObject> CharsGO = new List<GameObject>();
    public string profilename = "";
    public List<Texture2D> customchars = new List<Texture2D>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        tribeCurrent.time += Time.deltaTime;
        SetAge(tribeCurrent.time);
        if (curAge.days > tribeCurrent.age.days)
        {
            tribeCurrent.SetAge();
        }
    }

    public void SetAge(float chartime)
    {
        curAge.days = (int)chartime / 24;
        curAge.years = (int)chartime / (24 * 365);
        curAge.hours = (int)chartime;
    }
}
