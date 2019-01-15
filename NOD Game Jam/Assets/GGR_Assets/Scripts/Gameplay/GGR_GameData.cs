using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GGR_GameData : MonoBehaviour
{
    public int rounds;

    private static GGR_GameData instance;
    private List<GGR_Location> allLocations = new List<GGR_Location>();
    private static readonly string pathDirectory = "Locations";
    private Queue<GGR_Location> roundLocations = new Queue<GGR_Location>();

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

    private void DeckShuffle()
    {
        List<GGR_Location> locations = new List<GGR_Location>(allLocations);

        for(int i = 0; i < rounds; i++)
        {
            int randomIndex = Random.Range(0, locations.Count);
            roundLocations.Enqueue(Instantiate(locations[randomIndex]));
            locations.RemoveAt(randomIndex);
        }
    }




}
