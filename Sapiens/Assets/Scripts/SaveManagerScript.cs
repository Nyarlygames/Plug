using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveManagerScript {
    
    void Start()
    {

    }
    
    void Update()
    {

    }

    public PlayerScript LoadPlayer(string filepath)
    {
        if (File.Exists(filepath)){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filepath, FileMode.Open);
            PlayerScript Player = (PlayerScript)bf.Deserialize(file);
            file.Close();
            return (Player);
        }
        return (null );

    }

    public void SavePlayer(PlayerGo playergo, PlayerScript player) {

        BinaryFormatter bf = new BinaryFormatter();
        player.time += playergo.time;
        FileStream file = File.Open("C:/Users/Nyarly/Desktop/Sapiens/Player" + player.id + ".dat", FileMode.OpenOrCreate);
        bf.Serialize(file, player);
        file.Close();

    }
}
