using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public static GameManager instance = null;
    public int gameOver = 0;

    private GameManager() { }

    public static GameManager Instance {
        get {
            if(instance==null) instance = new GameManager();
 
            return instance;
        }
    }

    //Liczba graczy
    private int playerCount = 2;

    

    public int PlayerCount {
        get {
            return playerCount;
        }
        set {
            if (value > 6 || value < 2) return;
            else playerCount = value;
            Debug.Log("Ustawiono " + playerCount + " graczy");
        }
    }

    //Przydzielanie terytorium
    public enum AssignmentType { 
        manual,
        random
    }

    //aktualna tura
    public enum Round
    {
        Resources,
        Fight,
        Move
    }

    public IEnumerator NextTour(PlayersManager playersManager, TerritoriesManager territoriesManager)
    {
        int activePlayer = 0;
        while (gameOver != 1)
        {

            switch (round)
            {
                case GameManager.Round.Resources:
                    playersManager.Players[activePlayer].freeResources = playersManager.Players[activePlayer].Territories.Count;
                    Debug.Log("freeResources for player" + activePlayer + ": " + playersManager.Players[activePlayer].freeResources);

                    Territory currentTerritory;
                    while (playersManager.Players[activePlayer].freeResources > 0)
                    {
                        do
                        {
                            currentTerritory = territoriesManager.GetActiveTerritory();
                            yield return null;
                        }
                        while (currentTerritory == null || currentTerritory.Owner != playersManager.Players[activePlayer]);
                        currentTerritory.resources += 1;
                        playersManager.Players[activePlayer].freeResources -= 1;
                        Debug.Log("Dodano resources do terotorium. Terytorium ma teraz zasobow: " + currentTerritory.resources);

                        yield return null;
                    }
                    Debug.Log("Wyszedlem z resources");
                    round = GameManager.Round.Fight;
                    break;
                case GameManager.Round.Fight:
                    Debug.Log("Rozpoczynam walke. Wybieram teren do zaatakowania");
                    Territory destinationTerritory = null;
                    Territory attackingTerritory = null;
                    do
                    {
                        destinationTerritory = territoriesManager.GetActiveTerritory();
                        yield return null;
                    }
                    while (destinationTerritory == null || destinationTerritory.Owner != playersManager.Players[activePlayer]);
                    destinationTerritory.attack = true;


                    break;
                case GameManager.Round.Move:
                    Debug.Log("Zaczynam runde Move");
                    break;
            }
            Debug.Log("PlayerCount " + PlayerCount);
            activePlayer += 1;
            activePlayer = activePlayer % PlayerCount;
            Debug.Log("activePlayer = " + activePlayer);
            yield return null;
        }

        //int tura = GameManager.Tura.;

    }



    public AssignmentType TerritoryAssignment;
    public Round round;

    //Nazwy i kolory graczy
    public List<string> playerNames = new List<string>();
    public List<Color> playerColors = new List<Color>();
}
