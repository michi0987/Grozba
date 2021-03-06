﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoriesManager : MonoBehaviour
{
    private List<Territory> territories = new List<Territory>();
    public List<Territory> Territories { get => territories; set { } }

    public Territory ActiveTerritory;


    public Color hoverColor = new Color(0, 0, 0, 255);
    public Color attackColor = new Color(200, 20, 140, 255);
    public Color attackingColor = new Color(20, 200, 120, 200);
   
    public void SetTerritories()
    {
        foreach(Transform child in transform)
        {
            territories.Add(child.GetComponent<Territory>());
            child.GetComponent<Territory>().Manager = this;
        }
    }
    
    public Territory GetActiveTerritory()
    {
        Territory active = ActiveTerritory;
        ActiveTerritory = null;
        return active;
    }
}
