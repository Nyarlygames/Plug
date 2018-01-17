using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Transform Ppos;
    public Transform Cpos;
    public float init_zoom = 0;
    public float max_zoom = 0;
    public float current_zoom = 0;
    public float speeddezoom = 0;
    public float speedzoom = 0;
    Camera cam;
    Transform Cursor_Pos;

    void Start () {
        Ppos = GameObject.FindGameObjectWithTag("Tribe").GetComponent<Transform>();
        cam = gameObject.GetComponent<Camera>();
        cam.orthographicSize = init_zoom;
        Cpos = gameObject.GetComponent<Transform>();
        current_zoom = init_zoom;
        Cursor_Pos = GameObject.Find("Cursor").GetComponent<Transform>();
    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Dezoom();
        }
        else
        {
            Zoom();
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Cursor_Pos.position = hit.point;
        }
    }

    public void Zoom()
    {
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

    public void MoveToTribe()
    {
        Cpos.position = new Vector3(Ppos.position.x, Cpos.position.y, Ppos.position.z);
    }
}
