using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Territory> Territories = new List<Territory>();
    public Color Color;
    public int freeResources;
    public int numberOfTerritories;

    public Player()
    {
        this.freeResources = 0;
    }

    public void AddTerritory(Territory territory)
    {
        
        territory.Owner = this;
        territory.standardColor = this.Color;
        Territories.Add(territory);
    }

}
