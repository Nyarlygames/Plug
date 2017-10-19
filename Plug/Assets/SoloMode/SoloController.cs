using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SoloController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("MovieSprite").GetComponent<VideoPlayer>().Play();
        GameObject.Find("MovieSprite").GetComponent<VideoPlayer>().isLooping = false;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(GameObject.Find("Movie").GetComponent<VideoPlayer>().isPlaying);
		
	}
}
