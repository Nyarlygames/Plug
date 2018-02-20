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
                Tribe_Members.GetComponent<Transform>().position = new Vector3(sdata.tribesave.members[0].x, sdata.tribesave.members[0].y, GM.ZCharacters); // load tribe to char1
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
        previous.eventsave = GM.EM.events;
        if(PlayerPrefs.GetString("mapfile") != "")
        {
            previous.mapfile = PlayerPrefs.GetString("mapfile");
        }
        FileStream file = File.Open(previous.savefolder + previous.savefile + "/" + previous.savefile, FileMode.OpenOrCreate);
        bf.Serialize(file, previous);
        file.Close();

    }

    public void LoadMapGO(MapSave mapfile, List<GameObject> TilesGO, List<GameObject> ObjectsGO, List<ObjectSave> objectssave)
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject emptyMap = new GameObject("Map");
        int drawback = 10;
        for (int y = 0; y < mapfile.sizey; y++)
        {
            for (int x = 0; x < mapfile.sizex; x++)
            {
                /* TILES GROUND */
                GameObject tilego = new GameObject("[" + y + "/" + x + "]");
                tilego.AddComponent<TileGO>();
                tilego.tag = "tile";
                TileGO curtile = tilego.GetComponent<TileGO>();
                curtile.tileCur = mapfile.layer.tiles[y][x];
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
                Vector3 placement = Vector3.zero;
                switch (mapfile.orientation)
                {
                    case "orthogonal":
                        placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                        break;
                    case "staggered":
                        if (y % 2 == 1)
                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                        else
                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                        break;
                    case "isometric":
                        if (y % 2 == 1)
                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                        else
                            placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                        break;
                    default:
                        placement = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                        break;
                }
                tilego.GetComponent<Transform>().position = placement;
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
                                placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                break;
                            case "staggered":
                                if (y % 2 == 1)
                                    placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                else
                                    placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                break;
                            case "isometric":
                                if (y % 2 == 1)
                                    placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                else
                                    placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f + (mapfile.tilesizex / 2.0f / 100.0f), (y * mapfile.tilesizey / 2.0f + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
                                break;
                            default:
                                placementtrigger = new Vector3((x * mapfile.tilesizex + mapfile.tilesizex / 2.0f) / 100.0f, (y * mapfile.tilesizey + mapfile.tilesizey / 2.0f) / 100.0f, GM.ZGround + drawback);
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
        GameObject emptyGO = new GameObject("Objects");
        Sprite Radius = Resources.Load<Sprite>("Play/radius");
        foreach (ObjectSave obj in mapfile.objects)
        {
            GameObject tilego = new GameObject("[" + obj.x + "/" + obj.y + "]" + obj.id);
            tilego.tag = "object";
            tilego.AddComponent<ObjectGO>();
            ObjectGO curObj = tilego.GetComponent<ObjectGO>();
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
            Vector3 placement = Vector3.zero;
            switch (mapfile.orientation)
            {
                case "orthogonal":
                    placement = new Vector3((curObj.objectCur.x + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f, GM.ZObjects);
                    break;
                case "staggered":
                    if (curObj.objectCur.y % 2 == 1)
                        placement = new Vector3((curObj.objectCur.x +curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey/2) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f, GM.ZObjects);
                    else
                        placement = new Vector3((curObj.objectCur.x + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey / 2) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f + (mapfile.tilesizey / 2.0f / 100.0f), GM.ZObjects);
                    break;
                case "isometric":
                    placement = new Vector3((curObj.objectCur.x + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey/2) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f, GM.ZObjects);
                    break;
                default:
                    placement = new Vector3((curObj.objectCur.x + curObj.objectCur.offsetx + curObj.objectCur.width / 2) / 100.0f, ((mapfile.sizey * mapfile.tilesizey) - ((curObj.objectCur.y + curObj.objectCur.offsety - curObj.objectCur.height / 2.0f))) / 100.0f, GM.ZObjects);
                    break;
            }
            if (curObj.objectCur.modifiers.ContainsValue("trigger"))
            {
                Vector2 S = tilego.GetComponent<SpriteRenderer>().sprite.bounds.size;
                tilego.AddComponent<BoxCollider2D>();
                tilego.GetComponent<BoxCollider2D>().size = S;
                tilego.GetComponent<BoxCollider2D>().isTrigger = true;
                GameObject trigger_radius = new GameObject(tilego.name + "_radius");
                trigger_radius.AddComponent<SpriteRenderer>();
                trigger_radius.GetComponent<SpriteRenderer>().sprite = Radius;
                trigger_radius.GetComponent<Transform>().localScale = tilego.GetComponent<SpriteRenderer>().bounds.extents / 2;
                trigger_radius.GetComponent<Transform>().SetParent(tilego.GetComponent<Transform>());
                tilego.tag = "trigger";

                if ((curObj.objectCur.modifiers.ContainsKey("capacity_max")) && (curObj.objectCur.modifiers.ContainsKey("capacity_min")) && (objectssave.Count == 0))
                {
                    if (Convert.ToInt32(curObj.objectCur.modifiers["capacity_max"]) == -1)
                    {
                        if (Convert.ToInt32(curObj.objectCur.modifiers["capacity_min"]) == -1)
                        {
                            curObj.objectCur.modifiers["capacity"] = "99999";
                        }
                        else
                        {
                            curObj.objectCur.modifiers["capacity"] = UnityEngine.Random.Range(Convert.ToInt32(curObj.objectCur.modifiers["capacity_min"]), 99999).ToString();
                        }
                    }
                    else
                    {
                        curObj.objectCur.modifiers["capacity"] = UnityEngine.Random.Range(Convert.ToInt32(curObj.objectCur.modifiers["capacity_min"]), Convert.ToInt32(curObj.objectCur.modifiers["capacity_max"]) + 1).ToString();
                    }
                }
                else if ((objectssave != null) && (objectssave.Find(os => os.id == curObj.objectCur.id).modifiers.ContainsKey("capacity") == true))
                {
                    curObj.objectCur.modifiers["capacity"] = objectssave.Find(os => os.id == curObj.objectCur.id).modifiers["capacity"];
                }
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
                            }
                            else if (ground.name == "AddedGround")
                            {
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
                            }*/
                            ground.tiles[j].Add(tilesave);
                            tilesave.posx = i;
                            tilesave.posy = j;
                        }
                        newid = reader.ReadLine();
                    }
                    if (ground.name == "Ground")
                        ground.tiles.Reverse();
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
        reader.Close();
    }
}
