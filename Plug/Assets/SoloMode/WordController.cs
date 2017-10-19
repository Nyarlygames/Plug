using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordController : MonoBehaviour {

    public string word="";
    public float time = 0;
    public int points = 0;
    public float pvhit = 0;
    public int errormargin = 0;
    public List<GameObject> wordsprite = new List<GameObject>();

	// Use this for initialization
	void Start () {
		/*foreach(char letter in word)
        {
            GameObject newletter = new GameObject();
            wordsprite.Add(newletter);
        }*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
