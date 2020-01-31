using System.Collections;
using System.Collections.Generic;
using System;
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
        Debug.Log("Wchodze tu");
        GM.round = GameManager.Round.Resources;
        StartCoroutine(GM.NextTour(PlayersManager, TerritoriesManager));

        
        
    }

    public void ThrowCubes(int attackerResources, int defenderResources)
    {

        List<int> attackerThrows = new List<int>();
        List<int> defenderThrows = new List<int>();

        if (attackerResources <= 0 || defenderResources <= 0)
        {
            //Żadne jednostki nie walczą
            return;
        }

        for (int attCubeThrow = 0; attCubeThrow < attackerResources && attCubeThrow < 3; attCubeThrow++) { attackerThrows.Add(GetRandomNumber()); }
        for (int defCubeThrow = 0; defCubeThrow < defenderResources && defCubeThrow < 2; defCubeThrow++) { defenderThrows.Add(GetRandomNumber()); }

        int timesOfComparisons = Math.Min(attackerResources, defenderResources);
        int attackerWins = 0;
        int defenderWins = 0;

        for(int comparison = 0; comparison < timesOfComparisons; comparison++)
        {
            int maxAttackerThrow = 0;
            foreach (int atThrow in attackerThrows) { if (maxAttackerThrow < atThrow) maxAttackerThrow = atThrow; }
            attackerThrows.Remove(maxAttackerThrow);

            int maxDefenderThrow = 0;
            foreach (int defThrow in defenderThrows) { if (maxDefenderThrow < defThrow) maxDefenderThrow = defThrow; }
            defenderThrows.Remove(maxDefenderThrow);

            if (maxAttackerThrow == maxDefenderThrow) defenderWins++;
            else if (maxAttackerThrow > maxDefenderThrow) attackerWins++;
            else defenderWins++;

        }

        // attackerWins - liczba wygranych przez atakującego
        // defenderWins - -||- obraniającego


        return;




    }

    public void MetodaKtoraJestPustaOk()
    {

    }

    private int GetRandomNumber()
    {
        return UnityEngine.Random.Range(1, 6);
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
                    int rand = used[UnityEngine.Random.Range(0, used.Count)];
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
