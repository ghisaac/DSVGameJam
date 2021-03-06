﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KSR_Racer : MonoBehaviour
{
    public int lap = 0;
    public List<float> lapTimes;
    public bool finished = false;
    public int checkpointsCleared = 0;
    public KSR_Checkpoint lastCheckpoint;
    public KSR_Checkpoint nextCheckpoint;
    public Player player;
    public float distanceToNextCheckpoint = 0;
    public float positionScore = 0;
    public Camera playerCamera;
    public Canvas playerCanvas;
    public Text placementText;
    public Text lapText;

    private void Start()
    {
        lastCheckpoint = KSR_RaceManager.instance.checkpoints[0];
        nextCheckpoint = KSR_RaceManager.instance.checkpoints[0];
        lapText.text = lap.ToString() + " Lap";
    }

    private void Update()
    {
        distanceToNextCheckpoint = Vector3.Distance(nextCheckpoint.transform.position, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpoint = other.GetComponent<KSR_Checkpoint>();
            nextCheckpoint = KSR_RaceManager.instance.Checkpoint(this);
        }
    }

    public float CalculateScore()
    {
        positionScore = ((lap * 10000f) + (checkpointsCleared * 100f) - distanceToNextCheckpoint);
        return positionScore;
    }

    public void Finished()
    {
        GetComponent<PlayerController>().enabled = false;
    }
}
