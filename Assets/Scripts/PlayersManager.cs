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

    private List<Player> players = new List<Player>();
    public List<Player> Players { get => players; set {} }

    private Color[] colorPalette = new Color[] { Color.blue, Color.red, Color.green, Color.yellow}; //to na razie tylko dla testów

    public bool CreatePlayers(int playersCount)
    {
        if(!PlayersCreated)
        {
            count = playersCount;
            for(int i = 0; i < count; i ++)
            {
                GameObject playerObject = new GameObject("Player" + (i + 1));
                playerObject.AddComponent<Player>();
                playerObject.transform.parent = this.transform;
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
