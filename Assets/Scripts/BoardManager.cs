using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public PlayersManager PlayersManager;
    public TerritoriesManager TerritoriesManager;

    private GameManager GM = GameManager.Instance;

    void Awake()
    {
        PlayersManager = gameObject.transform.Find("Players").GetComponent<PlayersManager>();
        TerritoriesManager = gameObject.transform.Find("Map").GetComponent<TerritoriesManager>();

        TerritoriesManager.SetTerritories();

        switch (GM.PlayerCount) {
            case 6:
                GM.playerColors.Add(Color.magenta);
                goto case 5;
            case 5:
                GM.playerColors.Add(Color.cyan);
                goto case 4;
            case 4:
                GM.playerColors.Add(Color.green);
                goto case 3;
            case 3:
                GM.playerColors.Add(Color.yellow);
                goto case 2;
            case 2:
                GM.playerColors.Add(Color.blue);
                GM.playerColors.Add(Color.red);
                break;
        }

        PlayersManager.CreatePlayers(GM.PlayerCount, GM.playerNames, GM.playerColors);
    }

    private void Start() {
        switch (GM.TerritoryAssignment) {
            case GameManager.AssignmentType.random:
                RandomAssignTerritories();
                break;
            case GameManager.AssignmentType.manual:
                StartCoroutine(ManuallyAssignTerritories());
                break;
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
