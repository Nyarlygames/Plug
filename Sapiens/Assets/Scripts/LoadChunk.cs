using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Globalization;

public class LoadChunk {
    public MapSave chunk2 = new MapSave();
    public void LoadChunkedMap(string chunkfile)
    {
        LoadMap(chunkfile, chunk2);
    }
    
    public void LoadChunkedTiles(MapSave chunk, int startx, int starty, int sizex, int sizey, int offsetx, int offsety)
    {
        GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        int miny = 0;
        int maxy = chunk.sizey;
        if (starty - sizey > 0)
            miny = starty - sizey;
        if (starty + sizey < chunk.sizey)
            maxy = starty + sizey;
        

        int minx = 0;
        int maxx = chunk.sizex;
        if (startx - sizex > 0)
            minx = startx - sizex;
        if (startx + sizex < chunk.sizex)
            maxx = startx + sizex;
        for (int y = miny; y < maxy; y++)
        {
            for (int x = minx; x < maxx; x++)
            {
                int drawback = 10;
                /* TILES GROUND */
                TileSave tileCur = chunk.layer.tiles[y][x];
                TileSetSave tileset = new TileSetSave();
                GameObject tilego = null;
                GameObject emptyMap = GameObject.Find("Map");
                Vector3 placement = Vector3.zero;
                if (tileCur.mapid > chunk.basevalue)
                {
                    foreach (TileSetsSave tss in chunk.tilesets)
                    {
                        foreach (TileSetSave ts in tss.tilesets)
                        {
                            if ((tileCur.mapid == tss.first + ts.id))
                            {
                                tileset = ts;
                                int id_min = tileset.spritefile.Substring(0, tileset.spritefile.IndexOf("_")).Length;
                                string sub = tileset.spritefile.Substring(id_min + 1);
                                string id = sub.Substring(0, sub.IndexOf("."));

                                tilego = GameObject.Instantiate(GM.Prefabs[GM.prefabs_name[id]], Vector3.zero, Quaternion.identity);
                                tilego.name = "Instantiated : " + x + offsetx + " / " + y + offsety;
                                if (y % 2 == 1)
                                    placement = new Vector3(((x + offsetx) * chunk.tilesizex + chunk.tilesizex / 2.0f) / 100.0f, ((y + offsety) * chunk.tilesizey / 2.0f + chunk.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                else
                                    placement = new Vector3(((x + offsetx) * chunk.tilesizex + chunk.tilesizex / 2.0f) / 100.0f + (chunk.tilesizex / 2.0f / 100.0f), ((y + offsety) * chunk.tilesizey / 2.0f + chunk.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                tilego.GetComponent<Transform>().position = placement;
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
                                tilego.transform.SetParent(emptyMap.GetComponent<Transform>());
                                //TilesGO.Add(tilego);
                                break;
                            }
                        }
                        if (tileset.id > 0)
                            break;
                    }
                }
                /* ADDED GROUND */
                drawback = 9;
                if (chunk.layer.tiles[y][x].addedid > chunk.basevalue)
                {
                    TileSetSave tilesetAdded = new TileSetSave();
                    if (chunk.layer.tiles[y][x].addedid > chunk.basevalue)
                    {
                        GameObject tilegoAdded = null;
                        foreach (TileSetsSave tss in chunk.tilesets)
                        {
                            foreach (TileSetSave ts in tss.tilesets)
                            {
                                if ((chunk.layer.tiles[y][x].addedid == tss.first + ts.id))
                                {
                                    tilesetAdded = ts;
                                    int id_min = tilesetAdded.spritefile.Substring(0, tilesetAdded.spritefile.IndexOf("_")).Length;
                                    string sub = tilesetAdded.spritefile.Substring(id_min + 1);
                                    string id = sub.Substring(0, sub.IndexOf("."));
                                    tilegoAdded = GameObject.Instantiate(GM.Prefabs[GM.prefabs_name[id]]);
                                    tilegoAdded.name = "Instantiated_Added : " + x + " / " + y;
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
                                    placement = Vector3.zero;
                                    if (y % 2 == 1)
                                        placement = new Vector3(((x + offsetx) * chunk.tilesizex + chunk.tilesizex / 2.0f) / 100.0f, ((y + offsety) * chunk.tilesizey / 2.0f + chunk.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                    else
                                        placement = new Vector3(((x + offsetx) * chunk.tilesizex + chunk.tilesizex / 2.0f) / 100.0f + (chunk.tilesizex / 2.0f / 100.0f), ((y + offsety) * chunk.tilesizey / 2.0f + chunk.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                    tilegoAdded.GetComponent<Transform>().position = placement;
                                    tilegoAdded.GetComponent<Transform>().SetParent(emptyMap.GetComponent<Transform>());
                                }
                                if (tilesetAdded.id > 0)
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void LoadChunkByName(string chunk, int offsetx, int offsety)
    {
        chunk2 = new MapSave();
        SaveData sdatachunk2 = new SaveData();
        LoadMap(chunk, chunk2); // create map from file
        List<GameObject> TilesChunk2 = new List<GameObject>();
        List<GameObject> ObjectsChunk2 = new List<GameObject>();
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
