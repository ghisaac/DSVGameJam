using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_Racer: MonoBehaviour
{
    public int lap = 0;
    public List<float> lapTimes;
    public bool finished = false;
    public List<KSR_Checkpoint> checkpointsCleared;
    public KSR_Checkpoint lastCheckpoint;
    public KSR_Checkpoint nextCheckpoint;
    public Player player;
    public float distanceToNextCheckpoint = 0;


    private void Update()
    {
        distanceToNextCheckpoint = Vector3.Distance(nextCheckpoint.transform.position, transform.position);
    }

    private void Start()
    {
        lastCheckpoint = KSR_RaceManager.instance.checkpoints[0];
        nextCheckpoint = KSR_RaceManager.instance.checkpoints[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpoint = other.GetComponent<KSR_Checkpoint>();
            nextCheckpoint = KSR_RaceManager.instance.Checkpoint(this);
        }
    }

    public void Finished()
    {
 //       GetComponent<KSR_Controller>().enabled = false;
    }
}
