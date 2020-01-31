using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public static GameManager instance = null;
    public int gameOver = 0;
    public int zakonczonoPrzydzielanie = 0;

    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = new GameManager();

            return instance;
        }
    }

    //Liczba graczy
    private int playerCount = 2;



    public int PlayerCount
    {
        get
        {
            return playerCount;
        }
        set
        {
            if (value > 6 || value < 2) return;
            else playerCount = value;
            Debug.Log("Ustawiono " + playerCount + " graczy");
        }
    }

    //Przydzielanie terytorium
    public enum AssignmentType
    {
        manual,
        random
    }

    //aktualna tura
    public enum Round
    {
        Start,
        Resources,
        Fight,
        Move
    }

    public int attackButton = 0;

    public IEnumerator NextTour(PlayersManager playersManager, TerritoriesManager territoriesManager)
    {
        
        int activePlayer = 0;
        int attacking_resources = 0;

        for (int i = 0; i < territoriesManager.Territories.Count; i++)
        {
            territoriesManager.Territories[i].resources = 1;
        }
        while (gameOver != 1)
        {
            /*while(zakonczonoPrzydzielanie == 0)
            {
                yield return null;
            }
            */
            Debug.Log("Wchodze tu");
            switch (round)
            {
                case GameManager.Round.Start:

                    //Rozdawanie zasobow na poczatek
                    for(int i =0; i<playerCount; i++)
                    {
                        Debug.Log("gracz nr " + i);
                        int startResources = 35 - playerCount * 5;

                        Territory startTerritory;
                        while (startResources > 0)
                        {
                            do
                            {
                                startTerritory = territoriesManager.GetActiveTerritory();
                                yield return null;
                            }
                            while (startTerritory == null || startTerritory.Owner != playersManager.Players[activePlayer]);
                            startTerritory.resources += 1;
                            startResources -= 1;
                            Debug.Log("Dodano resources do terotorium. Terytorium ma teraz zasobow: " + startTerritory.resources);

                            yield return null;
                        }
                        Debug.Log("PlayerCount " + PlayerCount);
                        activePlayer += 1;
                        activePlayer = activePlayer % PlayerCount;
                        Debug.Log("activePlayer = " + activePlayer);
                       

                    }
                    Debug.Log("Wyszedlem ze startu");

                    round = Round.Resources;
                    activePlayer = 0;
                    break;
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
                    Debug.Log("Player: " + playersManager.Players[activePlayer]);
                    if (round != Round.Fight) break;
                    int defending_resources = 0;

                    attackButton = 0;
                    Debug.Log("Rozpoczynam walke. Wybieram teren do zaatakowania");
                    Territory destinationTerritory = null;
                    Territory attackingTerritory = null;
                    int activePlayerAround = 0;
                    while (destinationTerritory == null || destinationTerritory.Owner == playersManager.Players[activePlayer] || activePlayerAround != 1)
                    {
                        destinationTerritory = territoriesManager.GetActiveTerritory();
                        if (round != Round.Fight) break;
                        yield return null;
                        if (destinationTerritory == null) continue;
                        try
                        {
                            for (int neighboursIterator = 0; neighboursIterator < destinationTerritory.Neighoburs.Length; neighboursIterator++)
                            {

                                if (destinationTerritory.Neighoburs[neighboursIterator].Owner != playersManager.Players[activePlayer]) activePlayerAround = 1;
                                // Debug.Log(destinationTerritory.Neighoburs[neighboursIterator] + " " + destinationTerritory.Neighoburs[neighboursIterator].Owner
                                //    + " " + playersManager.Players[activePlayer] + " activeplayerAr = " + activePlayerAround);
                            }
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.Log("Nie ma sasiadow");
                        }

                    }
                    if (round != Round.Fight) break;
                    Color attackColor = new Color(200, 30, 80, 100);
                    destinationTerritory.standardColor = attackColor;
                    Debug.Log("destinationTerritory = " + destinationTerritory);

                    //Teraz wybieramy z którego terytorium chcemy zaatakować
                    while (attackButton == 0)
                    {
                        int checkNeigbours = 0;
                        attackingTerritory = null;
                        while (attackingTerritory == null || attackingTerritory.Owner != playersManager.Players[activePlayer] || checkNeigbours == 0
                            || attackingTerritory.resources < 2)
                        {
                            attackingTerritory = territoriesManager.GetActiveTerritory();
                            yield return null;

                            if (attackingTerritory == null) continue;
                            try
                            {
                                for (int neighboursIterator = 0; neighboursIterator < attackingTerritory.Neighoburs.Length; neighboursIterator++)
                                {
                                    if (attackingTerritory.Neighoburs[neighboursIterator] == destinationTerritory) checkNeigbours = 1;

                                    //Debug.Log(attackingTerritory.Neighoburs[neighboursIterator] + " /// " + destinationTerritory + " checkNe: " + checkNeigbours);

                                }
                            }
                            catch (NullReferenceException e)
                            {
                                Debug.Log("Nie ma sasiadow");
                            }
                            if (attackingTerritory.resources > 1)
                            {
                                attackingTerritory.resources--;
                                attacking_resources++;
                            }


                        }
                        attackingTerritory.GetComponent<SpriteRenderer>().color = territoriesManager.attackingColor;

                        Debug.Log("attackingTerritory = " + attackingTerritory + " AT res = " + attacking_resources + " AT.r = " + attackingTerritory.resources);
                    }
                    Debug.Log("Wyszedlem z wybierania walczenia");
                    attackButton = 0;
                    defending_resources = destinationTerritory.resources;

                    //tutaj bedzie losowanie walki
                    String wynik = BoardManager.ThrowCubes(attacking_resources, defending_resources);
                    String lostDef = "0";
                    String lostAtt = "0";
                    if (wynik != "null")
                    {
                        lostDef = wynik.Substring(0, wynik.LastIndexOf('/'));
                        lostAtt = wynik.Substring(wynik.LastIndexOf('/') + 1);
                        Debug.Log("lostdef " + lostDef);
                        Debug.Log("lostAtt " + lostAtt);
                    }
                    //walka skonczona, teraz liczymy wyniki
                    defending_resources -= int.Parse(lostDef);
                    attacking_resources -= int.Parse(lostAtt);

                    if (defending_resources < 1)
                    {
                        Debug.Log("Teraz jeszcze terytorium " + destinationTerritory + " Nalezy do " + destinationTerritory.Owner);
                        playersManager.Players[activePlayer].AddTerritory(destinationTerritory);
                        destinationTerritory.resources = attacking_resources;
                        attacking_resources = 0;
                        Debug.Log("Zmiana wlasciciela. Wlascicielem " + destinationTerritory + " jest " + destinationTerritory.Owner + playersManager.Players[activePlayer]);
                    }
                    else
                    {
                        destinationTerritory.standardColor = destinationTerritory.Owner.Color;
                        attackingTerritory.resources += attacking_resources;
                        attacking_resources = 0;
                    }




                    break;
                case GameManager.Round.Move:

                    Debug.Log("Zaczynam runde Move");
                    

                    Debug.Log("MOVE");
                    Boolean neighbour = false;
                    Territory fromTerritory;
                    do
                    {
                        fromTerritory = territoriesManager.GetActiveTerritory();
                        yield return null;
                    }
                    while (fromTerritory == null || fromTerritory.Owner != playersManager.Players[activePlayer]);
                    Debug.Log("from: " + fromTerritory);

                    //tutaj pownien pytać ile jednostek chce przenieść
                    //ale że nie ma czasu tego ogarniać to zakładam, że wszystko
                    int armyMoveCount = fromTerritory.resources - 1;

                    Territory toTerritory;
                    do
                    {
                        toTerritory = territoriesManager.GetActiveTerritory();
                        yield return null;

                        if (toTerritory == null) continue;
                        else
                        {
                            for (int neighboursIterator = 0; neighboursIterator < fromTerritory.Neighoburs.Length; neighboursIterator++)
                            {
                                if (fromTerritory.Neighoburs[neighboursIterator] == toTerritory)
                                    neighbour = true;
                            }
                        }
                    }
                    while (toTerritory == null || toTerritory.Owner != playersManager.Players[activePlayer] || !neighbour);
                    Debug.Log("to: " + toTerritory);
                    fromTerritory.resources -= armyMoveCount;
                    toTerritory.resources += armyMoveCount;


                    Debug.Log("PlayerCount " + PlayerCount);
                    activePlayer += 1;
                    activePlayer = activePlayer % PlayerCount;
                    Debug.Log("activePlayer = " + activePlayer);
                    round = GameManager.Round.Resources;

                    yield return null;
                    break;
            }
            
        }

        //int tura = GameManager.Tura.;

    }



    public AssignmentType TerritoryAssignment;
    public Round round;

    //Nazwy i kolory graczy
    public List<string> playerNames = new List<string>();
    public List<Color> playerColors = new List<Color>();
}
