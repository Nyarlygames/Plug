using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveManagerScript {
    
    public GameObject LoadGO(SaveData sdata)
    {
        GameObject Tribe = new GameObject(sdata.tribesave.tribename);
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
                    newchar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(chara.charSprite);
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
            sdata.savefile = filename;
            sdata.savefolder = "Save/";
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

    public void SaveGame(SaveData previous, GameObject Tribe) {
        
        if (Directory.Exists(previous.savefolder + previous.savefile + "/") == false)
        {
            Directory.CreateDirectory(previous.savefolder + previous.savefile + "/");
        }

        BinaryFormatter bf = new BinaryFormatter();
        previous.tribesave = Tribe.GetComponent<TribeGO>().tribeCurrent;
        FileStream file = File.Open(previous.savefolder + previous.savefile + "/" + previous.savefile, FileMode.OpenOrCreate);
        bf.Serialize(file, previous);
        file.Close(); 
        
    }
}
