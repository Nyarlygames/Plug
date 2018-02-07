using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGO : MonoBehaviour {

    GameManager GM;
    Transform tribePos;
    Transform camPos;
	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        camPos = gameObject.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        tribePos = GM.TribeGO.GetComponent<TribeGO>().CharsGO[0].GetComponent<Transform>();
        Vector3 temp = tribePos.position;
        temp.z -= 5.0f;
        camPos.position = temp;
	}
}
