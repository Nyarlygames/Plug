using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGO : MonoBehaviour {
    
    public CharacterSave charCurrent;
    public AgeStruct curAge = new AgeStruct();
    
    void Start () {

	}

    // Update is called once per frame
    void FixedUpdate()
    {
        charCurrent.time += Time.deltaTime;
        SetAge(charCurrent.time);
        if (curAge.days > charCurrent.age.days)
        {
            charCurrent.SetAge();
        }
    }

    public void SetAge(float chartime)
    {
        curAge.days = (int)chartime / 24;
        curAge.years = (int)chartime / (24 * 365);
        curAge.hours = (int)chartime;
    }
}
