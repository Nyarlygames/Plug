using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public SaveManagerScript SaveManager = new SaveManagerScript();
    //public List<GameObject> CharsGO = new List<GameObject>();
    //public List<GameObject> GroupsGO = new List<GameObject>();
    public GameObject TribeGO;
    public SaveData sdata;


    void Start()
    {
        PlayerPrefs.SetString("savefile", "default");
        if (PlayerPrefs.GetString("savefile") != "")
        {
            sdata = SaveManager.LoadSave(PlayerPrefs.GetString("savefile"));
            TribeGO = SaveManager.LoadGO(sdata);
            if ((sdata != null))
            {
                // load savegame state
                Debug.Log("Loaded " + sdata.savefile);
            }
            else
                Debug.Log("Failed to load " + sdata.savefile);
        }
        else
        {
            //new game
            Debug.Log("New Game");
            CreateTribeGO("newgame");
            //CreateTribeGO("newgame");
        }
        /* CreatePlayer("manfather", 0, null, null);
         CreatePlayer("womanmother", 1, null, null);
         CreatePlayer("son", 2, PlayerScripts[0], PlayerScripts[1]);*/

        /*  CharacterSave ps = new CharacterSave();
          ps.id = 0;
          ps.name = "man";
          CreatePlayer(ps);
          ps = new CharacterSave();
          ps.id = 1;
          ps.name = "woman";
          CreatePlayer(ps);
          ps = new CharacterSave();
          ps.id = 2;
          ps.name = "son";
          CreatePlayer(ps);*/
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S)) // save the game
        {
            SaveManager.SaveGame(sdata, TribeGO);
           /* SaveManager.SavePlayer(PlayerGos[0].GetComponent<Character>());
            SaveManager.SavePlayer(PlayerGos[1].GetComponent<Character>());
            SaveManager.SavePlayer(PlayerGos[2].GetComponent<Character>());
            if (PlayerGos[0].GetComponent<Character>().playerSave != null)
            {
                Debug.Log("Saved time : " + PlayerGos[0].GetComponent<Character>().time);
            }
            if (PlayerGos[1].GetComponent<Character>().playerSave != null)
            {
                Debug.Log("Saved time : " + PlayerGos[0].GetComponent<Character>().time);
            }
            if (PlayerGos[2].GetComponent<Character>().playerSave != null)
            {
                Debug.Log("Saved time : " + PlayerGos[0].GetComponent<Character>().time);
            }*/
        }

        if (Input.GetKeyDown(KeyCode.L)) // load the game
        {
           /* if (PlayerGos.Count == 0)
            {
                CharacterSave playerSave = SaveManager.LoadPlayer("C:/Users/Nyarly/Desktop/Sapiens/Player0.dat");
                CreatePlayer(playerSave);
                if (playerSave != null)
                {
                    Debug.Log("Loaded time : " + PlayerGos[playerSave.id].GetComponent<Character>().playerSave.time);
                }
                PlayerPrefs.SetInt("PlayerNum", 1);
            }*/

        }

    }

    void CreateTribeGO(string hackkey)
    {
        switch (hackkey)
        {
            case "newgame" :
                TribeSave newtribe = new TribeSave();
                newtribe.tribename = "NewTribe";
                TribeGO = new GameObject(newtribe.tribename);
                TribeGO.AddComponent<TribeGO>();
                TribeGO tribecomp = TribeGO.GetComponent<TribeGO>();
                tribecomp.tribeCurrent = newtribe;
                tribecomp.profilename = "newgame";

                CharacterSave newman = new CharacterSave();
                newman.id = 0;
                newman.name = "New man";
                newman.time = 800;
                newman.charSprite = "Play/TribeChar/man";
                tribecomp.tribeCurrent.members.Add(newman);
                GameObject CharGO = new GameObject(newman.name);
                CharGO.AddComponent<CharacterGO>();
                CharGO.GetComponent<CharacterGO>().charCurrent = newman;
                CharGO.AddComponent<SpriteRenderer>();
                CharGO.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(newman.charSprite);
                tribecomp.CharsGO.Add(CharGO);

                CharacterSave newwoman = new CharacterSave();
                newwoman.id = 1;
                newwoman.name = "New woman";
                newwoman.time = 300;
                newwoman.charSprite = "Play/TribeChar/woman";
                tribecomp.tribeCurrent.members.Add(newwoman);
                CharGO = new GameObject(newwoman.name);
                CharGO.AddComponent<CharacterGO>();
                CharGO.GetComponent<CharacterGO>().charCurrent = newwoman;
                CharGO.AddComponent<SpriteRenderer>();
                CharGO.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(newwoman.charSprite);
                tribecomp.CharsGO.Add(CharGO);

                sdata = new SaveData();
                sdata.tribesave = tribecomp.tribeCurrent;
                sdata.savefile = "default";
                break;
            default:
                break;
        }
       /* PlayerGos.Add(new GameObject(playerSave.name));
        PlayerGos[playerSave.id].AddComponent<Character>();
        PlayerGos[playerSave.id].GetComponent<Character>().playerSave = playerSave;*/
    }
    void CreatePlayer(CharacterSave playerSave)
    {
      /*  PlayerGos.Add(new GameObject(playerSave.name));
        PlayerGos[playerSave.id].AddComponent<Character>();
        PlayerGos[playerSave.id].GetComponent<Character>().playerSave = playerSave;*/
    }
}
