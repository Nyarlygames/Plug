using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGO : MonoBehaviour
{

    public TileSave tileCur;
    public SpriteRenderer tilerend;
    BoxCollider2D tilecol;
    Shader defaultShad;
    Color defaultColor;
    Color defaultColorVisited;

    void Start()
    {
        enabled = false;
        tilerend = gameObject.GetComponent<SpriteRenderer>();
        defaultShad = tilerend.material.shader;
        defaultColor = tilerend.material.color;
        defaultColorVisited = new Color(defaultColor.r-0.2f, defaultColor.g-0.2f, defaultColor.b-0.2f,defaultColor.a);
        tilecol = gameObject.GetComponent<BoxCollider2D>();
        CircleCollider2D triberadius = GameObject.Find("Tribe_Radius").GetComponent<CircleCollider2D>();
        if (tileCur.visitState == 0)
        {
            Shader shaderGUItext = Shader.Find("GUI/Text Shader");
            tilerend.material.shader = shaderGUItext;
            tilerend.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            foreach (Transform child in transform)
            {
                SpriteRenderer childrend = child.GetComponent<SpriteRenderer>();
                childrend.material.shader = shaderGUItext;
                childrend.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
    }

    public void SetVisited(int visit)
    {
        if (visit == 1)
        {
            tilerend.material.shader = defaultShad;
            tilerend.color = defaultColor;
            foreach (Transform child in transform)
            {
                SpriteRenderer childrend = child.GetComponent<SpriteRenderer>();
                childrend.material.shader = defaultShad;
                childrend.color = defaultColor;
            }
        }
        else if (visit == 2)
        {
            tilerend.material.shader = defaultShad;
            tilerend.color = defaultColorVisited;
            foreach (Transform child in transform)
            {
                SpriteRenderer childrend = child.GetComponent<SpriteRenderer>();
                childrend.material.shader = defaultShad;
                childrend.color = defaultColorVisited;
            }
        }
    }
    
    void FixedUpdate()
    {
        
    }

    void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((tilerend.isVisible) && (collision.name == "Tribe_Radius") && (tileCur.visitState != 1))
        {
            SetVisited(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((tilerend.isVisible) && (collision.name == "Tribe_Radius") && (tileCur.visitState != 2))
        {
                SetVisited(2);
        }
    }*/
}
