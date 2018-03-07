﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGO : MonoBehaviour
{

    public TileSave tileCur;
    SpriteRenderer tilerend;
    CircleCollider2D triberadius;
    BoxCollider2D tilecol;
    Shader defaultShad;
    Color defaultColor;
    Color defaultColorVisited;

    void Start()
    {
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
        }
    }

    public void SetVisited(int visit)
    {
        if (visit == 1)
        {
            tilerend.material.shader = defaultShad;
            tilerend.color = defaultColor;
        }
        else if (visit == 2)
        {
            tilerend.material.shader = defaultShad;
            tilerend.color = defaultColorVisited;
        }
    }
    
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tilerend.isVisible)
        {
            if (collision.name == "Tribe_Radius")
                SetVisited(1);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (tilerend.isVisible)
        {
            if (collision.name == "Tribe_Radius")
                SetVisited(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tilerend.isVisible)
        {
            if (collision.name == "Tribe_Radius")
                SetVisited(2);
        }
    }
}
