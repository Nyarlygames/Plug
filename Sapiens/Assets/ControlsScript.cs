using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScript : MonoBehaviour
{
    Transform pt;
    Transform ct;
    CameraScript cs;
    PlayerScript ps;
    Rigidbody prb;
    Vector3 targetHit = Vector3.zero;
    string seed = "";
    public Transform Charpic_selected;
    public int selectedChar = 0;
    public List<Transform> Charpics = new List<Transform>();

    void Start ()
    {
        seed = PlayerPrefs.GetString("SeedName");
        /*   prb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
           pt = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
           ct = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
           cs = Camera.main.GetComponent<CameraScript>();
           ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();*/
        Charpic_selected = GameObject.Find("CharacterPic_Select").GetComponent<Transform>();
        Charpics.Add(GameObject.Find("CharacterPic0").GetComponent<Transform>());
        Charpics.Add(GameObject.Find("CharacterPic1").GetComponent<Transform>());
        Charpics.Add(GameObject.Find("CharacterPic2").GetComponent<Transform>());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C) && (GameObject.Find("CharacterPanel") == null))
        {
            SceneManager.LoadScene("CharacterPanel", LoadSceneMode.Additive);
        }

        // create group with mouse ?
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Charpic_selected.position = Charpics[0].position;
            selectedChar = 0;
            // select P1
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Charpic_selected.position = Charpics[1].position;
            selectedChar = 1;
            // select P2
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Charpic_selected.position = Charpics[2].position;
            selectedChar = 2;
            // select P3
        }
    }
    
}
