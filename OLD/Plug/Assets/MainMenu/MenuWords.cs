using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuWords : MonoBehaviour {
    
    public float frameSeconds = 0.5f;
    public SpriteRenderer spr;
    public List<Sprite> sprites;
    private int frame = 0;
    private int reverser = 1;
    private float deltaTime = 0;
    public string word = "";
    public List<KeyCode> letterstring = new List<KeyCode>();
    private List<Sprite> letters = new List<Sprite>();
    private List<String> wordstring = new List<String>();
    private List<GameObject> sprite_word = new List<GameObject>();
    public float startposx = 0;

    void Start()
    {
        startposx = gameObject.GetComponent<Transform>().position.x;
        spr = GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sprites = new List<Sprite>(Resources.LoadAll<Sprite>("img/cursor/"));
        letters.Add(Resources.Load<Sprite>("img/letters/A"));
        letterstring.Add(KeyCode.A);
        letters.Add(Resources.Load<Sprite>("img/letters/B"));
        letterstring.Add(KeyCode.B);
        letters.Add(Resources.Load<Sprite>("img/letters/C"));
        letterstring.Add(KeyCode.C);
        letters.Add(Resources.Load<Sprite>("img/letters/D"));
        letterstring.Add(KeyCode.D);
        letters.Add(Resources.Load<Sprite>("img/letters/E"));
        letterstring.Add(KeyCode.E);
        letters.Add(Resources.Load<Sprite>("img/letters/F"));
        letterstring.Add(KeyCode.F);
        letters.Add(Resources.Load<Sprite>("img/letters/G"));
        letterstring.Add(KeyCode.G);
        letters.Add(Resources.Load<Sprite>("img/letters/H"));
        letterstring.Add(KeyCode.H);
        letters.Add(Resources.Load<Sprite>("img/letters/I"));
        letterstring.Add(KeyCode.I);
        letters.Add(Resources.Load<Sprite>("img/letters/J"));
        letterstring.Add(KeyCode.J);
        letters.Add(Resources.Load<Sprite>("img/letters/K"));
        letterstring.Add(KeyCode.K);
        letters.Add(Resources.Load<Sprite>("img/letters/L"));
        letterstring.Add(KeyCode.L);
        letters.Add(Resources.Load<Sprite>("img/letters/M"));
        letterstring.Add(KeyCode.M);
        letters.Add(Resources.Load<Sprite>("img/letters/N"));
        letterstring.Add(KeyCode.N);
        letters.Add(Resources.Load<Sprite>("img/letters/O"));
        letterstring.Add(KeyCode.O);
        letters.Add(Resources.Load<Sprite>("img/letters/P"));
        letterstring.Add(KeyCode.P);
        letters.Add(Resources.Load<Sprite>("img/letters/Q"));
        letterstring.Add(KeyCode.Q);
        letters.Add(Resources.Load<Sprite>("img/letters/R"));
        letterstring.Add(KeyCode.R);
        letters.Add(Resources.Load<Sprite>("img/letters/S"));
        letterstring.Add(KeyCode.S);
        letters.Add(Resources.Load<Sprite>("img/letters/T"));
        letterstring.Add(KeyCode.T);
        letters.Add(Resources.Load<Sprite>("img/letters/U"));
        letterstring.Add(KeyCode.U);
        letters.Add(Resources.Load<Sprite>("img/letters/V"));
        letterstring.Add(KeyCode.V);
        letters.Add(Resources.Load<Sprite>("img/letters/W"));
        letterstring.Add(KeyCode.W);
        letters.Add(Resources.Load<Sprite>("img/letters/X"));
        letterstring.Add(KeyCode.X);
        letters.Add(Resources.Load<Sprite>("img/letters/Y"));
        letterstring.Add(KeyCode.Y);
        letters.Add(Resources.Load<Sprite>("img/letters/Z"));
        letterstring.Add(KeyCode.Z);
        wordstring.Add("PLAY");
        wordstring.Add("OPTIONS");
        wordstring.Add("EXIT");
    }

    public void addWord(string action)
    {
        wordstring.Add(action);
    }

    void fade_in_out_in()
    {
        //Keep track of the time that has passed
        deltaTime += Time.deltaTime; // deleta varies between 0 and frameseconds.
        float slice = 0.05f;

        if (deltaTime >= frameSeconds) // hit frameseconds => goto next frame
        {
            frame += reverser;
            frameSeconds += slice;
            if (frame >= sprites.Count)
            {
                frame = sprites.Count - 2; // maximum sprite => reverse order (--)
                reverser = -1;
            }
            if (frame < 0)
            {
                frame = 1;
                reverser = 1; // minimum sprite => reverse order (++)
            }
        }
        else
        {
           // keeps building up time

        }
        spr.sprite = sprites[frame];
    }

    void typing()
    {
        foreach(KeyCode input in letterstring)
        {
            if (Input.GetKeyDown(input))
            {
                GameObject letter = new GameObject("");
                letter.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                letter.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
                letter.AddComponent<SpriteRenderer>();
                SpriteRenderer newletter = letter.GetComponent<SpriteRenderer>();
                word += input.ToString();
                letter.name = input.ToString();
                newletter.sprite = letters[Convert.ToInt32((char)input) - 97];
                
                sprite_word.Add(letter);
                gameObject.GetComponent<Transform>().position = new Vector3(startposx + sprite_word.Count * 0.5f, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);

            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (word.Length > 0)
            {
                word = word.Substring(0, word.Length - 1);
                Destroy(sprite_word[sprite_word.Count - 1]);
                sprite_word.RemoveAt(sprite_word.Count - 1);
                gameObject.GetComponent<Transform>().position = new Vector3(startposx + sprite_word.Count * 0.5f, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);

            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            word += " ";
            GameObject letter = new GameObject("Space");
            sprite_word.Add(letter);
            gameObject.GetComponent<Transform>().position = new Vector3(startposx + sprite_word.Count * 0.5f, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (word.Length > 0)
            {
                for (int i = sprite_word.Count - 1; i >= 0; i--)
                {
                    Destroy(sprite_word[i]);
                }
                sprite_word = new List<GameObject>();
                foreach (String action in wordstring)
                {
                    if (string.Compare(word, action) == 0)
                    {
                        // possible action
                        DetectedWord(action);
                    }
                    else
                    {
                        // unrecognized word
                    }
                }
                word = "";
                gameObject.GetComponent<Transform>().position = new Vector3(startposx, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            }
        }
    }

    void DetectedWord(string w)
    {
        switch(w)
        {
            case "PLAY":
                SceneManager.LoadScene("PlayMode", LoadSceneMode.Single);
                // GameObject.Find("PlayButton").GetComponent<Image>().enabled = true;
                //GameObject.Find("OptionsButton").GetComponent<Image>().enabled = false;
                break;
            case "OPTIONS":
                SceneManager.LoadScene("Options", LoadSceneMode.Single);
               // GameObject.Find("PlayButton").GetComponent<Image>().enabled = false;
                //GameObject.Find("OptionsButton").GetComponent<Image>().enabled = true;
                break;
            case "EXIT":
                Application.Quit();
                break;
            default:
                break;
        }
        
    }

    void Update() {
        fade_in_out_in();
        typing();
    }
    
}
