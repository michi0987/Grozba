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
        StartCoroutine(NextTour());

        
        
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

    private IEnumerator NextTour()
    {
        int activePlayer = 0;
        while(GM.gameOver != 1)
        {

            switch (GM.round)
            {
                case GameManager.Round.Resources:
                    PlayersManager.Players[activePlayer].freeResources = PlayersManager.Players[activePlayer].Territories.Count;
                    Debug.Log("freeResources for player" + activePlayer + ": " + PlayersManager.Players[activePlayer].freeResources);

                    Territory currentTerritory;
                    while(PlayersManager.Players[activePlayer].freeResources > 0)
                    {
                        do
                        {
                            currentTerritory = TerritoriesManager.GetActiveTerritory();
                            yield return null;
                        }
                        while (currentTerritory == null || currentTerritory.Owner != PlayersManager.Players[activePlayer]);
                        currentTerritory.resources += 1;
                        PlayersManager.Players[activePlayer].freeResources -= 1;
                        Debug.Log("Dodano resources do terotorium. Terytorium ma teraz zasobow: " + currentTerritory.resources);

                        yield return null;
                    }
                    Debug.Log("Wyszedlem z resources");
                    GM.round = GameManager.Round.Fight;
                    break;
                case GameManager.Round.Fight:
                    Debug.Log("Rozpoczynam walke. Wybieram teren do zaatakowania");
                    Territory destinationTerritory = null;
                    Territory attackingTerritory = null;
                    do
                    {
                        destinationTerritory = TerritoriesManager.GetActiveTerritory();
                        yield return null;
                    }
                    while (destinationTerritory == null || destinationTerritory.Owner != PlayersManager.Players[activePlayer]);
                    destinationTerritory.attack = true;


                    break;
                case GameManager.Round.Move:
                    Debug.Log("Zaczynam runde Move");
                    break;
            }
            Debug.Log("PlayerCount " + GM.PlayerCount);
            activePlayer += 1;
            activePlayer = activePlayer % GM.PlayerCount;
            Debug.Log("activePlayer = " + activePlayer);
            yield return null;
        }
        
        //int tura = GameManager.Tura.;
        
    }
}
