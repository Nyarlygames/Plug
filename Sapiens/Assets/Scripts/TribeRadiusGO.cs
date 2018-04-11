using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TribeRadiusGO : MonoBehaviour {
    GameObject[] triggers;
    GameManager GM;

    // Use this for initialization
    void Start () {
        triggers = GameObject.FindGameObjectsWithTag("trigger");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {


    }

    private void FixedUpdate()
    {
         //RaycastHit hit;
         if (Physics.Raycast(transform.position, Vector3.up, 10))
         {
             // executes if hits a collider
         }
         else
         {
             // executes if it doesnt hit any collider
         }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectGO objgo = collision.gameObject.GetComponent<ObjectGO>();
        if ((objgo != null) && (objgo.objectCur.visitState != 1))
        {
            if (objgo.tag == "Loader")
            {
                //LoadChunk Load = GameObject.Find("Load_Chunk").GetComponent<LoadChunk>();
                GM.LoadManager.LoadChunkedMap(objgo.objectCur.modifiers["chunkfile"]);
                GM.LoadManager.LoadChunkedTiles(GM.LoadManager.chunk2, 10, 0, 10, 10, 0, GM.map.sizey);
                GM.map.tilesizex += 0;
                GM.map.tilesizey += 10;
                /*if (objgo.objectCur.modifiers["loadChunk"] != "0")
                {
                    //GM.map.sizey += Load.chunk2.sizey;
                    //GM.map.sizex += Load.chunk2.sizex;
                    GM.map.tilesizey += Load.chunk2.tilesizey;
                    GM.map.tilesizex += Load.chunk2.tilesizex;
                }*/
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
