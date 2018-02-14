using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeGO : MonoBehaviour {
    
    public TribeSave tribeCurrent;
    public AgeStruct curAge = new AgeStruct();
    public List<GameObject> CharsGO = new List<GameObject>();
    public string profilename = "";
    GameManager GM;
    RatioFactory RF = new RatioFactory();

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
            GM.EM.newDay();
            tribeCurrent.SetAge();
        }
        UpdateCharLists();
    }

    public void UpdateCharLists()
    {
        if (tribeCurrent.members.Count > 0)
        {
            foreach (CharacterSave cs in tribeCurrent.members)
            {
                if ((cs.age.years >= RF.adult_age) && (!tribeCurrent.adults.Contains(cs)))
                    tribeCurrent.adults.Add(cs);
                else if ((cs.age.years < RF.adult_age) && (!tribeCurrent.youngs.Contains(cs)))
                    tribeCurrent.youngs.Add(cs);
            }
        }
    }

    public void SetAge(float chartime)
    {
        curAge.days = (int)chartime / 24;
        curAge.years = (int)chartime / (24 * 365);
        curAge.hours = (int)chartime;
    }
}
