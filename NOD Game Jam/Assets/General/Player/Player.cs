using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Player
{
    public int RewierdId { get; private set; }
    public int Points { get; private set; }
    public string Name { get; private set; }

    public Rewired.Player Input { get { return Rewired.ReInput.players.GetPlayer(RewierdId); } }

    public static List<Player> AllPlayers;

    public Player(int RewierdId, string Name)
    {
        this.RewierdId = RewierdId;
        this.Name = Name;
        this.Points = 0;
        AllPlayers.Add(this);
    }

    public static bool CheckIfPlayerExists(int RewierdId)
    {
        foreach(Player p in AllPlayers)
        {
            if (p.RewierdId == RewierdId)
                return true;
        }
        return false;
    }
    public static void RemovePlayer(int RewierdId)
    {
        Player toRemove = null;
        foreach (Player p in AllPlayers)
        {
            if (p.RewierdId == RewierdId)
                toRemove = p;
        }
        if (toRemove != null)
            AllPlayers.Remove(toRemove);
    }

    public static void DistributePoints()
    {

    }
    public static void NameWinners()
    {

    }
    public static Player GetPlayerByPlacement(int placement)
    {
        
        return null;
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
            if (p.Name == name)
            {
                toReturn = p;
                break;
            }

        }
        return toReturn;
    }
}
