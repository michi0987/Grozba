using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoriesManager : MonoBehaviour
{
    private List<Territory> territories = new List<Territory>();
    public List<Territory> Territories { get => territories; set { } }

    public bool ShowOwners = false;
   

    public void SetTerritories()
    {
        foreach(Transform child in transform)
        {
            territories.Add(child.GetComponent<Territory>());
        }
    }
}
