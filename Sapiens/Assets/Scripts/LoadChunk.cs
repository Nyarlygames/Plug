using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Globalization;

public class LoadChunk : MonoBehaviour {
    public MapSave chunk2;
    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadChunkByName(string chunk, int offsetx, int offsety)
    {
        chunk2 = new MapSave();
        SaveData sdatachunk2 = new SaveData();
        LoadMap(chunk, chunk2); // create map from file
        List<GameObject> TilesChunk2 = new List<GameObject>();
        List<GameObject> ObjectsChunk2 = new List<GameObject>();
        LoadMapGO(chunk2, TilesChunk2, ObjectsChunk2, sdatachunk2.mapsave.objects, offsetx, offsety);
    }



    public void LoadMapGO(MapSave mapfile, List<GameObject> TilesGO, List<GameObject> ObjectsGO, List<ObjectSave> objectssave, int offsetx, int offsety)
    {
        GameObject emptyMap = GameObject.Find("Map");
        int drawback = 10;
        for (int y = offsety; y < mapfile.sizey + offsety; y++)
        {
            for (int x = offsetx; x < mapfile.sizex + offsetx; x++)
            {
                /* TILES GROUND */
                GameObject tilego = new GameObject("[" + y + "/" + x + "]");
                tilego.AddComponent<TileGO>();
                tilego.tag = "tile";
                TileGO curtile = tilego.GetComponent<TileGO>();
                curtile.tileCur = mapfile.layer.tiles[y-offsety][x-offsetx];
                tilego.AddComponent<SpriteRenderer>();
                TileSetSave tileset = new TileSetSave();
                if (curtile.tileCur.mapid > mapfile.basevalue)
                {
                    foreach (TileSetsSave tss in mapfile.tilesets)
                    {
                        foreach (TileSetSave ts in tss.tilesets)
                        {
                            if ((curtile.tileCur.mapid == tss.first + ts.id))
                            {
                                tileset = ts;
                                tilego.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/" + tileset.spritefile.Substring(0, tileset.spritefile.Length - 4));
                                break;
                            }
                        }
                        if (tileset.id > 0)
                            break;
                    }
                }
                if (tileset.modifiers.ContainsKey("collider") && (tileset.modifiers["collider"] == "true"))
                {
                    tilego.AddComponent<PolygonCollider2D>();
                    tilego.AddComponent<PolygonCollider2D>().isTrigger = false;
                }
                else
                {
                    tilego.AddComponent<BoxCollider2D>();
                    tilego.GetComponent<BoxCollider2D>().isTrigger = true;
                }

                Vector3 placement = Vector3.zero;
                switch (mapfile.orientation)
                {
                    case "orthogonal":
                        placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                        break;
                    case "staggered":
                        if (y % 2 == 1)
                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                        else
                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                        break;
                    case "isometric":
                        if (y % 2 == 1)
                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                        else
                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                        break;
                    default:
                        placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                        break;
                }
                tilego.GetComponent<Transform>().position = placement;
                // Added ground
                if (mapfile.layer.tiles[y-offsety][x-offsetx].addedid > mapfile.basevalue)
                {
                    GameObject tilegoAdded = new GameObject("[" + y + "/" + x + "]");
                    SpriteRenderer tileadrend = tilegoAdded.AddComponent<SpriteRenderer>();
                    TileSetSave tilesetAdded = new TileSetSave();
                    if (mapfile.layer.tiles[y-offsety][x-offsetx].addedid > mapfile.basevalue)
                    {
                        foreach (TileSetsSave tss in mapfile.tilesets)
                        {
                            foreach (TileSetSave ts in tss.tilesets)
                            {
                                if ((mapfile.layer.tiles[y-offsety][x-offsetx].addedid == tss.first + ts.id))
                                {
                                    tilesetAdded = ts;
                                    tileadrend.sprite = Resources.Load<Sprite>("Map/" + tilesetAdded.spritefile.Substring(0, tilesetAdded.spritefile.Length - 4));

                                }
                            }
                            if (tilesetAdded.id > 0)
                                break;
                        }
                    }
                    if (tilesetAdded.modifiers.ContainsKey("collider") && (tilesetAdded.modifiers["collider"] == "true"))
                    {
                        tilegoAdded.AddComponent<PolygonCollider2D>();
                        tilegoAdded.GetComponent<PolygonCollider2D>().isTrigger = false;
                    }
                    else
                    {
                        tilegoAdded.AddComponent<BoxCollider2D>();
                        tilegoAdded.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    placement.z -= 1;
                    tilegoAdded.GetComponent<Transform>().position = placement;
                    tilegoAdded.GetComponent<Transform>().SetParent(tilego.GetComponent<Transform>());
                }
                tilego.transform.SetParent(emptyMap.GetComponent<Transform>());
                /* TRIGGERS */ // deleted because no need, triggers in objects is better for LD.
                /*if (mapfile.layer.tiles[y][x].triggerid > mapfile.basevalue)
                {
                    drawback = 9;
                    GameObject tilegotrigger = new GameObject("[" + y + "/" + x + "]_Trigger");
                    tilegotrigger.AddComponent<TileGO>();
                    tilegotrigger.tag = "trigger";
                    TileGO curtiletrigger = tilegotrigger.GetComponent<TileGO>();
                    curtiletrigger.tileCur = mapfile.layer.tiles[y][x];
                    tilegotrigger.AddComponent<SpriteRenderer>();
                    tilegotrigger.AddComponent<BoxCollider2D>();
                    //tilegotrigger.GetComponent<BoxCollider2D>().center = new Vector2((S.x / 2), 0);
                    TileSetSave tilesettrigger = new TileSetSave();
                    foreach (TileSetsSave tss in mapfile.tilesets)
                    {
                        foreach (TileSetSave ts in tss.tilesets)
                        {
                            if ((curtiletrigger.tileCur.triggerid == tss.first + ts.id))
                            {
                                tilesettrigger = ts;
                                tilegotrigger.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/" + tilesettrigger.spritefile.Substring(0, tilesettrigger.spritefile.Length - 4));
                                break;
                            }
                        }
                        if (tilesettrigger.id > 0)
                            break;
                    }
                    if (tilesettrigger.id > 0)
                    {
                        Vector3 placementtrigger = Vector3.zero;
                        switch (mapfile.orientation)
                        {
                            case "orthogonal":
                                placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                                break;
                            case "staggered":
                                if (y % 2 == 1)
                                    placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                                else
                                    placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                                break;
                            case "isometric":
                                if (y % 2 == 1)
                                    placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                                else
                                    placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                                break;
                            default:
                                placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, 0.0f + drawback);
                                break;
                        }
                        tilegotrigger.GetComponent<Transform>().position = placementtrigger;
                        tilegotrigger.transform.SetParent(tilego.GetComponent<Transform>());
                        Vector2 S = tilegotrigger.GetComponent<SpriteRenderer>().sprite.bounds.size;
                        tilegotrigger.GetComponent<BoxCollider2D>().size = S;
                        tilegotrigger.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    drawback = 10;
                }*/
            }
        }
        GameObject emptyGO = GameObject.Find("Objects");
        Sprite Radius = Resources.Load<Sprite>("Play/radius_rect");
        foreach (ObjectSave obj in mapfile.objects)
        {
            GameObject tilego = new GameObject("[" + obj.x + "/" + obj.y + "]" + obj.id);
            tilego.tag = "object";
            ObjectGO curObj = tilego.AddComponent<ObjectGO>();
            curObj.objectCur = obj;
            tilego.AddComponent<SpriteRenderer>();
            TileSetSave tileset = new TileSetSave();
            if (curObj.objectCur.gid > mapfile.basevalue)
            {
                foreach (TileSetsSave tss in mapfile.tilesets)
                {
                    foreach (TileSetSave ts in tss.tilesets)
                    {
                        if ((curObj.objectCur.gid == tss.first + ts.id))
                        {
                            tileset = ts;
                            foreach (string key in tileset.modifiers.Keys)
                            {
                                if (curObj.objectCur.modifiers.ContainsKey(key) == false)
                                    curObj.objectCur.modifiers.Add(key, tileset.modifiers[key]);
                            }
                            tilego.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/" + tileset.spritefile.Substring(0, tileset.spritefile.Length - 4));
                            if (curObj.objectCur.width != tileset.width)
                            {
                                curObj.GetComponent<Transform>().localScale = new Vector3((float)curObj.objectCur.width / tileset.width, (float)curObj.objectCur.height / tileset.height, 0.0f);
                            }
                            break;
                        }
                    }
                    if (tileset.id > 0)
                        break;
                }
            }
            curObj.InitObj();
            Vector3 placement = Vector3.zero;
            switch (mapfile.orientation)
            {
                case "orthogonal":
                    placement = new Vector3((curObj.objectCur.x + offsetx * mapfile.tilesizex + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f, 10.0f);
                    break;
                case "staggered":
                    if (curObj.objectCur.y % 2 == 1)
                        placement = new Vector3((curObj.objectCur.x + offsetx * mapfile.tilesizex + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey / 2) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f, 10.0f);
                    else
                        placement = new Vector3((curObj.objectCur.x + offsetx * mapfile.tilesizex + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey / 2) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f + (mapfile.tilesizey / 2.0f / 100.0f), 10.0f);
                    break;
                case "isometric":
                    placement = new Vector3((curObj.objectCur.x + offsetx * mapfile.tilesizex + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey / 2) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f, 10.0f);
                    break;
                default:
                    placement = new Vector3((curObj.objectCur.x + offsetx * mapfile.tilesizex + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f, 10.0f);
                    break;
            }
            if (curObj.objectCur.modifiers.ContainsKey("collider") && (curObj.objectCur.modifiers["collider"] == "true"))
                tilego.AddComponent<PolygonCollider2D>();
            else if (curObj.objectCur.modifiers.ContainsValue("trigger"))
            {
                curObj.addTrigger(tilego, Radius, objectssave);
            }
            else
            {
                tilego.AddComponent<BoxCollider2D>();
                tilego.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            tilego.GetComponent<Transform>().position = placement;
            tilego.transform.SetParent(emptyGO.GetComponent<Transform>());
        }
    }

    public void LoadMap(string mapfile, MapSave map)
    {
        StreamReader reader = new StreamReader(mapfile);
        string line = reader.ReadLine();
        while (!reader.EndOfStream)
        {
            if (line.Contains("<map"))
            {
                map.sizex = Convert.ToInt32(map.GetValueFromKey("width", line));
                map.sizey = Convert.ToInt32(map.GetValueFromKey("height", line));
                map.tilesizex = Convert.ToInt32(map.GetValueFromKey("tilewidth", line));
                map.tilesizey = Convert.ToInt32(map.GetValueFromKey("tileheight", line));
                map.basevalue = Convert.ToInt32(map.GetValueFromKey("infinite", line));
                map.render = map.GetValueFromKey("renderorder", line);
                map.orientation = map.GetValueFromKey("orientation", line);
                map.nextobject = Convert.ToInt32(map.GetValueFromKey("nextobjectid", line));
                LayerSave maptiles = new LayerSave();
                map.layer = maptiles; // here
            }
            if (line.Contains("<layer"))
            {
                LayerSave ground = map.layer;
                ground.name = map.GetValueFromKey("name", line);
                ground.sizex = Convert.ToInt32(map.GetValueFromKey("width", line));
                ground.sizey = Convert.ToInt32(map.GetValueFromKey("height", line));
                line = reader.ReadLine();
                if (line.Contains("<data encoding=\"csv\""))
                {

                    string newid = reader.ReadLine();
                    for (int j = 0; j < ground.sizey; j++)
                    {
                        if (ground.name == "Ground")
                            ground.tiles.Add(new List<TileSave>());
                        for (int i = 0; i < ground.sizex; i++)
                        {
                            TileSave tilesave = new TileSave();
                            if (ground.name == "Ground")
                            {
                                if (newid.IndexOf(",") > 0)
                                {
                                    tilesave.mapid = Convert.ToInt32(newid.Substring(0, newid.IndexOf(",")));
                                    // newid for other layers
                                    newid = newid.Substring(newid.IndexOf(",") + 1);
                                    // same
                                }
                                else
                                {
                                    tilesave.mapid = Convert.ToInt32(newid);
                                }
                                tilesave.posx = i;
                                tilesave.posy = j;
                                ground.tiles[j].Add(tilesave);
                            }
                            /*else if (ground.name == "Triggers")
                            {
                                tilesave = ground.tiles[j][i];
                                if (newid.IndexOf(",") > 0)
                                {
                                    tilesave.triggerid = Convert.ToInt32(newid.Substring(0, newid.IndexOf(",")));
                                    newid = newid.Substring(newid.IndexOf(",") + 1);
                                }
                                else
                                {
                                    tilesave.triggerid = Convert.ToInt32(newid);
                                }
                            }*/
                            else if (ground.name == "AddedGround")
                            {
                                // tile already created in ground.
                                tilesave = ground.tiles[j][i];
                                if (newid.IndexOf(",") > 0)
                                {
                                    tilesave.addedid = Convert.ToInt32(newid.Substring(0, newid.IndexOf(",")));
                                    newid = newid.Substring(newid.IndexOf(",") + 1);
                                }
                                else
                                {
                                    tilesave.addedid = Convert.ToInt32(newid);
                                }
                            }
                        }
                        newid = reader.ReadLine();
                    }
                }
            }
            if (line.Contains("<objectgroup"))
            {
                ObjectGroupSave objectlayer = new ObjectGroupSave();
                objectlayer.name = map.GetValueFromKey("name", line);
                if (line.Contains("offsetx"))
                    objectlayer.offsetx = Convert.ToInt32(map.GetValueFromKey("offsetx", line));
                if (line.Contains("offsety"))
                    objectlayer.offsety = Convert.ToInt32(map.GetValueFromKey("offsety", line));
                map.objectgroups.Add(objectlayer);
                line = reader.ReadLine();
                if (line.Contains("<object"))
                {
                    while (!line.Contains("</objectgroup>"))
                    {
                        ObjectSave obj = new ObjectSave();
                        obj.id = Convert.ToInt32(map.GetValueFromKey("id", line));
                        obj.gid = Convert.ToInt32(map.GetValueFromKey("gid", line));
                        obj.x = Convert.ToSingle(map.GetValueFromKey("x", line), CultureInfo.InvariantCulture.NumberFormat);
                        obj.y = Convert.ToSingle(map.GetValueFromKey("y", line));
                        obj.width = Convert.ToInt32(map.GetValueFromKey("width", line));
                        obj.height = Convert.ToInt32(map.GetValueFromKey("height", line));
                        obj.offsetx = objectlayer.offsetx;
                        obj.offsety = objectlayer.offsety;

                        line = reader.ReadLine();
                        if (line.Contains("<properties"))
                        {
                            while ((!line.Contains("</object>")) && (!line.Contains("</properties")))
                            {
                                if (line.Contains("<property"))
                                {
                                    obj.modifiers.Add(map.GetValueFromKey("name", line), map.GetValueFromKey("value", line));
                                }
                                line = reader.ReadLine();
                            }
                            if (line.Contains("</properties"))
                                line = reader.ReadLine();
                            line = reader.ReadLine();
                        }
                        map.objects.Add(obj);
                    }
                }
            }
            if (line.Contains("<tileset"))
            {
                TileSetsSave tilesets = new TileSetsSave();
                tilesets.first = Convert.ToInt32(map.GetValueFromKey("firstgid", line));
                tilesets.source = map.GetValueFromKey("source", line);
                StreamReader readertileset = new StreamReader("Assets/Resources/Map/" + tilesets.source);
                int internid = 0;
                string linetileset = readertileset.ReadLine();
                while (!readertileset.EndOfStream)
                {
                    if (linetileset.Contains("<tileset"))
                    {
                        tilesets.name = map.GetValueFromKey("name", linetileset);
                        tilesets.tilewidth = Convert.ToInt32(map.GetValueFromKey("tilewidth", linetileset));
                        tilesets.tileheight = Convert.ToInt32(map.GetValueFromKey("tileheight", linetileset));
                        tilesets.spritecount = Convert.ToInt32(map.GetValueFromKey("tilecount", linetileset));
                    }
                    if (linetileset.Contains("<tile "))
                    {
                        TileSetSave tileset = new TileSetSave();
                        tileset.id = Convert.ToInt32(map.GetValueFromKey("id", linetileset));
                        linetileset = readertileset.ReadLine();
                        tileset.internal_id = internid;
                        while (!linetileset.Contains("</tile>"))
                        {
                            if (linetileset.Contains("<property"))
                            {
                                tileset.modifiers.Add(map.GetValueFromKey("name", linetileset), map.GetValueFromKey("value", linetileset));
                            }
                            if (linetileset.Contains("<image") && (!linetileset.Contains("format")))
                            {
                                tileset.height = Convert.ToInt32(map.GetValueFromKey("height", linetileset));
                                tileset.spritefile = map.GetValueFromKey("source", linetileset);
                                tileset.width = Convert.ToInt32(map.GetValueFromKey("width", linetileset));
                            }
                            linetileset = readertileset.ReadLine();
                        }
                        internid++;
                        tilesets.tilesets.Add(tileset);
                    }
                    linetileset = readertileset.ReadLine();
                }
                map.tilesets.Add(tilesets);
            }
            line = reader.ReadLine();
        }
        map.layer.tiles.Reverse();
        reader.Close();
    }
}
