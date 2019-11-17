using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    private int count;
    public int Count { get => count; set {} }
    private bool PlayersCreated = false;

    private int aliveCount;
    public int AliveCount { get => aliveCount; set {} }

    List<Player> Players = new List<Player>();

    private Color[] colorPalette = new Color[] { Color.blue, Color.red, Color.green, Color.yellow}; //to na razie tylko dla testów





    public bool CreatePlayers(int playersCount)
    {
        if(!PlayersCreated)
        {
            for(int i = 0; i < playersCount; i ++)
            {
                GameObject playerObject = new GameObject("Player" + (i + 1));
                playerObject.AddComponent<Player>();
                Players.Add(playerObject.GetComponent<Player>());
                Players[i].Color = colorPalette[i];
            }

            return true;
        } 
        else
        {
            return false;
        }
    }
}
