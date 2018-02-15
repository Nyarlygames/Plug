using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ObjectGO : MonoBehaviour {

    public ObjectSave objectCur;
    GameManager GM;
    GameObject canvasinfo;
    GameObject assignmentinfo;
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
                    EventSave es = new EventSave();
                    es.obj = objectCur;
                    es.id = GM.EM.events.Count;
                    if (GM.EM.events.Exists(e => e.obj.id == objectCur.id) == false)
                    {
                        GM.EM.events.Add(es);
                    }
                }
                else
                    GM.EM.events.RemoveAll(e => e.obj.id == objectCur.id);
            }

           if (canvasinfo != null)
            {
                foreach (Transform child in canvasinfo.GetComponent<Transform>())
                {
                    switch (child.gameObject.name)
                    {
                        case "UI_TriggerName":
                            child.gameObject.GetComponent<Text>().text = "Activity : " + objectCur.modifiers["activity"];
                            break;
                        case "UI_TriggerCapacity":
                            child.gameObject.GetComponent<Text>().text = "Stock : " + objectCur.modifiers["capacity"];
                            break;
                        case "UI_TriggerCurrent":
                            child.gameObject.GetComponent<Text>().text = "Daily gain : " + objectCur.modifiers["extract_daily"]; // do formula with exp on gather depending on chars
                            break;
                        case "UI_TriggerResource":
                            child.gameObject.GetComponent<Text>().text = "Resource type : " + objectCur.modifiers["resource_type"];
                            break;
                        default:
                            break;
                    }
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
            if (canvasinfo == null)
            {
                canvasinfo = Instantiate(Resources.Load<GameObject>("Play/Prefabs/UI_TriggerInfo"), Vector3.zero, Quaternion.identity);
                canvasinfo.name = "Info_" + objectCur.id;
                GameObject cam = GameObject.Find("Camera");
                GameObject UI = GameObject.Find("UI_Panel");
                canvasinfo.GetComponent<Transform>().position = cam.GetComponent<Camera>().WorldToScreenPoint(gameObject.GetComponent<Transform>().position);
                canvasinfo.GetComponent<Transform>().SetParent(UI.GetComponent<Transform>());
            }
        }
    }
    void OnMouseExit()
    {
        Destroy(canvasinfo);
    }
}
