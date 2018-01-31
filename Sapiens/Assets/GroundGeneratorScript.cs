using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneratorScript : MonoBehaviour {
    public int SizeX = 0;
    public int SizeY = 0;

    public float TileSizeX = 0;
    public float TileSizeY = 0;

    public int SightRadiusX = 0;
    public int SightRadiusY = 0;

    public List<List<GameObject>> Tiles;
    public List<List<TileScript>> TileMap;

    int tribeX = 0;
    int tribeY = 0;
    Transform tribePos;
    ControlsScript Cs;


    GameObject TileP;
    GameObject TileF;

    // Use this for initialization
    void Start()
    {
    }

    public void Generate_Map()
    {
        Cs = GameObject.Find("Controls").GetComponent<ControlsScript>();
        tribePos = GameObject.Find("Tribe").GetComponent<Transform>();
        TileP = Resources.Load("Play/Prefabs/Tiles/Tile_Plains") as GameObject;
        TileF = Resources.Load("Play/Prefabs/Tiles/Tile_Forests") as GameObject;
        Tiles = new List<List<GameObject>>(SizeX);
        TileMap = new List<List<TileScript>>(SizeX);
        for (int x = 0; x < SizeX; x++)
        {
            //List<GameObject> temp = new List<GameObject>(SizeY);
            List<TileScript> tempMap = new List<TileScript>(SizeY);
            for (int y = 0; y < SizeY; y++)
            {
                int first_random = (int)Random.Range(1.0f, 101.0f);
                TileScript tempTile = new TileScript();
                Vector3 offset = new Vector3(x * TileSizeX + (y * 2.75f), Cs.GroundPlane, y * TileSizeY);
                if (first_random <= 95) {
                    tempTile.type = "Forest";
                    tempTile.position = offset;
                    tempTile.x = x;
                    tempTile.y = y;
                    if ((Camera.main.WorldToViewportPoint(tempTile.position).x > 0) && (Camera.main.WorldToViewportPoint(tempTile.position).x < 1) && (Camera.main.WorldToViewportPoint(tempTile.position).y > 0) && (Camera.main.WorldToViewportPoint(tempTile.position).y < 1))
                    {
                        tempTile.displayed = true;
                        tempTile.Tile = Instantiate(TileF);
                        tempTile.Tile.GetComponent<Transform>().position = offset;
                    }
                }
                else {
                    tempTile.type = "Plains";
                    tempTile.position = offset;
                    tempTile.x = x;
                    tempTile.y = y;
                    if ((Camera.main.WorldToViewportPoint(tempTile.position).x > 0) && (Camera.main.WorldToViewportPoint(tempTile.position).x < 1) && (Camera.main.WorldToViewportPoint(tempTile.position).y > 0) && (Camera.main.WorldToViewportPoint(tempTile.position).y < 1))
                    {
                        tempTile.displayed = true;
                        tempTile.Tile = Instantiate(TileP);
                        tempTile.Tile.GetComponent<Transform>().position = offset;
                    }
                }
                tempMap.Add(tempTile);
            }
            TileMap.Add(tempMap);
        }

        Generate_Biomes();
    }

    void Generate_Biomes()
    {

    }
	
	// Update is called once per frame
	void Update () {
    }

    void FixedUpdate()
    {
        // ---------------------------------------------------------------------------------------- TODO  : memorize what is displayed and don't recheck ----------------------------------------------------------------- //
        tribeY = (int) (tribePos.position.z / TileSizeY);
        tribeX = (int)((tribePos.position.x - (tribeY * 2.75f)) / TileSizeX);
        for (int x = tribeX - SightRadiusX; x < tribeX + SightRadiusX; x++)
        {
            if ((x >= 0) && (x < SizeX))
            {
                for (int y = tribeY - SightRadiusY; y < tribeY + SightRadiusY; y++)
                {
                    if ((y < SizeY) && (y >= 0) && (TileMap[x][y].displayed == false))
                    {
                        TileMap[x][y].displayed = true;
                        switch (TileMap[x][y].type)
                        {
                            case "Forest":
                                TileMap[x][y].Tile = Instantiate(TileF);
                                TileMap[x][y].Tile.GetComponent<Transform>().position = TileMap[x][y].position;
                                break;
                            case "Plains":
                                TileMap[x][y].Tile = Instantiate(TileP);
                                TileMap[x][y].Tile.GetComponent<Transform>().position = TileMap[x][y].position;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
