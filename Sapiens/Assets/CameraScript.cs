using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    
    public float init_zoom = 0;
    public float max_zoom = 0;
    public float current_zoom = 0;
    public float speeddezoom = 0;
    public float speedzoom = 0;
    Camera cam;

    void Start () {
        cam = gameObject.GetComponent<Camera>();
        cam.orthographicSize = init_zoom;
        current_zoom = init_zoom;
    }
	
	void Update ()
    {
    }

    private void FixedUpdate()
    {
    }

    public void Zoom()
    {
        // move camera down
        if (current_zoom - (speedzoom * Time.deltaTime) >= init_zoom)
        {
            cam.orthographicSize = current_zoom - speedzoom * Time.deltaTime;
            current_zoom = cam.orthographicSize;
        }
        else
        {
            current_zoom = init_zoom;
        }
    }

    public void Dezoom()
    {
        // move camera up
        if (current_zoom + (speeddezoom * Time.deltaTime) <= max_zoom)
        {
            cam.orthographicSize = current_zoom + speeddezoom * Time.deltaTime;
            current_zoom = cam.orthographicSize;
        }
        else
        {
            current_zoom = max_zoom;
        }
    }
}
