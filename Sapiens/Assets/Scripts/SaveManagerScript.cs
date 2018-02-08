using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Globalization;


public class SaveManagerScript {

    GameManager GM;
    public GameObject LoadGO(SaveData sdata)
    {
        GameObject Tribe = new GameObject(sdata.tribesave.tribename);
        GameObject Tribe_Members = new GameObject("Tribe_Members");
        Tribe.AddComponent<TribeGO>();
        TribeGO tribego = Tribe.GetComponent<TribeGO>();
        tribego.tribeCurrent = sdata.tribesave;
        if (sdata.tribesave != null)
        {
            if (sdata.tribesave.members.Count > 0)
            {
                foreach (CharacterSave chara in sdata.tribesave.members)
                {
                    GameObject newchar = new GameObject(chara.name);
                    newchar.AddComponent<CharacterGO>();
                    newchar.GetComponent<CharacterGO>().charCurrent = chara;
                    newchar.AddComponent<SpriteRenderer>();
                    Texture2D sprite = Resources.Load<Texture2D>(chara.charSprite);
                    Sprite FaceSprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), new Vector2(0.5f, 0.5f));
                    newchar.GetComponent<SpriteRenderer>().sprite = FaceSprite;
                    newchar.GetComponent<Transform>().position = new Vector3(chara.x, chara.y, chara.z);
                    newchar.transform.SetParent(Tribe_Members.transform);
                    tribego.CharsGO.Add(newchar);
                }
            }
        }
        return Tribe;
    }

    public SaveData LoadSave(string filename)
    {
        if (File.Exists("Save/"+filename+"/"+filename))
        {
            SaveData sdata = new SaveData();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(sdata.savefolder + filename + "/"  + filename, FileMode.Open);
            sdata = (SaveData)bf.Deserialize(file); // load saveX file names
            file.Close();
            return (sdata);
        }
        else
        { 
            return null; // can't load, don't exist
        }
    }

    public void SaveGame(SaveData previous, GameObject Tribe, MapSave Map) {
        
        if (Directory.Exists(previous.savefolder + previous.savefile + "/") == false)
        {
            Directory.CreateDirectory(previous.savefolder + previous.savefile + "/");
        }

        BinaryFormatter bf = new BinaryFormatter();
        previous.tribesave = Tribe.GetComponent<TribeGO>().tribeCurrent;
        previous.mapsave = Map;
        if(PlayerPrefs.GetString("mapfile") != "")
        {
            previous.mapfile = PlayerPrefs.GetString("mapfile");
        }
        FileStream file = File.Open(previous.savefolder + previous.savefile + "/" + previous.savefile, FileMode.OpenOrCreate);
        bf.Serialize(file, previous);
        file.Close();

    }

    public void LoadMapGO(MapSave mapfile, List<GameObject> TilesGO, List<GameObject> ObjectsGO)
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject emptyMap = new GameObject("Map");
        foreach (LayerSave lay in mapfile.layers) {
            for (int y = 0; y < mapfile.sizey; y++)
            {
                for (int x = 0; x < mapfile.sizex; x++)
                {
                    GameObject tilego = new GameObject("[" + y + "/" + x + "]");
                    tilego.AddComponent<TileGO>();
                    TileGO curtile = tilego.GetComponent<TileGO>();
                    curtile.tileCur = lay.tiles[y][x];
                    tilego.AddComponent<SpriteRenderer>();
                    TileSetSave tileset = new TileSetSave();
                    if (curtile.tileCur.id > mapfile.basevalue)
                    {
                        foreach (TileSetsSave tss in mapfile.tilesets)
                        {
                            if ((curtile.tileCur.id >= tss.first) && (curtile.tileCur.id < tss.first + tss.spritecount))
                            {
                                tileset = tss.tilesets[curtile.tileCur.id - tss.first];
                                break;
                            }
                        }
                        if (tileset.spritefile != "")
                        {
                            tilego.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/" + tileset.spritefile.Substring(0, tileset.spritefile.Length - 4));
                        }
                    }
                    tilego.GetComponent<Transform>().position = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround);
                    tilego.transform.SetParent(emptyMap.GetComponent<Transform>());
                }
            }
        }
        GameObject emptyGO = new GameObject("Objects");        
        foreach (ObjectSave obj in mapfile.objects) { 
            GameObject tilego = new GameObject("[" + obj.x + "/" + obj.y + "]" + obj.id);
            tilego.AddComponent<ObjectGO>();
            ObjectGO curObj = tilego.GetComponent<ObjectGO>();
            curObj.objectCur = obj;
            tilego.AddComponent<SpriteRenderer>();
            TileSetSave tileset = new TileSetSave();
            if (curObj.objectCur.gid > mapfile.basevalue)
            {
                foreach (TileSetsSave tss in mapfile.tilesets)
                {
                    if ((curObj.objectCur.gid >= tss.first) && (curObj.objectCur.gid < tss.first + tss.spritecount))
                    {
                        tileset = tss.tilesets[curObj.objectCur.gid - tss.first];
                        break;
                    }
                }
                if (tileset.spritefile != "")
                {
                    tilego.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/" + tileset.spritefile.Substring(0, tileset.spritefile.Length - 4));
                    if (curObj.objectCur.width != tileset.width)
                    {
                        curObj.GetComponent<Transform>().localScale = new Vector3((float)curObj.objectCur.width / tileset.width, (float)curObj.objectCur.height / tileset.height, 0.0f);
                    }
                }
            }
            tilego.GetComponent<Transform>().position = new Vector3((curObj.objectCur.x + curObj.objectCur.offsetx + curObj.objectCur.width/2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height/2))) / 100.0f, GM.ZObjects);
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
            }
            if (line.Contains("<layer"))
            {
                LayerSave ground = new LayerSave();
                ground.name = map.GetValueFromKey("name", line);
                    ground.sizex = Convert.ToInt32(map.GetValueFromKey("width", line));
                    ground.sizey = Convert.ToInt32(map.GetValueFromKey("height", line));
                    map.layers.Add(ground);
                    line = reader.ReadLine();
                    if (line.Contains("<data encoding=\"csv\""))
                    {

                        string newid = reader.ReadLine();
                        for (int j = 0; j < ground.sizey; j++)
                        {
                            ground.tiles.Add(new List<TileSave>());
                            for (int i = 0; i < ground.sizex; i++)
                            {
                                TileSave tilesave = new TileSave();
                                if (newid.Length >= 2)
                                {
                                    tilesave.id = Convert.ToInt32(newid.Substring(0, 1));
                                    ground.tiles[j].Add(tilesave);
                                    newid = newid.Substring(2);
                                }
                                else
                                {
                                    tilesave.id = Convert.ToInt32(newid);
                                    ground.tiles[j].Add(tilesave);
                                }
                                tilesave.posx = i;
                                tilesave.posy = j;
                            }
                            if (j < ground.sizey) // to prevent reading </data>
                                newid = reader.ReadLine();
                        }
                    }
                    ground.tiles.Reverse();
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
                    map.objects.Add(obj);
                    line = reader.ReadLine();
                }
                //map.objects.Reverse();
            }
            if (line.Contains("<tileset"))
            {
                TileSetsSave tilesets = new TileSetsSave();
                tilesets.first = Convert.ToInt32(map.GetValueFromKey("firstgid", line));
                tilesets.source = map.GetValueFromKey("source", line);
                StreamReader readertileset = new StreamReader("Assets/Resources/Map/" + tilesets.source);
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
                    if (linetileset.Contains("<image"))
                    {
                        TileSetSave tileset = new TileSetSave();
                        tileset.height = Convert.ToInt32(map.GetValueFromKey("height", linetileset));
                        tileset.spritefile = map.GetValueFromKey("source", linetileset);
                        tileset.width = Convert.ToInt32(map.GetValueFromKey("width", linetileset));
                        tilesets.tilesets.Add(tileset);
                    }
                    linetileset = readertileset.ReadLine();
                }
                map.tilesets.Add(tilesets);
            }
            line = reader.ReadLine();
        }
        reader.Close();
    }
}
