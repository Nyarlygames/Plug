using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public SaveManagerScript SaveManager = new SaveManagerScript();
    public List<GameObject> PlayerGos = new List<GameObject>();
    

    void Start()
    {
       /* CreatePlayer("manfather", 0, null, null);
        CreatePlayer("womanmother", 1, null, null);
        CreatePlayer("son", 2, PlayerScripts[0], PlayerScripts[1]);*/
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S)) // save the game
        {
            SaveManager.SavePlayer(PlayerGos[0].GetComponent<PlayerGo>(), PlayerGos[0].GetComponent<PlayerGo>().playerSave);
            if (PlayerGos[0].GetComponent<PlayerGo>().playerSave != null)
            {
                Debug.Log("Saved time : " + PlayerGos[0].GetComponent<PlayerGo>().time);
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) // load the game
        {
            if (PlayerGos.Count ==00)
            {
                PlayerScript playerSave = SaveManager.LoadPlayer("C:/Users/Nyarly/Desktop/Sapiens/Player0.dat");
                CreatePlayer(playerSave);
                if (playerSave != null)
                {
                    Debug.Log("Loaded time : " + PlayerGos[playerSave.id].GetComponent<PlayerGo>().playerSave.time);
                }
                PlayerPrefs.SetInt("PlayerNum", 1);
            }
            else if (PlayerGos.Count ==1)
            {

            }

        }

    }

    void CreatePlayer(PlayerScript playerSave)
    {
        PlayerGos.Add(new GameObject(playerSave.name));
        PlayerGos[playerSave.id].AddComponent<PlayerGo>();
        PlayerGos[playerSave.id].GetComponent<PlayerGo>().playerSave = playerSave;
    }
}
