using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGO : MonoBehaviour {

    GameManager GM;
    Transform tribePos;
    Transform camPos;
    Camera thiscam;
    GameObject tribemem;

    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        camPos = gameObject.GetComponent<Transform>();
        thiscam = gameObject.GetComponent<Camera>();
        tribemem = GameObject.Find("Tribe_Members");
    }
	
	void Update ()
    {
        Vector3 temp = Vector3.zero;
        if (tribemem == null) {
            tribemem = GameObject.Find("Tribe_Members");
            tribePos = tribemem.GetComponent<Transform>();
        }
        if (((tribePos.position.x < (GM.map.tilesizex * GM.map.sizex) /2 - thiscam.orthographicSize*2) && (tribePos.position.x > thiscam.orthographicSize*2))
            && ((tribePos.position.y < (GM.map.tilesizey * GM.map.sizey) /2 - thiscam.orthographicSize) && (tribePos.position.y > thiscam.orthographicSize))) {
            temp = new Vector3(tribePos.position.x, tribePos.position.y, -thiscam.orthographicSize);
        }
        /* TODO : add * GM.map.sizex and sizey everywhere)*/
        else if (tribePos.position.x >= GM.map.tilesizex * GM.map.sizex / 2 - thiscam.orthographicSize * 2)
        {
            if (tribePos.position.y >= GM.map.tilesizey * GM.map.sizey / 2 - thiscam.orthographicSize)
                temp = new Vector3(GM.map.tilesizex * GM.map.sizex / 2 - thiscam.orthographicSize * 2, GM.map.tilesizey * GM.map.sizey / 2 - thiscam.orthographicSize, -thiscam.orthographicSize);
            else if (tribePos.position.y <= thiscam.orthographicSize)
                temp = new Vector3(GM.map.tilesizex * GM.map.sizex / 2 - thiscam.orthographicSize * 2, thiscam.orthographicSize, -thiscam.orthographicSize);
            else
                temp = new Vector3(GM.map.tilesizex / 2 - thiscam.orthographicSize * 2, tribePos.position.y, -thiscam.orthographicSize);
        }
        else if (tribePos.position.x <= thiscam.orthographicSize * 2)
        {
            if (tribePos.position.y >= GM.map.tilesizey * GM.map.sizey / 2 - thiscam.orthographicSize)
                temp = new Vector3(thiscam.orthographicSize * 2, GM.map.tilesizey * GM.map.sizey / 2 - thiscam.orthographicSize, -thiscam.orthographicSize);
            else if (tribePos.position.y <= thiscam.orthographicSize)
                temp = new Vector3(thiscam.orthographicSize * 2, thiscam.orthographicSize, -thiscam.orthographicSize);
            else
                temp = new Vector3(thiscam.orthographicSize * 2, tribePos.position.y, -thiscam.orthographicSize);
        }
        else if (tribePos.position.y >= GM.map.tilesizey * GM.map.sizey / 2 - thiscam.orthographicSize)
        {
            if (tribePos.position.x >= GM.map.tilesizex * GM.map.sizex / 2 - thiscam.orthographicSize * 2)
                temp = new Vector3(GM.map.tilesizex * GM.map.sizex / 2 - thiscam.orthographicSize * 2, GM.map.tilesizey * GM.map.sizey / 2 - thiscam.orthographicSize, -thiscam.orthographicSize);
            else if (tribePos.position.x <= thiscam.orthographicSize * 2)
                temp = new Vector3(thiscam.orthographicSize * 2, GM.map.tilesizey * GM.map.sizey / 2 - thiscam.orthographicSize, -thiscam.orthographicSize);
            else
                temp = new Vector3(tribePos.position.x, GM.map.tilesizey * GM.map.sizey / 2 - thiscam.orthographicSize, -thiscam.orthographicSize);
        }
        else if (tribePos.position.y <= thiscam.orthographicSize)
        {
            if (tribePos.position.x >= GM.map.tilesizex * GM.map.sizex / 2 - thiscam.orthographicSize * 2)
                temp = new Vector3(GM.map.tilesizex * GM.map.sizex / 2 - thiscam.orthographicSize * 2, thiscam.orthographicSize, -thiscam.orthographicSize);
            else if (tribePos.position.x <= thiscam.orthographicSize * 2)
                temp = new Vector3(thiscam.orthographicSize * 2, thiscam.orthographicSize, -thiscam.orthographicSize);
            else
                temp = new Vector3(tribePos.position.x, thiscam.orthographicSize, -thiscam.orthographicSize);
        }
        camPos.position = temp;
    }
}
