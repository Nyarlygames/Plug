using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneratorScript : MonoBehaviour {
    public int SizeX = 0;
    public int SizeY = 0;

    public List<List<GameObject>> Tiles;
    ControlsScript Cs;


    // Use this for initialization
    void Start()
    {
        Cs = GameObject.Find("Controls").GetComponent<ControlsScript>();
        Tiles = new List<List<GameObject>>(SizeX);
        GameObject Tile = Resources.Load("Play/Prefabs/Tiles/Tile_Plains") as GameObject;
        for (int x = 0; x < SizeX; x++)
        {
            // Debug.Log("i : " + i + " tiles : " + Tiles + " yolo : " + Tiles[0]);
            // Tiles[i] = new List<GameObject>(SizeY);
            List<GameObject> temp = new List<GameObject>(SizeY);
            for (int y = 0; y < SizeY; y++)
            {
                GameObject tempTile = Instantiate(Tile);
                Vector3 offset = new Vector3((x) * tempTile.GetComponent<SpriteRenderer>().bounds.size.x * 0.6f + y * 2.7f, Cs.GroundPlane, y * tempTile.GetComponent<SpriteRenderer>().bounds.size.z);
                tempTile.GetComponent<Transform>().position = offset;
                temp.Add(tempTile);

            }
            Tiles.Add(temp);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
