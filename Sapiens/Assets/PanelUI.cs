using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour {

    Button Nomadism;
    Button Debug;
    Text NomadismT;
    GameManager GM;
    TribeGO tribe;

	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Nomadism = GameObject.Find("UI_Ingame_Nomadism_B").GetComponent<Button>();
        Nomadism.onClick.AddListener(NomadismClick);
        Nomadism = GameObject.Find("UI_Ingame_Nomadism_B").GetComponent<Button>();
        Nomadism.onClick.AddListener(NomadismClick);
        NomadismT = GameObject.Find("UI_Ingame_Nomadism_T").GetComponent<Text>();
    }

    void NomadismClick()
    {
        if (GM.TribeGO != null)
        {
            tribe = GM.tribe;
            if (tribe.tribeCurrent.nomadism == false)
            {
                tribe.tribeCurrent.nomadism = true;
                NomadismT.text = "Nomadism : On";
            }
            else
            {
                tribe.tribeCurrent.nomadism = false;
                NomadismT.text = "Nomadism : Off";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
}
