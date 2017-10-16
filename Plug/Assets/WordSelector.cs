using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSelector : MonoBehaviour {

    public bool loop;
    public float frameSeconds = 0.5f;
    private SpriteRenderer spr;
    private List<Sprite> sprites;
    private int frame = 0;
    private int reverser = 1;
    private float deltaTime = 0;
    private float lastdelta = 0;
    public string word = "";
    private List<Sprite> letters = new List<Sprite>();
    private List<GameObject> sprite_word = new List<GameObject>();

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        sprites = new List<Sprite>(Resources.LoadAll<Sprite>("img/cursor/"));
        letters.Add(Resources.Load<Sprite>("img/letters/P"));
        letters.Add(Resources.Load<Sprite>("img/letters/L"));
        letters.Add(Resources.Load<Sprite>("img/letters/A"));
        letters.Add(Resources.Load<Sprite>("img/letters/Y"));
        letters.Add(Resources.Load<Sprite>("img/letters/Z"));
    }

    void Update() {
        //Keep track of the time that has passed
        deltaTime += Time.deltaTime; //- lastdelta;/// HHHEERRREEEEE time keeps growing. delta only needs to add
        lastdelta = Time.deltaTime;
        float slice = 0.05f;
        //Debug.Log("deltaTime " + +deltaTime + " lastdelta " + frameSeconds);
        if (deltaTime >= frameSeconds) {
            frame += reverser;
             frameSeconds += slice;
             if (frame >= sprites.Count)
             {
                 frame = sprites.Count - 2;
                 reverser = -1;
             }
             if (frame < 0)
             {
             frame  = 1;
             reverser = 1;
             }
        }
        else
        {
           // Debug.Log("bullshit : " + (deltaTime > frameSeconds) + " deltaTime " + +deltaTime + " lastdelta " + frameSeconds);

        }
        spr.sprite = sprites[frame];
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("hit p");
                word += "P";
                GameObject letter = new GameObject("P");
                letter.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                letter.AddComponent<SpriteRenderer>();
                SpriteRenderer newletter = letter.GetComponent<SpriteRenderer>();
               newletter.sprite = letters[1];
                sprite_word.Add(letter);
                gameObject.GetComponent<Transform>().position = new Vector3(sprite_word.Count,0,0);
                
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                word += "L";
                GameObject letter = new GameObject("L");
                letter.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                letter.AddComponent<SpriteRenderer>();
                SpriteRenderer newletter = letter.GetComponent<SpriteRenderer>();
                newletter.sprite = letters[2];
                sprite_word.Add(letter);
                gameObject.GetComponent<Transform>().position = new Vector3(sprite_word.Count, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                word += "A";
                GameObject letter = new GameObject("A");
                letter.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                letter.AddComponent<SpriteRenderer>();
                SpriteRenderer newletter = letter.GetComponent<SpriteRenderer>();
                newletter.sprite = letters[3];
                sprite_word.Add(letter);
                gameObject.GetComponent<Transform>().position = new Vector3(sprite_word.Count, 0, 0);

            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                word += "Y";
                GameObject letter = new GameObject("Y");
                letter.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                letter.AddComponent<SpriteRenderer>();
                SpriteRenderer newletter = letter.GetComponent<SpriteRenderer>();
                newletter.sprite = letters[4];
                sprite_word.Add(letter);
                gameObject.GetComponent<Transform>().position = new Vector3(sprite_word.Count, 0, 0);

            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                word += "Z";
                GameObject letter = new GameObject("Z");
                letter.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                letter.AddComponent<SpriteRenderer>();
                SpriteRenderer newletter = letter.GetComponent<SpriteRenderer>();
                newletter.sprite = letters[0];
                sprite_word.Add(letter);
                gameObject.GetComponent<Transform>().position = new Vector3(sprite_word.Count, 0, 0);

            }
            //           GameObject.Find("PlayButton").GetComponent<Image>().enabled = true;
            if (string.Compare(word,"PLAY") == 0)
            {
                GameObject.Find("PlayButton").GetComponent<Image>().enabled = true;
            }
        }

    }
}
