using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_SpeedBoost : MonoBehaviour
{
    public float respawnTime = 2f;

    private MeshRenderer meshRenderer;
    private CapsuleCollider col;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        col = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           if(other.GetComponent<PlayerController>().stateMachine.CurrentState is KSR_RaceState)
            {
                other.GetComponent<PlayerController>().stateMachine.GetState<KSR_RaceState>().Boost();
                Debug.Log("SPEEDBOOST");
                Deactivate();
            }
        }
    }

    private void Deactivate()
    {
        meshRenderer.enabled = false;
        col.enabled = false;
        StartCoroutine(ReActivate());
    }

    private IEnumerator ReActivate()
    {
        yield return new WaitForSeconds(respawnTime);
        meshRenderer.enabled = true;
        col.enabled = true;
    }
}
