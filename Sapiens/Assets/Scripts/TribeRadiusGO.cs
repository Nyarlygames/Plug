using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TribeRadiusGO : MonoBehaviour {
    GameObject[] triggers;

	// Use this for initialization
	void Start () {
        triggers = GameObject.FindGameObjectsWithTag("trigger");
	}
	
	// Update is called once per frame
	void Update ()
    {


    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectGO objgo = collision.gameObject.GetComponent<ObjectGO>();
        if ((objgo != null) && (objgo.objectCur.visitState != 1))
        {
            if (objgo.tag == "Loader")
            {
                LoadChunk Load = GameObject.Find("Load_Chunk").GetComponent<LoadChunk>();
                GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
                Debug.Log(objgo.objectCur.modifiers["loadChunk"] + " / started");
                switch (objgo.objectCur.modifiers["loadChunk"])
                {
                    /*case "0":
                        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Load_Chunk", LoadSceneMode.Additive);
                        break;*/
                    case "1":
                        Load.LoadChunkByName("Assets/Resources/Map/TestChunk2.tmx", 50, 0);
                        break;
                    case "2":
                        Load.LoadChunkByName("Assets/Resources/Map/TestChunk3.tmx", 50, 100);
                        break;
                    case "3":
                        Load.LoadChunkByName("Assets/Resources/Map/TestChunk4.tmx", 0, 100);
                        break;
                    /*case "4":
                        Load.LoadChunkByName("Assets/Resources/Map/TestChunk2.tmx", -50, 100);
                        break;
                    case "5":
                        Load.LoadChunkByName("Assets/Resources/Map/TestChunk2.tmx", 50, 0);
                        break;
                    case "6":
                        Load.LoadChunkByName("Assets/Resources/Map/TestChunk2.tmx", 50, 0);
                        break;
                    case "7":
                        Load.LoadChunkByName("Assets/Resources/Map/TestChunk2.tmx", 50, 0);
                        break;
                    case "8":
                        Load.LoadChunkByName("Assets/Resources/Map/TestChunk2.tmx", 50, 0);
                        break;*/
                }
                if (objgo.objectCur.modifiers["loadChunk"] != "0")
                {
                    //GM.map.sizey += Load.chunk2.sizey;
                    //GM.map.sizex += Load.chunk2.sizex;
                    GM.map.tilesizey += Load.chunk2.tilesizey;
                    GM.map.tilesizex += Load.chunk2.tilesizex;
                }
                Destroy(collision.gameObject);
            }
            else
            {
                objgo.SetVisited(1);
            }
        }
        else
        {
            TileGO tilego = collision.gameObject.GetComponent<TileGO>();
            if ((tilego != null) && (tilego.tileCur.visitState != 1))
            {
                tilego.SetVisited(1);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ObjectGO objgo = collision.gameObject.GetComponent<ObjectGO>();
        if ((objgo != null) && (objgo.objectCur.visitState != 2))
        {
            objgo.SetVisited(2);
        }
        else
        {
            TileGO tilego = collision.gameObject.GetComponent<TileGO>();
            if ((tilego != null) && (tilego.tileCur.visitState != 2))
            {
                tilego.SetVisited(2);
            }
        }
    }
}
