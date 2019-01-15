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
    void Start()
    {
        result.Clear();
        racers.Clear();
        for (int i = 0; i<nrPlayers; i++)
        {
            GameObject p = Instantiate(playerPrefab, startLine.position + new Vector3(i*2, 0, 0), startLine.rotation);
            p.GetComponentInChildren<KSR_Racer>().nextCheckpoint = checkpoints[0];
            racers.Add(p.GetComponent<KSR_Racer>());
        }
        positions.Capacity = nrPlayers;
}

    //void Start()
    //{
    //    int i = 0;
    //    foreach (Player player in Player.AllPlayers)
    //    {
    //        GameObject p = Instantiate(playerPrefab, startLine.position + new Vector3(i * 5, 0, 0), startLine.rotation);
    //        p.GetComponent<KSR_Racer>().player = player;
    //        p.GetComponent<KSR_Racer>().nextCheckpoint = checkpoints[0];
    //        racers.Add(p.GetComponent<KSR_Racer>());
    //        i++;
    //    }
    //}

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

        Positions();
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

    //public void Lap(KSR_Racer player)
    //{
    //    if(player.lap > lapsToWin)
    //    {
    //        player.finished = true;
    //    }
    //    int nrFinished = 0;
    //    foreach(KSR_Racer racer in racers)
    //    {
    //        if (racer != null)
    //        {
    //            if (racer.finished)
    //            {
    //                nrFinished++;
    //                result.Add(racer.GetComponent<KSR_Racer>());
    //            }
    //        }
    //    }
    //    if (nrFinished == racers.Count)
    //    {
    //        EndRace();
    //    }
    //}

    public KSR_Checkpoint Checkpoint(KSR_Racer racer)
    {
        if(racer.lastCheckpoint == checkpoints[0] && racer.nextCheckpoint == checkpoints[0])
        {
            racer.lap += 1;
            racer.lapTimes.Add(Time.time);
            if (racer.lap == lapsToWin + 1)
            {
                result.Add(racer);
                racer.Finished();

            }
            if(racers.Count == result.Count)
            {
                EndRace();
            }
        }
        if (racer.lastCheckpoint == racer.nextCheckpoint)
        {
            if (checkpoints.IndexOf(racer.lastCheckpoint) != checkpoints.Count - 1)
            {
                return checkpoints[checkpoints.IndexOf(racer.lastCheckpoint) + 1];
            }
            else
            {
                return checkpoints[0];
            }
        }
        else return racer.nextCheckpoint;
    }

    public void Positions()
    {
        positions.Clear();
        foreach(KSR_Racer racer in racers)
        {
            if(!TestPosition(0, racer))
            {
                if(!TestPosition(1, racer))
                {
                    if(!TestPosition(2, racer))
                    {
                        positions.Add(racer);
                    }
                }
            }
        }
    }

    private bool TestPosition(int index, KSR_Racer racer)
    {
        if (positions.Count > index && racer.lap > positions[index].lap)
        {
            positions.Insert(index, racer);
            return true;
        }
        else if (positions.Count > index && racer.lastCheckpoint.checkpointNr < positions[index].lastCheckpoint.checkpointNr)
        {
            positions.Insert(index, racer);
            return true;
        }
        else if(positions.Count > index && racer.distanceToNextCheckpoint < positions[index].distanceToNextCheckpoint)
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
