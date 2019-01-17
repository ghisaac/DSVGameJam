using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_Checkpoint : MonoBehaviour
{
    public int checkpointNr = 0;

    private void Start()
    {
        checkpointNr = KSR_RaceManager.instance.checkpoints.IndexOf(this);
    }
}
