using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantParticleTriggerScript : MonoBehaviour
{
    public GameObject dirtParticle, leafParticle;

    private void OnCollisionEnter(Collision collision) {
        
        dirtParticle.SetActive(true);
        leafParticle.SetActive(true);

    }

}
