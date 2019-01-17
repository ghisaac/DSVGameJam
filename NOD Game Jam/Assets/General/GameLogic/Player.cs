using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Player
{
    public int RewierdId { get; private set; }
    public int Points { get; private set; }
    public string Name { get; private set; }
    public Color PlayerColor { get; private set; }

    public int LocalScore { get; set; }
    public int LocalPlacement;
    private Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow };
    public Rewired.Player Input { get { return Rewired.ReInput.players.GetPlayer(RewierdId); } }
    public static List<Player> AllPlayers = new List<Player>();
    private static int[] POINTSFORPLACEMENTS = { 3,2,1};


    public Player(int RewierdId, string Name)
    {
        this.RewierdId = RewierdId;
        this.Name = Name;
        this.Points = 0;
        this.PlayerColor = colors[RewierdId];
        AllPlayers.Add(this);
    }
    public static bool SetNewName(int playerID, string name)
    {
        for(int i = 0; i < Player.AllPlayers.Count; i++)
        {
            if(Player.AllPlayers[i].Name == name)
                return false;
        }
        Player.GetPlayerByRewindID(playerID).Name = name;
        return true;
       
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
    public static void DistributePoints(Player firstPlace, Player secondPlace = null, Player thirdPlace = null)
    {
        Player[] tempArr = { firstPlace, secondPlace, thirdPlace };
        DistributePoints(tempArr);
    }
    public static void DistributePoints(Player[] placements)
    {
        if (placements.Length > 3)
        {
            Player[] tempArr = { placements[0], placements[1], placements[2] };
            placements = tempArr;
        }
            
        for(int i = 0; i < placements.Length; i++)
        {
            if(placements[i] != null)
                placements[i].Points += POINTSFORPLACEMENTS[i];
        }

    }
    public static void GivePlayerPointsBasedOnPlacement(Player player, int Placement)
    {
        player.Points += POINTSFORPLACEMENTS[Placement - 1];
    }
    public static Player GetPlayerAtPlacement(int placement)
    {
        return GetPlayersByPoints()[placement];
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
    /// <summary>
    /// This method should only be used for testing when a scene is not played from the HUB scene. 
    /// </summary>
    /// <param name="amountOfPlayers"></param>
    public static void SpawnTestPlayers(int amountOfPlayers)
    {
        for(int i = 0; i < amountOfPlayers; i++)
        {
            Player temp = new Player(i, "player " + i);
            temp.Points = Random.Range(0, 10);
        }
    }
    public static int GetPlayerPlacementByID(int ID)
    {
        List<Player> tempList = GetPlayersByPoints();
        Player myPlayer = Player.GetPlayerByRewindID(ID);
        for(int i = 0; i < tempList.Count; i++)
        {
            if (myPlayer == tempList[i])
                return i;
        }

        return -1;
    }
    private static List<Player> GetPlayersByLocalPlacement()
    {
        List<Player> tempPlayers = new List<Player>(AllPlayers);
        tempPlayers = tempPlayers.OrderBy(x => x.LocalPlacement).ToList();
        return tempPlayers;
    }

    public static List<Player> GetPlayersByPoints()
    {
        List<Player> tempPlayers = AllPlayers;
        tempPlayers = tempPlayers.OrderByDescending(x => x.Points).ToList();
        return tempPlayers;
    }
}
