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
    public SpriteRenderer objectrend;
    Shader defaultShad;
    Color defaultColor;
    Color defaultColorVisited;
    Shader defaultShadRadius;
    Color defaultColorRadius;
    GameObject trigger_radius;
    public GameObject trigger_type;
    
    public void InitObj ()
    {
        enabled = false;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        objectrend = gameObject.GetComponent<SpriteRenderer>();
        
        defaultShad = objectrend.material.shader;
        defaultColor = objectrend.material.color;
        defaultColorVisited = new Color(defaultColor.r - 0.2f, defaultColor.g - 0.2f, defaultColor.b - 0.2f, defaultColor.a);
        if (objectCur.visitState == 0)
        {
            Shader shaderGUItext = Shader.Find("GUI/Text Shader");
            objectrend.material.shader = shaderGUItext;
            objectrend.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }






        /*else if (objectCur.visitState == 1)
        {
            if (GM.visitedTrig.Contains(this) == false)
                GM.visitedTrig.Add(this);
            objectrend.material.shader = defaultShad;
            objectrend.color = defaultColor;

        }
        else if (objectCur.visitState == 2)
        {
            objectrend.material.shader = defaultShad;
            objectrend.color = defaultColorVisited;
        }*/
    }

    public void addTrigger(GameObject tilego, Sprite Radius, List<ObjectSave> objectssave)
    {
        RatioFactory RF = new RatioFactory();
        Vector2 Triggersize = new Vector2(RF.trigger_radiusx, RF.trigger_radiusy);
        tilego.AddComponent<BoxCollider2D>();
        tilego.GetComponent<BoxCollider2D>().size = Triggersize;
        tilego.GetComponent<BoxCollider2D>().isTrigger = true;
        tilego.tag = "trigger";

        trigger_type = new GameObject(tilego.name + "_type");
        SpriteRenderer type = trigger_type.AddComponent<SpriteRenderer>();
        if (objectCur.modifiers.ContainsKey("resource_type"))
        {
            type.sprite = GM.resource_types[objectCur.modifiers["resource_type"]];
        }
        trigger_type.GetComponent<Transform>().position = tilego.GetComponent<Transform>().position + new Vector3(0,0,-1.0f);
        trigger_type.GetComponent<Transform>().SetParent(tilego.GetComponent<Transform>());

        trigger_radius = new GameObject(tilego.name + "_radius");
        trigger_radius.AddComponent<SpriteRenderer>();
        trigger_radius.GetComponent<SpriteRenderer>().sprite = Radius;
        trigger_radius.GetComponent<Transform>().localScale = Triggersize/5;
        trigger_radius.GetComponent<Transform>().SetParent(trigger_type.GetComponent<Transform>());
        trigger_type.SetActive(false);
        if ((objectCur.modifiers.ContainsKey("capacity_max")) && (objectCur.modifiers.ContainsKey("capacity_min")) && (objectssave.Count == 0))
        {
            if (Convert.ToInt32(objectCur.modifiers["capacity_max"]) == -1)
            {
                if (Convert.ToInt32(objectCur.modifiers["capacity_min"]) == -1)
                {
                    objectCur.modifiers["capacity"] = "99999";
                }
                else
                {
                    objectCur.modifiers["capacity"] = UnityEngine.Random.Range(Convert.ToInt32(objectCur.modifiers["capacity_min"]), 99999).ToString();
                }
            }
            else
            {
                objectCur.modifiers["capacity"] = UnityEngine.Random.Range(Convert.ToInt32(objectCur.modifiers["capacity_min"]), Convert.ToInt32(objectCur.modifiers["capacity_max"]) + 1).ToString();
            }
        }
        else if ((objectssave != null) && (objectssave.Find(os => os.id == objectCur.id).modifiers.ContainsKey("capacity") == true))
        {
            objectCur.modifiers["capacity"] = objectssave.Find(os => os.id == objectCur.id).modifiers["capacity"];
        }
    }

    void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate () {


        // TO REMOVE WITH OnEnter and OnExit ?
       /* if ((gameObject.tag == "trigger") && (objectrend.isVisible == true))
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
               // if (canvasinfo != null)
                //    Destroy(canvasinfo);
               // Destroy(gameObject);
            }
        }*/
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

    private void OnMouseExit()
    {
        Destroy(canvasinfo);
    }

    public void SetVisited(int visit)
    {
        if (visit == 1)
        {
            if (GM.visitedTrig.Contains(this) == false)
                GM.visitedTrig.Add(this);
            objectCur.visitState = 1;
            objectrend.material.shader = defaultShad;
            objectrend.color = defaultColor;

        }
        else if (visit == 2)
        {
            if (GM.visitedTrig.Contains(this) == false)
                GM.visitedTrig.Add(this);
            objectCur.visitState = 2;
            objectrend.material.shader = defaultShad;
            objectrend.color = defaultColorVisited;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((objectrend.isVisible) && (collision.name == "Tribe_Radius") && (objectCur.visitState != 1))
        {
            SetVisited(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((objectrend.isVisible) && (collision.name == "Tribe_Radius") && (objectCur.visitState != 2))
        {
            SetVisited(2);
        }
    }*/
}
