using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    Profiles PlayerProf;

	void Start () {

        PlayerProf = gameObject.AddComponent<Profiles>();
        List<string> valuesres = new List<string>();
        List<string> valueslang = new List<string>();
        valueslang.Add(PlayerProf.lang);
        valueslang.Add("English");
        valueslang.Add("French");
        valueslang.Add("Italian");
        valueslang.Add("German");
        valueslang.Add("Spanish");
        valuesres.Add(PlayerProf.resolution);
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            valuesres.Add(res.width + "x" +  res.height);
        }
        GameObject.Find("Resolution").GetComponent<Dropdown>().ClearOptions();
        GameObject.Find("Resolution").GetComponent<Dropdown>().AddOptions(valuesres);
        GameObject.Find("Language").GetComponent<Dropdown>().ClearOptions();
        GameObject.Find("Language").GetComponent<Dropdown>().AddOptions(valueslang);
        GameObject.Find("Volumeall").GetComponent<Slider>().value = PlayerProf.volumeall / 100.0f;
        GameObject.Find("Volumesfx").GetComponent<Slider>().value = PlayerProf.volumesfx / 100.0f;
        GameObject.Find("Volumemus").GetComponent<Slider>().value = PlayerProf.volumemus / 100.0f;
        GameObject.Find("Brightness").GetComponent<Scrollbar>().value = PlayerProf.brightness / 100.0f;
        GameObject.Find("InputField").GetComponent<InputField>().text = PlayerProf.profilename;
    }
	
	void Update () {
		
	}
}
