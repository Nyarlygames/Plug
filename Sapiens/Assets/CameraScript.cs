﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    
    public float init_zoom = 0;
    public float max_zoom = 0;
    public float current_zoom = 0;
    public float speeddezoom = 0;
    public float speedzoom = 0;
    Camera cam;
    Transform camPos;
    Transform tribePos;

    void Start ()
    {
        cam = gameObject.GetComponent<Camera>();
        camPos = gameObject.GetComponent<Camera>().GetComponent<Transform>();
        tribePos = GameObject.Find("Tribe").GetComponent<Transform>();
        cam.orthographicSize = init_zoom;
        current_zoom = init_zoom;
    }
	
	void Update ()
    {
    }

    void FixedUpdate()
    {
        Vector3 offset = tribePos.position;
        offset.y = current_zoom;
        camPos.position = offset;
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
