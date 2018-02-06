using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDebug : MonoBehaviour
{
    Slider TimeSlider;
    Text RatioSpeed;
    public float init_speed;
    // Use this for initialization
    void Start ()
    {
        TimeSlider = GameObject.Find("TimeSlider").GetComponent<Slider>();
        RatioSpeed = GameObject.Find("TimeSlider_Info").GetComponent<Text>();
        TimeSlider.onValueChanged.AddListener(delegate {
            ScaleChange(TimeSlider);
        });
        TimeSlider.value = 3.5f;
        Time.timeScale = 3.5f;
        RatioSpeed.text = "time ratio : x" + Time.timeScale;
    }

    public void ScaleChange(Slider scale)
    {
        Time.timeScale = scale.value;
        RatioSpeed.text = "time ratio : x" + Time.timeScale;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
