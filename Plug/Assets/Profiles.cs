using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Profiles : MonoBehaviour {

    public string profilename = "Default";
    public string lang = "English";
    public int volumemus = 50;
    public int volumesfx = 80;
    public int volumeall = 100;
    public string resolution = "1920x1080";
    public int quality = 0;
    public int difficulty = 0;
    public int brightness = 50;
    public int fullscreen = 0; // 0 windowed - 1 fullscreen - 2 windowed fullscreen
    
    void Start () {
        if (File.Exists("Profile.prof"))
        {
            Load("Profile.prof");
        }
        else
        {
            File.Create("Profile.prof").Dispose();
            string profiletext = "profilename=" + "Default" + "\n" + "lang=" + lang + "\n" + "volumemus=" + volumemus + "\n" + "volumesfx=" + volumesfx + "\n" + "volumeall=" + volumeall + "\n"
                + "resolution=" + resolution + "\n" + "quality=" + quality + "\n" + "difficulty=" + difficulty + "\n" + "brightness=" + brightness + "\n" + "fullscreen=" + fullscreen + "\n";
            File.WriteAllText("Profile.prof", profiletext);
            Load("Profile.prof");
        }
    }
    
        public void SaveToProfile(string filename)
        {
            string profiletext = "profilename=" + profilename + "\n" + "lang=" + lang + "\n" + "volumemus=" + volumemus + "\n" + "volumesfx=" + volumesfx + "\n" + "volumeall=" + volumeall + "\n"
            + "resolution=" + resolution + "\n" + "quality=" + quality + "\n" + "difficulty=" + difficulty + "\n" + "brightness=" + brightness + "\n" + "fullscreen=" + fullscreen + "\n";
            File.WriteAllText(filename, profiletext);
        }
    
        public void Load(string fileName)
    {
        try
        {
            string line;
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);
            using (theReader)
            {
                do
                {
                    line = theReader.ReadLine();

                    if (line != null)
                    {
                        string[] entries = line.Split('=');
                        if (entries.Length > 0)
                        {
                            switch (entries[0])
                            {
                                case "profilename":
                                    profilename = entries[1];
                                    break;
                                case "lang":
                                    lang = entries[1];
                                    break;
                                case "volumemus":
                                    volumemus = int.Parse(entries[1]);
                                    break;
                                case "volumesfx":
                                    volumesfx = int.Parse(entries[1]);
                                    break;
                                case "volumeall":
                                    volumeall = int.Parse(entries[1]);
                                    break;
                                case "resolution":
                                    resolution = entries[1];
                                    break;
                                case "quality":
                                    quality = int.Parse(entries[1]);
                                    break;
                                case "difficulty":
                                    difficulty = int.Parse(entries[1]);
                                    break;
                                case "brightness":
                                    brightness = int.Parse(entries[1]);
                                    break;
                                case "fullscreen":
                                    fullscreen = int.Parse(entries[1]);
                                    break;
                            }
                        }
                    }
                }
                while (line != null);
                theReader.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log("Profile not found : " + e);
        }
        if (string.Compare(SceneManager.GetActiveScene().name, "Options") == 0)
        {
            GameObject.Find("Resolution").GetComponent<Dropdown>().options[0].text = resolution;
            GameObject.Find("LabelReso").GetComponent<Text>().text = resolution;
            GameObject.Find("Resolution").GetComponent<Dropdown>().value = 0;
            GameObject.Find("Language").GetComponent<Dropdown>().options[0].text = lang;
            GameObject.Find("LabelLang").GetComponent<Text>().text = lang;
            GameObject.Find("Language").GetComponent<Dropdown>().value = 0;
            GameObject.Find("Volumeall").GetComponent<Slider>().value = volumeall / 100.0f;
            GameObject.Find("Volumesfx").GetComponent<Slider>().value = volumesfx / 100.0f;
            GameObject.Find("Volumemus").GetComponent<Slider>().value = volumemus / 100.0f;
            GameObject.Find("Brightness").GetComponent<Scrollbar>().value = brightness / 100.0f;
            GameObject.Find("InputField").GetComponent<InputField>().text = profilename;
        }
    }
    
    void Update () {
		
	}
}
