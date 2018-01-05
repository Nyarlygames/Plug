using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SoloController : MonoBehaviour {

    public List<GameObject> wordslist = new List<GameObject>();
    public float main_timer = 0.0f;
    private bool activeword = false;
    // Use this for initialization
    void Start ()
    {
        GameObject.Find("MovieSprite").GetComponent<VideoPlayer>().Play();
        GameObject.Find("MovieSprite").GetComponent<VideoPlayer>().isLooping = false;
        foreach (GameObject word in GameObject.FindGameObjectsWithTag("Word"))
        {
            wordslist.Add(word);
            word.GetComponent<WordController>().letters = GameObject.Find("Selector").GetComponent<SoloWords>().letters;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        main_timer = Time.time;
        GameObject ToRemove = null;
        foreach (GameObject wordgo in wordslist)
        {
            if ((main_timer >= wordgo.GetComponent<WordController>().time) && (main_timer - wordgo.GetComponent<WordController>().time <= wordgo.GetComponent<WordController>().duration) && wordgo.GetComponent<WordController>().ishown == false)
            {
                    wordgo.GetComponent<WordController>().ishown = true;
                GameObject.Find("Selector").GetComponent<SoloWords>().currentword = wordgo.GetComponent<WordController>().word;
                GameObject.Find("Selector").GetComponent<SoloWords>().validword = wordgo.GetComponent<WordController>().word;
                GameObject.Find("Selector").GetComponent<SoloWords>().wordcontrol = wordgo.GetComponent<WordController>();
                GameObject.Find("Selector").GetComponent<SoloWords>().word = "";
                for (int i = GameObject.Find("Selector").GetComponent<SoloWords>().sprite_word.Count - 1; i >= 0; i--)
                {
                    Destroy(GameObject.Find("Selector").GetComponent<SoloWords>().sprite_word[i]);
                }
                GameObject.Find("Selector").GetComponent<SoloWords>().sprite_word = new List<GameObject>();
            }
            else if ((main_timer >= wordgo.GetComponent<WordController>().time) && (main_timer - wordgo.GetComponent<WordController>().time > wordgo.GetComponent<WordController>().duration))
            {
                ToRemove = wordgo;
            }
        }

        if (ToRemove != null)
        {
            foreach (GameObject letter in ToRemove.GetComponent<WordController>().wordsprite)
            {
                Destroy(letter);
            }
            wordslist.Remove(ToRemove);
            Destroy(ToRemove);
        }

    }
}
 