using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int RewierdId { get; private set; }
    public int Points { get; private set; }
    public string Name { get; private set; }

    public static List<Player> AllPlayers;

    public static void DistributePoints()
    {
    }
    public static void NameWinners()
    {

    }
    public static void GetPlayerByPlacement(int placement)
    {
    }
    public static Player GetPlayerByRewindID(int id)
    {
        Player toReturn = null;
        foreach (Player p in AllPlayers)
        {
            if (p.RewierdId == id)
            {
                toReturn = p;
                break;
            }

        }
        return toReturn;
    }
    public static Player GetPlayerByName(string name)
    {
        Player toReturn = null;
        foreach (Player p in AllPlayers)
        {
            if (p.name == name)
            {
                toReturn = p;
                break;
            }

        }
        return toReturn;
    }
}
