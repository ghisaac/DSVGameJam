using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GGR_GameData : MonoBehaviour
{
    public int rounds;
    public PlayerController playerControllerPrefab;
    public List<Transform> spawnPositions = new List<Transform>();

    public static GGR_GameData instance { get; private set; }
    private List<GGR_Location> allLocations = new List<GGR_Location>();
    private static readonly string pathDirectory = "Locations";
    private Queue<GGR_Location> roundLocations = new Queue<GGR_Location>();

    private Dictionary<Player, PlayerController> allPlayerControllers = new Dictionary<Player, PlayerController>();
    private Dictionary<Player, float> scoreBoard = new Dictionary<Player, float>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Too much data");

        allLocations = Resources.LoadAll<GGR_Location>(pathDirectory).ToList();
        DeckShuffle();
    }

    public static GGR_Location GetNextLocation()
    {
        return instance.roundLocations.Peek();
    }

    public static GGR_Location DequeueCurrentLocation()
    {
        return instance.roundLocations.Dequeue();
    }

    public static int GetLocationsRemaining()
    {
        return instance.roundLocations.Count;
    }

    public static void SpawnPlayer(Player player)
    {
        PlayerController pc = Instantiate(instance.playerControllerPrefab) as PlayerController;
        pc.myPlayer = player;
        instance.allPlayerControllers.Add(player, pc);
    }

    public static void SetPlayerPosition(Player player, Vector3 position)
    {
        instance.allPlayerControllers[player].transform.position = position;
    }

    public static Vector3 GetSpawnPosition(int spawnIndex)
    {
        return instance.spawnPositions[spawnIndex].position;
    }
    public static List<PlayerController> GetAllPlayerControllers()
    {
        return instance.allPlayerControllers.Values.ToList();
    }

    public static void FreezeAllPlayers()
    {
        foreach(PlayerController pc in instance.allPlayerControllers.Values)
        {
            pc.stateMachine.TransitionToState<GGR_FreezeState>();
        }
    }

    public static void UnfreezeAllPlayers()
    {
        foreach (PlayerController pc in instance.allPlayerControllers.Values)
        {
            pc.stateMachine.TransitionToState<GroundState>();
        }
    }

    public static void GivePlayerScore(Player player, float score)
    {
        instance.scoreBoard[player] += score;
    }

    
    private void DeckShuffle()
    {
        List<GGR_Location> locations = new List<GGR_Location>(allLocations);

        for(int i = 0; i < rounds; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, locations.Count);
            roundLocations.Enqueue(Instantiate(locations[randomIndex]));
            locations.RemoveAt(randomIndex);
        }
    }

    //temp
    public static List<Player> GetAllPlayers()
    {
        return instance.allPlayerControllers.Keys.ToList();
    }



}
