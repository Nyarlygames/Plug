using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ObjectGO : MonoBehaviour {

    public ObjectSave objectCur;
    GameManager GM;
    GameObject canvasinfo;
    public EventSave es;
    // Use this for initialization
    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.tag == "trigger")
        {
            foreach (GameObject radius in GameObject.FindGameObjectsWithTag("radius"))
            {
                if ((radius.GetComponent<Transform>().position.x + radius.GetComponent<CircleCollider2D>().radius > gameObject.GetComponent<BoxCollider2D>().bounds.center.x - gameObject.GetComponent<BoxCollider2D>().bounds.extents.x) &&
                    (radius.GetComponent<Transform>().position.x - radius.GetComponent<CircleCollider2D>().radius < gameObject.GetComponent<BoxCollider2D>().bounds.center.x + gameObject.GetComponent<BoxCollider2D>().bounds.extents.x) &&
                    (radius.GetComponent<Transform>().position.y + radius.GetComponent<CircleCollider2D>().radius > gameObject.GetComponent<BoxCollider2D>().bounds.center.y - gameObject.GetComponent<BoxCollider2D>().bounds.extents.y) &&
                    (radius.GetComponent<Transform>().position.y - radius.GetComponent<CircleCollider2D>().radius < gameObject.GetComponent<BoxCollider2D>().bounds.center.y + gameObject.GetComponent<BoxCollider2D>().bounds.extents.y))
                {
                    if (GM.EM.events.Exists(e => e.obj == objectCur) == false)
                    {
                        es = new EventSave();
                        es.obj = objectCur;
                        es.id = GM.EM.events.Count;
                        GM.EM.events.Add(es);
                    }
                    else
                    {
                        es = GM.EM.events.Find(e => e.obj.id == objectCur.id);
                    }
                }
                else
                {
                    GM.EM.events.RemoveAll(e => e.obj.id == objectCur.id);
                }
            }
            if (objectCur.modifiers.ContainsKey("capacity") && (objectCur.modifiers["capacity"] == "0"))
            {
                // TO DESTROY, better replace with lower graph, so as to regen later !
               /* if (canvasinfo != null)
                    Destroy(canvasinfo);
                Destroy(gameObject);*/
            }
        }
    }
    void OnMouseDown()
    {

    }
    void OnMouseOver()
    {

        if (gameObject.tag == "trigger")
        {
            if ((canvasinfo == null) && (GameObject.Find("Trigger_Info") == null) && (GM.UIChar.activeSelf == false) && (GM.UIEscape.activeSelf == false))
            {
                canvasinfo = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_TriggerInfo"), Vector3.zero, Quaternion.identity);
                canvasinfo.name = "Trigger_Info";
              //  GameObject cam = GameObject.Find("Camera");
                GameObject UI = GameObject.Find("UI_Panel");
                canvasinfo.GetComponent<Transform>().position = Camera.main.GetComponent<Camera>().WorldToScreenPoint(gameObject.GetComponent<Transform>().position);
                canvasinfo.GetComponent<Transform>().SetParent(UI.GetComponent<Transform>());
                canvasinfo.GetComponent<PanelTriggerInfo>().obj = objectCur;
                canvasinfo.GetComponent<PanelTriggerInfo>().objGO = gameObject;
            }
        }
    }
}
