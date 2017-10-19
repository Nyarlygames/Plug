using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordController : MonoBehaviour {

    public string word="";
    public float time = 0.0f;
    public int points = 0;
    public float pvhit = 0.0f;
    public int errormargin = 0;
    public float duration = 0.0f;
    public List<GameObject> wordsprite = new List<GameObject>();
    public bool ishown = false;
    public bool preloaded = false;
    public List<Sprite> letters = new List<Sprite>();

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (ishown == true)
        {
            if (preloaded == false)
            {
                int wordcount = 0;
                int currchar = 0;
                foreach (char c in word)
                {
                    if (Convert.ToInt32((char)c) >= 97)
                    {
                        currchar = c - 97; // lower case
                    }
                    else
                    {
                        currchar = c - 65; // upper case
                    }
                    GameObject letter = new GameObject(c.ToString());
                    letter.GetComponent<Transform>().position = new Vector3(wordcount * 1.0f, 0, 0); // "-1.0f" should be called ground scale, but i'm lazy
                    letter.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    letter.AddComponent<SpriteRenderer>();
                    letter.GetComponent<SpriteRenderer>().sprite = letters[currchar];
                    wordsprite.Add(letter);
                    wordcount++;
                    
                }
                preloaded = true;
            }
            else
            {
                //shown and first loaded
            }
        }
	}
}
