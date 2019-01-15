using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class KSR_RaceManager : MonoBehaviour
{
    public static KSR_RaceManager instance = null;

    public List<KSR_Checkpoint> checkpoints;
    public float raceTime = 300;
    public int lapsToWin = 4;
    public Transform startLine;
    public GameObject playerPrefab;

    float startTime;
    bool raceStarted = false;
    public List<KSR_Racer> result;
    public List<KSR_Racer> racers;
    public List<KSR_Racer> positions;

    public int nrPlayers = 2;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    result.Clear();
    //    racers.Clear();
    //    for (int i = 0; i<nrPlayers; i++)
    //    {
    //        GameObject p = Instantiate(playerPrefab, startLine.position + new Vector3(i*2, 0, 0), startLine.rotation);
    //        p.GetComponentInChildren<KSR_Racer>().nextCheckpoint = checkpoints[0];
    //        racers.Add(p.GetComponent<KSR_Racer>());
    //    }
    //    positions.Capacity = nrPlayers;
    //}

    void Start()
    {
        Player.SpawnTestPlayers(4);
        int i = 0;
        foreach (Player player in Player.AllPlayers)
        {
            GameObject p = Instantiate(playerPrefab, startLine.position + new Vector3(i * 2, 0, 0), startLine.rotation);
            p.GetComponent<KSR_Racer>().player = player;
            p.GetComponent<KSR_Racer>().nextCheckpoint = checkpoints[0];
            racers.Add(p.GetComponent<KSR_Racer>());
            i++;
            p.gameObject.GetComponentInChildren<Camera>().targetDisplay = i;
            p.gameObject.name = "Player " + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!raceStarted)
            {
                StartRace();
            }
        }

        UpdatePositions();
    }

    void StartTime()
    {
        startTime = Time.time;
        Debug.Log("Race Start!");
    }

    void EndRace()
    {
        foreach (KSR_Racer racer in racers)
        {
            if (racer != null)
            {
     //           racer.GetComponent<KSR_Controller>().enabled = false;
            }
        }
        Debug.Log("Race Over!");
    }

    void StartRace()
    {
        foreach(KSR_Racer racer in racers)
        {
            //racer.GetComponent<KSR_Controller>().enabled = true;
            //racer.GetComponent<Rigidbody>().useGravity = true;
        }
        StartTime();
        raceStarted = true;
    }

    public KSR_Checkpoint Checkpoint(KSR_Racer racer)
    {
        if (racer.lastCheckpoint == checkpoints[0] && racer.nextCheckpoint == checkpoints[0])
        {
            racer.lap += 1;
            racer.lapTimes.Add(Time.time);
            if (racer.lap == lapsToWin + 1)
            {
                result.Add(racer);
                racer.Finished();
            }
            if (racers.Count == result.Count)
            {
                EndRace();
            }
        }
        if (racer.lastCheckpoint == racer.nextCheckpoint)
        {
            if (checkpoints.IndexOf(racer.lastCheckpoint) != checkpoints.Count - 1)
            {
                racer.checkpointsCleared = +1;
                return checkpoints[checkpoints.IndexOf(racer.lastCheckpoint) + 1];
            }
            else
            {
                racer.checkpointsCleared = +1;
                return checkpoints[0];
            }
        }
        else return racer.nextCheckpoint;
    }

    //public void Positions()
    //{
    //    positions.Clear();
    //    foreach(KSR_Racer racer in racers)
    //    {
    //        if(!TestPosition(0, racer))
    //        {
    //            if(!TestPosition(1, racer))
    //            {
    //                if(!TestPosition(2, racer))
    //                {
    //                    positions.Add(racer);
    //                }
    //            }
    //        }
    //    }
    //}

    private void UpdatePositions()
    {
        positions.Clear();
        foreach (KSR_Racer racer in racers)
        {
            racer.CalculateScore();
            positions.Add(racer);
        }
        //Sorting list and check it count
        if (positions.Count > 0)
        {
            positions.Sort(delegate (KSR_Racer a, KSR_Racer b)
            {
                return (a.positionScore).CompareTo(b.positionScore);
            });
        }
        positions.Reverse();
    }

    bool TestPosition(int index, KSR_Racer racer)
    {
        if (positions.Count > index && racer.lap > positions[index].lap)
        {
            positions.Insert(index, racer);
            return true;
        }
        else if (positions.Count > index && racer.nextCheckpoint.checkpointNr > positions[index].nextCheckpoint.checkpointNr && racer.lap >= positions[index].lap)
        {
            positions.Insert(index, racer);
            return true;
        }
        else if (positions.Count > index && racer.distanceToNextCheckpoint < positions[index].distanceToNextCheckpoint)
        {
            positions.Insert(index, racer);
            return true;
        }
        else
        {
            return false;
        }
    }
}
