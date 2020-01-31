using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class Territory : MonoBehaviour
{
    public Territory[] Neighoburs;
    public Player Owner;
    public TerritoriesManager Manager;
    public ResourceTagController resourceTagController;
    public int Resources = 1;
    /*
    public Territory()
    {
        this.resources = 0;
    }
    */
    public int resources { get => Resources; 
        set {         
            resourceTagController.setNumber(value);
            Resources = value;
        } 
    }
    
    public bool attack;

    public Color standardColor;
    private void Awake()
    {
        resourceTagController = this.gameObject.GetComponentInChildren<ResourceTagController>();
    }

    private void Start()
    {
        this.attack = false;
        this.resources = 1;
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
