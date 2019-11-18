using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayersManager PlayersManager;
    public TerritoriesManager TerritoriesManager;

    public bool randomAssignTerritories;

    void Awake()
    {
        PlayersManager = gameObject.transform.Find("Players").GetComponent<PlayersManager>();
        TerritoriesManager = gameObject.transform.Find("Map").GetComponent<TerritoriesManager>();

        TerritoriesManager.SetTerritories();

        //to listy są na razie tylko do testów, potem z interfejsu powinny być dodawane tutaj
        List<string> names = new List<string>();
        List<Color> colors = new List<Color>();
        names.Add("Player 1");
        names.Add("Player 2");
        names.Add("Player 3");
        names.Add("Player 4");
        colors.Add(Color.blue);
        colors.Add(Color.green);
        colors.Add(Color.red);
        colors.Add(Color.yellow);
        //

        PlayersManager.CreatePlayers(4, names, colors);
    }

    private void Start()
    {
        if (randomAssignTerritories)
        {
            RandomAssignTerritories();
        }
        else
        {
            StartCoroutine(ManuallyAssignTerritories());
        }
    }



    private void RandomAssignTerritories()
    {
        List<int> used = new List<int>();

        for (int i = 0; i < TerritoriesManager.Territories.Count; i++)
        {
            used.Add(i);
        }

        while (used.Count != 0)
        {
            foreach (Player player in PlayersManager.Players)
            {
                if (used.Count != 0)
                {
                    int rand = used[Random.Range(0, used.Count)];
                    used.Remove(rand);
                    player.AddTerritory(TerritoriesManager.Territories[rand]);
                }
            }
        }
        
        foreach(Transform child in TerritoriesManager.transform)
        {
            child.GetComponent<SpriteRenderer>().color = child.GetComponent<Territory>().standardColor;
        }
    }

    private IEnumerator ManuallyAssignTerritories()
    {
        Player currentPlayer;
        for(int i = 0; i < TerritoriesManager.Territories.Count; i++)
        {
            currentPlayer = PlayersManager.Players[i % PlayersManager.Players.Count];

            Territory currentTerritory;
            do
            {
                currentTerritory = TerritoriesManager.GetActiveTerritory();
                yield return null;
            }
            while (currentTerritory == null || currentTerritory.Owner != null);

            Debug.Log(currentTerritory.gameObject.name + " has been assigned to " + currentPlayer.gameObject.name + "!");
            currentPlayer.AddTerritory(currentTerritory);
        }
    }
}
