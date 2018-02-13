using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData {

    public string savefolder = "Save/";
    public string savefile = "";
    public string tribefile = "";
    public string optionsfile = "";
    public string eventfile = "";
    public string mapfile = "";
    public string statefile = "";
    public string profilefile = "";

    // public List<CharacterSave> charsave = new List<CharacterSave>();
    public TribeSave tribesave = new TribeSave();
    public MapSave mapsave = new MapSave();
    public List<EventSave> eventsave = new List<EventSave>();

}
