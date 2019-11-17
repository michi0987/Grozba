﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class Territory : MonoBehaviour
{
    public Territory[] Neighoburs;
    public Player Owner;

    public Color hoverColor = new Color32(0, 0, 0, 255);
    public Color standardColor;

  
    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = hoverColor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = standardColor;
    }
}
