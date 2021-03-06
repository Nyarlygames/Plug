﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Globalization;


public class SaveManagerScript
 {

    GameManager GM;
   // public List<GameObject> PrefabTiles = new List<GameObject>();
    public GameObject LoadGO(SaveData sdata)
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject Tribe = new GameObject(sdata.tribesave.tribename);
        GameObject Tribe_Members = new GameObject("Tribe_Members");
        Tribe_Members.AddComponent<SpriteRenderer>();
        Tribe_Members.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Play/Sprite_Camp");
        Tribe_Members.AddComponent<BoxCollider2D>();
        Tribe_Members.AddComponent<TribeMembersGO>();
        GameObject Tribe_Radius = new GameObject("Tribe_Radius");
        RatioFactory RF = new RatioFactory();
        Tribe_Radius.AddComponent<CircleCollider2D>();
        Tribe_Radius.GetComponent<CircleCollider2D>().radius = RF.tribe_sightradius;
        Tribe_Radius.AddComponent<TribeRadiusGO>();
        Tribe_Radius.GetComponent<CircleCollider2D>().isTrigger = true;
        Tribe_Radius.GetComponent<Transform>().SetParent(Tribe_Members.GetComponent<Transform>());
        Tribe_Radius.AddComponent<SpriteRenderer>();
        Sprite Radius = Resources.Load<Sprite>("Play/radius");
        Tribe_Radius.GetComponent<SpriteRenderer>().sprite = Radius;
        Tribe_Radius.GetComponent<Transform>().position = new Vector3(Tribe_Radius.GetComponent<Transform>().position.x, Tribe_Radius.GetComponent<Transform>().position.y, GM.ZGround);
        Tribe_Radius.GetComponent<Transform>().localScale = new Vector3(RF.tribe_sightradius, RF.tribe_sightradius, 1.0f);
        Tribe_Radius.tag = "radius";
        Tribe.AddComponent<TribeGO>();
        TribeGO tribego = Tribe.GetComponent<TribeGO>();
        GameObject Tribe_Fire = new GameObject("Tribe_Fire");
        Tribe_Fire.AddComponent<SpriteRenderer>();
        Sprite[] Fire = Resources.LoadAll<Sprite>("Play/Feu_Spritesheet");
        Tribe_Fire.GetComponent<SpriteRenderer>().sprite = Fire[0];
        Tribe_Members.GetComponent<TribeMembersGO>().fire = Tribe_Fire;
        Tribe_Members.GetComponent<TribeMembersGO>().firesprites = Fire;
        Tribe_Members.GetComponent<TribeMembersGO>().radius = Tribe_Radius;
        Vector3 frontfire = new Vector3(Tribe_Members.GetComponent<Transform>().position.x, Tribe_Members.GetComponent<Transform>().position.y, Tribe_Members.GetComponent<Transform>().position.z - 0.5f);
        Tribe_Fire.GetComponent<Transform>().position = frontfire;
        Tribe_Fire.GetComponent<Transform>().SetParent(Tribe_Members.GetComponent<Transform>());
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

                    GameObject body = new GameObject(chara.name + "_Body");
                    Transform bodyT = body.GetComponent<Transform>();
                    bodyT.SetParent(newchar.GetComponent<Transform>());
                    bodyT.position = new Vector3(bodyT.position.x, bodyT.position.y, GM.ZCharacters + 5);
                    body.AddComponent<SpriteRenderer>();
                    Sprite FaceSprite;
                    if (chara.sexe == 0)
                        FaceSprite = GM.basesF[chara.Pic_base];
                    else
                        FaceSprite = GM.basesM[chara.Pic_base];
                    body.GetComponent<SpriteRenderer>().sprite = FaceSprite;

                    GameObject paints = new GameObject(chara.name + "_Paints");
                    Transform paintsT = paints.GetComponent<Transform>();
                    paintsT.SetParent(newchar.GetComponent<Transform>());
                    paintsT.position = new Vector3(paintsT.position.x, paintsT.position.y, GM.ZCharacters + 4);
                    paints.AddComponent<SpriteRenderer>();
                    Sprite PaintSprite;
                    if (chara.sexe == 0)
                        PaintSprite = GM.paintsF[chara.Pic_paints];
                    else
                        PaintSprite = GM.paintsM[chara.Pic_paints];
                    paints.GetComponent<SpriteRenderer>().sprite = PaintSprite;

                    GameObject hairs = new GameObject(chara.name + "_Hairs");
                    Transform hairsT = hairs.GetComponent<Transform>();
                    hairsT.SetParent(newchar.GetComponent<Transform>());
                    hairsT.position = new Vector3(hairsT.position.x, hairsT.position.y, GM.ZCharacters + 3);
                    hairs.AddComponent<SpriteRenderer>();
                    Sprite HairsSprite;
                    if (chara.sexe == 0)
                        HairsSprite = GM.hairsF[chara.Pic_hairs];
                    else
                        HairsSprite = GM.hairsM[chara.Pic_hairs];
                    hairs.GetComponent<SpriteRenderer>().sprite = HairsSprite;

                    GameObject extra = new GameObject(chara.name + "_Extra");
                    Transform extraT = extra.GetComponent<Transform>();
                    extraT.SetParent(newchar.GetComponent<Transform>());
                    extraT.position = new Vector3(extraT.position.x, extraT.position.y, GM.ZCharacters + 2);
                    extra.AddComponent<SpriteRenderer>();
                    Sprite ExtraSprite;
                    if (chara.sexe == 0)
                        ExtraSprite = GM.jewelsF[chara.Pic_jewels];
                    else
                        ExtraSprite = GM.beardsM[chara.Pic_beard];
                    extra.GetComponent<SpriteRenderer>().sprite = ExtraSprite;

                    GameObject clothes = new GameObject(chara.name + "_Clothes");
                    Transform clothesT = clothes.GetComponent<Transform>();
                    clothesT.SetParent(newchar.GetComponent<Transform>());
                    clothesT.position = new Vector3(clothesT.position.x, clothesT.position.y, GM.ZCharacters + 1);
                    clothes.AddComponent<SpriteRenderer>();
                    Sprite ClothesSprite;
                    if (chara.sexe == 0)
                        ClothesSprite = GM.clothesF[chara.Pic_clothes];
                    else
                        ClothesSprite = GM.clothesM[chara.Pic_clothes];
                    clothes.GetComponent<SpriteRenderer>().sprite = ClothesSprite;
                    
                    newchar.GetComponent<Transform>().position =  Vector3.zero;//(chara.x, chara.y, chara.z);
                    newchar.GetComponent<Transform>().SetParent(Tribe_Members.transform);

                    paints.GetComponent<SpriteRenderer>().enabled = false;
                    extra.GetComponent<SpriteRenderer>().enabled = false;
                    clothes.GetComponent<SpriteRenderer>().enabled = false;
                    hairs.GetComponent<SpriteRenderer>().enabled = false;
                    body.GetComponent<SpriteRenderer>().enabled = false;

                    tribego.CharsGO.Add(newchar);
                }
                Vector3 TribePos = new Vector3(sdata.tribesave.TribePosX, sdata.tribesave.TribePosY, GM.ZCharacters);
                Tribe_Members.GetComponent<Transform>().position = TribePos;
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
            return null; // can't load, doesn't exist
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
        previous.eventsave = GM.EM.events;
        if(PlayerPrefs.GetString("mapfile") != "")
        {
            previous.mapfile = PlayerPrefs.GetString("mapfile");
        }
        FileStream file = File.Open(previous.savefolder + previous.savefile + "/" + previous.savefile, FileMode.OpenOrCreate);
        bf.Serialize(file, previous);
        file.Close();

    }

    public void LoadMapGO(MapSave mapfile, List<GameObject> TilesGO, List<GameObject> ObjectsGO, List<ObjectSave> objectssave, List<GameObject> Ground_Prefabs)
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject emptyMap = new GameObject("Map");
        Vector3 placement = Vector3.zero;
        for (int y = 0; y < mapfile.sizey; y++)
        {
            for (int x = 0; x < mapfile.sizex; x++)
            {
                int drawback = 10;
                /* TILES GROUND */
                TileSave tileCur = mapfile.layer.tiles[y][x];
                TileSetSave tileset = new TileSetSave();
                GameObject tilego = null;
                if (tileCur.mapid > mapfile.basevalue)
                    {
                        foreach (TileSetsSave tss in mapfile.tilesets)
                        {
                            foreach (TileSetSave ts in tss.tilesets)
                            {
                                if ((tileCur.mapid == tss.first + ts.id))
                                {
                                    tileset = ts;
                                    int id_min = tileset.spritefile.Substring(0, tileset.spritefile.IndexOf("_")).Length;
                                    string sub = tileset.spritefile.Substring(id_min+1); 
                                    string id = sub.Substring(0, sub.IndexOf("."));

                                    tilego = GameObject.Instantiate(Ground_Prefabs[GM.prefabs_name[id]], Vector3.zero, Quaternion.identity);
                                    tilego.name = "Instantiated : " + x + " / " + y;                                    
                                // useless but keep just in case
                                    /*switch (mapfile.orientation)
                                    {
                                        case "orthogonal":
                                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround+ drawback);
                                            break;
                                        case "staggered":*/
                                            if (y % 2 == 1)
                                                placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                            else
                                                placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                          /*  break;
                                        case "isometric":
                                            if (y % 2 == 1)
                                                placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                            else
                                                placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                            break;
                                        default:
                                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                            break;
                                    }*/
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
                                    TilesGO.Add(tilego);
                                break;
                                }
                            }
                            if (tileset.id > 0)
                                break;
                        }
                    }
                /* ADDED GROUND */
                drawback=9;
                if (mapfile.layer.tiles[y][x].addedid > mapfile.basevalue)
                {
                    TileSetSave tilesetAdded = new TileSetSave();
                    if (mapfile.layer.tiles[y][x].addedid > mapfile.basevalue)
                    {
                        GameObject tilegoAdded = null;
                        foreach (TileSetsSave tss in mapfile.tilesets)
                        {
                            foreach (TileSetSave ts in tss.tilesets)
                            {
                                if ((mapfile.layer.tiles[y][x].addedid == tss.first + ts.id))
                                {
                                    tilesetAdded = ts;
                                    int id_min = tilesetAdded.spritefile.Substring(0, tilesetAdded.spritefile.IndexOf("_")).Length;
                                    string sub = tilesetAdded.spritefile.Substring(id_min + 1);
                                    string id = sub.Substring(0, sub.IndexOf("."));
                                    tilegoAdded = GameObject.Instantiate(Ground_Prefabs[GM.prefabs_name[id]]);
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
                                    // useless but keep just in case
                                    /* switch (mapfile.orientation)
                                     {
                                         case "orthogonal":
                                             placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                             break;
                                         case "staggered":*/
                                    if (y % 2 == 1)
                                                placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                            else
                                                placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                           /* break;
                                        case "isometric":
                                            if (y % 2 == 1)
                                                placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                            else
                                                placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                            break;
                                        default:
                                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                            break;
                                    }*/
                                    tilegoAdded.GetComponent<Transform>().position = placement;
                                    // maybe use one day for sorting, but there's an issue with white shader on addedground
                                   // if (tilego == null)
                                        tilegoAdded.GetComponent<Transform>().SetParent(emptyMap.GetComponent<Transform>());
                                   // else
                                   //     tilegoAdded.GetComponent<Transform>().SetParent(tilego.GetComponent<Transform>());
                                }
                                if (tilesetAdded.id > 0)
                                    break;
                            }
                        }
                    }
                }
            }
        }
        GameObject emptyGO = new GameObject("Objects");
        Sprite Radius = Resources.Load<Sprite>("Play/radius_rect");
        foreach (ObjectSave obj in mapfile.objects)
        {
            GameObject curObj = null;
            ObjectGO curObjGO = null;
            TileSetSave tileset = new TileSetSave();
            if (obj.gid > mapfile.basevalue)
            {
                foreach (TileSetsSave tss in mapfile.tilesets)
                {
                    foreach (TileSetSave ts in tss.tilesets)
                    {
                        if ((obj.gid == tss.first + ts.id))
                        {
                            tileset = ts;
                            int id_min = ts.spritefile.Substring(0, ts.spritefile.IndexOf("_")).Length;
                            string sub = ts.spritefile.Substring(id_min + 1);
                            string id = sub.Substring(0, sub.IndexOf("."));
                            foreach (string key in tileset.modifiers.Keys)
                            {
                                if (obj.modifiers.ContainsKey(key) == false)
                                    obj.modifiers.Add(key, tileset.modifiers[key]);
                            }
                            curObj = GameObject.Instantiate(Ground_Prefabs[GM.prefabs_name[id]]);
                            curObj.tag = "object";
                            curObj.name = "Instantiated_Added : " + obj.x + " / " + obj.y;

                            if (obj.width != tileset.width)
                            {
                                curObj.GetComponent<Transform>().localScale = new Vector3((float)obj.width / tileset.width, (float)obj.height / tileset.height, 0.0f);
                            }
                            curObjGO = curObj.GetComponent<ObjectGO>();
                            curObjGO.objectCur = obj;
                            break;
                        }
                    }
                    if (tileset.id > 0)
                        break;
                }
            }
            if ((curObj != null) && (curObjGO != null))
            {
                placement = Vector3.zero;
                curObjGO.InitObj();
               /* switch (mapfile.orientation)
                {
                    case "orthogonal":
                        placement = new Vector3((obj.x + obj.offsetx + obj.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey) - ((obj.y + obj.offsety - obj.height / 2.0f))) / 100.0f, GM.ZObjects);
                        break;
                    case "staggered":*/
                        if (obj.y % 2 == 1)
                            placement = new Vector3((obj.x + obj.offsetx + obj.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey / 2) - ((obj.y + obj.offsety - obj.height / 2.0f))) / 100.0f, GM.ZObjects);
                        else
                            placement = new Vector3((obj.x + obj.offsetx + obj.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey / 2) - ((obj.y + obj.offsety - obj.height / 2.0f))) / 100.0f + (mapfile.tilesizey / 2.0f / 100.0f), GM.ZObjects);
                        /*break;
                    case "isometric":
                        placement = new Vector3((obj.x + obj.offsetx + obj.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey / 2) - ((obj.y + obj.offsety - obj.height / 2.0f))) / 100.0f, GM.ZObjects);
                        break;
                    default:
                        placement = new Vector3((obj.x + obj.offsetx + obj.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey) - ((obj.y + obj.offsety - obj.height / 2.0f))) / 100.0f, GM.ZObjects);
                        break;
                }*/
                placement.z += 2;
                if (obj.modifiers.ContainsKey("collider") && (obj.modifiers["collider"] == "true"))
                    curObj.AddComponent<PolygonCollider2D>();
                else if (obj.modifiers.ContainsValue("trigger"))
                {
                    curObjGO.addTrigger(curObj, Radius, objectssave);
                }
                else
                {
                    curObj.AddComponent<BoxCollider2D>();
                    curObj.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                if (obj.modifiers.ContainsKey("loadChunk"))
                {
                    curObj.tag = "Loader";
                }
                curObj.GetComponent<Transform>().position = placement;
                curObj.transform.SetParent(emptyGO.GetComponent<Transform>());
            }
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
                map.maxsizex = map.tilesizex;
                map.maxsizey = map.tilesizey;
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
