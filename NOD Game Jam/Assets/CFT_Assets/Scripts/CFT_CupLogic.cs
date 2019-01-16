using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_CupLogic : MonoBehaviour
{
    [HideInInspector]public CFT_CupController _controller;
    [HideInInspector] public ParticleSystem particleSystem;
    [HideInInspector] public bool collided = false;
    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.rigidbody != null)
            SoundManager.Instance.PlayCupStack();   
        else SoundManager.Instance.PlayCupFallen();
          SoundManager.Instance.PlayCoffeeSplash();
        
        if (collided) return;
        if (GetComponent<Rigidbody>().isKinematic == true) return;

        if (collision.rigidbody != null && (collision.rigidbody.isKinematic == false) || collision.rigidbody == null)
        {
                    
            _controller.InstantiateCup();
            particleSystem.Play();
            collided = true;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (collided) return;
        _controller.InstantiateCup();
        collided = true;
    }
}
