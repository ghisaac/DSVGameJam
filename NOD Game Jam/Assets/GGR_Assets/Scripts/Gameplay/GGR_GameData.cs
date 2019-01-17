using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GGR_GameData : MonoBehaviour
{
    public int rounds;
    public List<Transform> spawnPositions = new List<Transform>();
    public List<Transform> boardPictureSlots;
    public Transform instructionNoteTransform;
    public GameObject splashScreen;
    public GameObject timesUp;

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

    public static void FindPlayers()
    {
        List <PlayerController> controllers = FindObjectsOfType<PlayerController>().ToList();
        foreach(PlayerController pc in controllers)
        {
            instance.allPlayerControllers.Add(pc.myPlayer, pc);
        }
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
        if (instance.scoreBoard.ContainsKey(player))
        {
            instance.scoreBoard[player] += score;
            player.LocalScore = Mathf.RoundToInt(instance.scoreBoard[player]);
        }
        else
        {
            instance.scoreBoard.Add(player, score);
            player.LocalScore = Mathf.RoundToInt(instance.scoreBoard[player]);
        }
         
    }

    public static float GetPlayerScore(Player player)
    {
        return instance.scoreBoard[player];
    }

    public static List<Transform> GetPictureSlots()
    {
        return instance.boardPictureSlots;
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
