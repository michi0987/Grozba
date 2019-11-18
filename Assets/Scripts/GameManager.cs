﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public static GameManager instance = null;

    private GameManager() { }

    public static GameManager Instance {
        get {
            if(instance==null) instance = new GameManager();
 
            return instance;
        }
    }

    //Liczba graczy
    private int playerCount = 0;

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

    public AssignmentType TerritoryAssignment;
}
