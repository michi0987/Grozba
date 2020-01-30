using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//dodajcie tu komentarze kurwa...
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
        GM.round = GameManager.Round.Resources;
        StartCoroutine(GM.NextTour(PlayersManager, TerritoriesManager));

        
        
    }

    public void EndAttack()
    {
        if(GM.round == GameManager.Round.Fight)
        {
            GM.round = GameManager.Round.Move;
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
