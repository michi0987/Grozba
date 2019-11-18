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

    public bool CreatePlayers(int playersCount, List<string> names, List<Color> colors)
    {
        if(playersCount != names.Count || playersCount != colors.Count)
        {
            Debug.Log("PlayersManager : CreatePlayers() - Incorrect Parameters!");
            return false;
        }
        if(!PlayersCreated)
        {
            count = playersCount;
            for(int i = 0; i < count; i ++)
            {
                GameObject playerObject = new GameObject(names[i]);
                playerObject.AddComponent<Player>();
                playerObject.transform.parent = this.transform;
                Players.Add(playerObject.GetComponent<Player>());
                Players[i].Color = colors[i];
            }
            return true;
        } 
        else
        {
            Debug.Log("PlayersManager : CreatePlayers() - Players have already been created!");
            return false;
        }
    }


}
