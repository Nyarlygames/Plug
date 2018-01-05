using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SoloWords : MonoBehaviour {
    
    public float frameSeconds = 0.5f;
    public SpriteRenderer spr;
    public List<Sprite> sprites;
    private int frame = 0;
    private int reverser = 1;
    private float deltaTime = 0;
    public string word = "";
    public List<KeyCode> letterstring = new List<KeyCode>();
    public List<Sprite> letters = new List<Sprite>();
    private List<String> wordstring = new List<String>();
    public List<GameObject> sprite_word = new List<GameObject>();
    public float startposx = 0;
    public string currentword = "";
    public string validword = "";
    public Vector3 letternumb;
    public WordController wordcontrol;
    public List<GameObject> wrong_answers = new List<GameObject>();

    void Start()
    {
     letternumb = new Vector3(-7, 4, 1);
    startposx = gameObject.GetComponent<Transform>().position.x;
        spr = GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
                if (currentword != "")
                {
                    if (String.Compare(currentword[0].ToString(),input.ToString()) == 0)
                    {
                        GameObject letter = new GameObject("");
                        letter.GetComponent<Transform>().position = new Vector3(startposx + sprite_word.Count * 1.0f, GameObject.Find("Ground").GetComponent<Transform>().position.y - 1.0f, GameObject.Find("Ground").GetComponent<Transform>().position.z); // "-1.0f" should be called ground scale, but lazy
                        letter.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        letter.AddComponent<SpriteRenderer>();
                        SpriteRenderer newletter = letter.GetComponent<SpriteRenderer>();
                        word += input.ToString();
                        letter.name = input.ToString();
                        newletter.sprite = letters[Convert.ToInt32((char)input) - 97];
                        letter.GetComponent<SpriteRenderer>().color = Color.green;

                        sprite_word.Add(letter);
                        currentword = currentword.Substring(1, currentword.Length - 1);
                    }
                    else
                    {
                        GameObject letter = new GameObject("");
                        letter.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        letter.AddComponent<SpriteRenderer>();
                        SpriteRenderer newletter = letter.GetComponent<SpriteRenderer>();
                        letter.name = input.ToString();
                        newletter.sprite = letters[Convert.ToInt32((char)input) - 97];
                        letter.GetComponent<SpriteRenderer>().color = Color.black;
                        wrong_answers.Add(letter);
                        Vector3 Letterpos = new Vector3((wrong_answers.IndexOf(letter) % 7), (wrong_answers.IndexOf(letter) / 7), 1);
                        letter.GetComponent<Transform>().position = letternumb;
                        if (letternumb.x == 7)
                        {
                            letternumb.x = -7;
                            letternumb.y--;
                        }
                        else
                        {
                            letternumb.x++;
                        }
                        if (letternumb.y < -2)
                        {
                            // out of scope => ded
                        }


                    }

                    
                }
               // gameObject.GetComponent<Transform>().position = new Vector3(startposx + sprite_word.Count * 1.0f, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            }
        }
       /* if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (word.Length > 0)
            {
                word = word.Substring(0, word.Length - 1);
                Destroy(sprite_word[sprite_word.Count - 1]);
                sprite_word.RemoveAt(sprite_word.Count - 1);
               // gameObject.GetComponent<Transform>().position = new Vector3(startposx + sprite_word.Count * 1.0f, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            }
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            word += " ";
            GameObject letter = new GameObject("Space");
            sprite_word.Add(letter);
           // gameObject.GetComponent<Transform>().position = new Vector3(startposx + sprite_word.Count * 1.0f, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (word.Length > 0)
            {

                if (string.Compare(word, validword) == 0)
                {
                    for (int i = sprite_word.Count - 1; i >= 0; i--)
                    {
                        Destroy(sprite_word[i]);
                    }
                    if ((wordcontrol != null) && (wordcontrol.wordsprite.Count > 0))
                    {
                        foreach (GameObject letter in wordcontrol.wordsprite)
                        {
                            Destroy(letter);
                        }
                    }
                    sprite_word = new List<GameObject>();
                    word = "";
                }
            }
        }
    }

    void DetectedWord(string w)
    {
        /*switch(w)
        {
            case "PLAY":
                GameObject.Find("PlayButton").GetComponent<Image>().enabled = true;
                GameObject.Find("OptionsButton").GetComponent<Image>().enabled = false;
                break;
            case "OPTIONS":
                GameObject.Find("PlayButton").GetComponent<Image>().enabled = false;
                GameObject.Find("OptionsButton").GetComponent<Image>().enabled = true;
                break;
            case "EXIT":
                Application.Quit();
                break;
            default:
                break;
        }*/
        
    }

    void Update() {
        fade_in_out_in();
        typing();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
           // Debug.DrawRay(contact.point, contact.normal, Color.white);
           // Debug.Log(collision.collider.name);
        }
    }
}
