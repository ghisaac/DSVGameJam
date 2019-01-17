using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_ParticleSpawner : MonoBehaviour
{
    [Header("DrivingParticles")]
    public GameObject smokeTrailParticle;
    public GameObject brakeParticle;
    [Header("EnviromentParticles")]
    public GameObject dirtParticle;
    public GameObject leafParticle;
    [Header("ImpactParticles")]
    public GameObject bigImpactParticle;
    public GameObject smallImpactParticle;

    [HideInInspector]
    public static KSR_ParticleSpawner instance;

    public void Awake()
    {
        instance = this;
    }

    public void SpawnSmokeTrailParticle(Vector3 pos, Quaternion rotation)
    {
        GameObject temp = smokeTrailParticle;
        Instantiate(temp, pos, rotation);
    }

    public void SpawnBrakeParticle(Vector3 pos, Quaternion rotation)
    {
        GameObject temp = brakeParticle;
        Instantiate(temp, pos, rotation);
    }

    public void SpawnDirtParticles(Vector3 pos, Quaternion rotation)
    {
        GameObject temp = dirtParticle;
        Instantiate(temp, pos, rotation);
    }

    public void SpawnLeafPatricles(Vector3 pos, Quaternion rotation)
    {
        GameObject temp = leafParticle;
        Instantiate(temp, pos, rotation);
    }

    public void SpawnBigImpact(Vector3 pos, Quaternion rotation)
    {
        GameObject temp = bigImpactParticle;
        Instantiate(temp, pos, rotation);
    }

    public void SpawnSmallImpact(Vector3 pos, Quaternion rotation)
    {
        GameObject temp = smallImpactParticle;
        Instantiate(temp, pos, rotation);
    }

   
}
