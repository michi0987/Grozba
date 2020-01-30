using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class Territory : MonoBehaviour
{
    public Territory[] Neighoburs;
    public Player Owner;
    public TerritoriesManager Manager;
    public int resources;
    public bool attack;

    public Color standardColor;

    public Territory()
    {
        this.attack = false;
        this.resources = 0;
    }

    private void OnMouseDown()
    {
        Manager.ActiveTerritory = this;
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Manager.hoverColor;
    }

    

    private void OnMouseExit()
    {
       
        GetComponent<SpriteRenderer>().color = standardColor;
       
        
    }
}
