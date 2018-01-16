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
    Transform Cursor_Pos;

    void Start () {
        Ppos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Cpos = gameObject.GetComponent<Transform>();
        Cpos.position = new Vector3(Cpos.position.x, init_zoom, Cpos.position.z);
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
        Cpos.position = Vector3.MoveTowards(new Vector3(Cpos.position.x, Cpos.position.y, Cpos.position.z), new Vector3(Cpos.position.x, init_zoom, Cpos.position.z), speedzoom * Time.deltaTime);
    }

    public void Dezoom()
    {
        Cpos.position = Vector3.MoveTowards(new Vector3(Cpos.position.x, Cpos.position.y, Cpos.position.z), new Vector3(Cpos.position.x, max_zoom, Cpos.position.z), speeddezoom * Time.deltaTime);
    }

    public void MoveToPlayer()
    {
        Cpos.position = new Vector3(Ppos.position.x, Cpos.position.y, Ppos.position.z);
    }
}
