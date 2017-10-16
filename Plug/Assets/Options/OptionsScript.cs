using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class OptionsScript : MonoBehaviour {

    public Canvas ProfileViewCanvas;
    public Canvas GraphViewCanvas;
    public Button ProfileTab;
    public Button GraphTab;
    public Button SaveButton;
    public Button ApplyButton;
    public Button ResetButton;
    public Button ResetProfButton;
    public Button DefaultButton;
    public Profiles PlayerProf;
    private Profiles Origin;
    
    void Start ()
    {
        PlayerProf = GameObject.Find("OptionsController").GetComponent<Profiles>();

        Button temp = ProfileTab.GetComponent<Button>();
        temp.onClick.AddListener(ProfileClick);
        temp = GraphTab.GetComponent<Button>();
        temp.onClick.AddListener(GraphClick);
        temp = SaveButton.GetComponent<Button>();
        temp.onClick.AddListener(SaveClick);
        temp = ApplyButton.GetComponent<Button>();
        temp.onClick.AddListener(ApplyClick);
        temp = ResetButton.GetComponent<Button>();
        temp.onClick.AddListener(ResetClick);
        temp = ResetProfButton.GetComponent<Button>();
        temp.onClick.AddListener(ResetProfClick);
        temp = DefaultButton.GetComponent<Button>();
        temp.onClick.AddListener(DefaultClick);
        
        ProfileViewCanvas.enabled = true;
        GraphViewCanvas.enabled = true;
    }
	
	void Update () {

    }

    void ProfileClick()
    {
        ProfileViewCanvas.enabled = true;
        GraphViewCanvas.enabled = true;
    }
    void GraphClick()
    {
        ProfileViewCanvas.enabled = true;
        GraphViewCanvas.enabled = true;
    }

    void SaveClick()
    {
        ApplyClick();
        PlayerProf.SaveToProfile("Profile.prof");
    }
    void ApplyClick()
    {
        PlayerProf.resolution = GameObject.Find("LabelReso").GetComponent<Text>().text;
        GameObject.Find("Resolution").GetComponent<Dropdown>().options[0].text = PlayerProf.resolution;

        string[] res = PlayerProf.resolution.Split('x');
        bool fullscreen = false;
        if (PlayerProf.fullscreen > 0)
            fullscreen = true; 
        Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), fullscreen);

        PlayerProf.lang = GameObject.Find("LabelLang").GetComponent<Text>().text;
        GameObject.Find("Language").GetComponent<Dropdown>().options[0].text = PlayerProf.lang;
        float temp = GameObject.Find("Volumeall").GetComponent<Slider>().value * 100.0f;
        PlayerProf.volumeall = (int) temp;
        temp = GameObject.Find("Volumesfx").GetComponent<Slider>().value * 100;
        PlayerProf.volumesfx = (int)temp;
        temp = GameObject.Find("Volumemus").GetComponent<Slider>().value * 100;
        PlayerProf.volumemus = (int)temp;
        temp = GameObject.Find("Brightness").GetComponent<Scrollbar>().value * 100;
        PlayerProf.brightness = (int) temp;
        PlayerProf.profilename = GameObject.Find("NameText").GetComponent<Text>().text;
    }
    void ResetClick()
    {
        GameObject.Find("Resolution").GetComponent<Dropdown>().options[0].text = PlayerProf.resolution;
        GameObject.Find("LabelReso").GetComponent<Text>().text = PlayerProf.resolution;
        GameObject.Find("Resolution").GetComponent<Dropdown>().value = 0;
        GameObject.Find("Language").GetComponent<Dropdown>().options[0].text = PlayerProf.lang;
        GameObject.Find("LabelLang").GetComponent<Text>().text = PlayerProf.lang;
        GameObject.Find("Language").GetComponent<Dropdown>().value = 0;
        GameObject.Find("Volumeall").GetComponent<Slider>().value = PlayerProf.volumeall / 100.0f;
        GameObject.Find("Volumesfx").GetComponent<Slider>().value = PlayerProf.volumesfx / 100.0f;
        GameObject.Find("Volumemus").GetComponent<Slider>().value = PlayerProf.volumemus / 100.0f;
        GameObject.Find("Brightness").GetComponent<Scrollbar>().value = PlayerProf.brightness / 100.0f;
        GameObject.Find("InputField").GetComponent<InputField>().text = PlayerProf.profilename;
    }
    void ResetProfClick()
    {
        Origin = gameObject.AddComponent<Profiles>();
        Origin.Load("Profile.prof");
        GameObject.Find("Resolution").GetComponent<Dropdown>().options[0].text = Origin.resolution;
        GameObject.Find("LabelReso").GetComponent<Text>().text = Origin.resolution;
        GameObject.Find("Resolution").GetComponent<Dropdown>().value = 0;
        GameObject.Find("Language").GetComponent<Dropdown>().options[0].text = Origin.lang;
        GameObject.Find("LabelLang").GetComponent<Text>().text = Origin.lang;
        GameObject.Find("Language").GetComponent<Dropdown>().value = 0;
        GameObject.Find("Volumeall").GetComponent<Slider>().value = Origin.volumeall / 100.0f;
        GameObject.Find("Volumesfx").GetComponent<Slider>().value = Origin.volumesfx / 100.0f;
        GameObject.Find("Volumemus").GetComponent<Slider>().value = Origin.volumemus / 100.0f;
        GameObject.Find("Brightness").GetComponent<Scrollbar>().value = Origin.brightness / 100.0f;
        GameObject.Find("InputField").GetComponent<InputField>().text = Origin.profilename;
        //GameObject.Find("Windowed").GetComponent<Toggle>().isOn = true;
        //GameObject.Find("Fullscreen").GetComponent<Toggle>().isOn = false;
    }
    void DefaultClick()
    {
        GameObject.Find("Resolution").GetComponent<Dropdown>().options[0].text = "1920x1080";
        GameObject.Find("LabelReso").GetComponent<Text>().text = "1920x1080";
        GameObject.Find("Resolution").GetComponent<Dropdown>().value = 0;
        GameObject.Find("Language").GetComponent<Dropdown>().options[0].text = "English";
        GameObject.Find("LabelLang").GetComponent<Text>().text = "English";
        GameObject.Find("Language").GetComponent<Dropdown>().value = 0;
        GameObject.Find("Volumeall").GetComponent<Slider>().value = 50 / 100.0f;
        GameObject.Find("Volumesfx").GetComponent<Slider>().value = 50 / 100.0f;
        GameObject.Find("Volumemus").GetComponent<Slider>().value = 50 / 100.0f;
        GameObject.Find("Brightness").GetComponent<Scrollbar>().value = 50 / 100.0f;
        GameObject.Find("InputField").GetComponent<InputField>().text = "Default";
        GameObject.Find("Windowed").GetComponent<Toggle>().isOn = true;
        GameObject.Find("Fullscreen").GetComponent<Toggle>().isOn = false;
    }
}
