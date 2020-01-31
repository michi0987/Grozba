﻿using System;
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
        Resources,
        Fight,
        Move
    }



    public IEnumerator NextTour(PlayersManager playersManager, TerritoriesManager territoriesManager)
    {
        int attackButton = 0;
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
                    round = GameManager.Round.Move;
                    break;
                case GameManager.Round.Fight:
                    
                    attacking_resources = 0;
                    attackButton = 0;
                    Debug.Log("Rozpoczynam walke. Wybieram teren do zaatakowania");
                    Territory destinationTerritory = null;
                    Territory attackingTerritory = null;
                    int activePlayerAround = 0;
                    while (destinationTerritory == null || destinationTerritory.Owner != playersManager.Players[activePlayer] || activePlayerAround != 1)
                    {
                        destinationTerritory = territoriesManager.GetActiveTerritory();
                        yield return null;
                        if (destinationTerritory == null) continue;
                        try
                        {
                            for (int neighboursIterator = 0; neighboursIterator < destinationTerritory.Neighoburs.Length; neighboursIterator++)
                            {
                                //Debug.Log(destinationTerritory.Neighoburs[neighboursIterator] + " " + destinationTerritory.Neighoburs[neighboursIterator].Owner
                                //   + " " + playersManager.Players[activePlayer]);
                                if (destinationTerritory.Neighoburs[neighboursIterator].Owner != playersManager.Players[activePlayer]) activePlayerAround = 1;

                            }
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.Log("Nie ma sasiadow");
                        }

                    }

                    destinationTerritory.GetComponent<SpriteRenderer>().color = territoriesManager.attackColor;
                    Debug.Log("destinationTerritory = " + destinationTerritory);

                    //Teraz wybieramy z którego terytorium chcemy zaatakować
                    while (attackButton == 0)
                    {
                        int checkNeigbours = 0;
                        attackingTerritory = null;
                        while (attackingTerritory == null || attackingTerritory.Owner == playersManager.Players[activePlayer] || checkNeigbours == 0)
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

                    
                    break;
                case GameManager.Round.Move:
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
