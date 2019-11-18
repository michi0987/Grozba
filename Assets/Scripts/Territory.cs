using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class Territory : MonoBehaviour
{
    public Territory[] Neighoburs;
    public Player Owner;
    public TerritoriesManager Manager;

    public Color standardColor;


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
