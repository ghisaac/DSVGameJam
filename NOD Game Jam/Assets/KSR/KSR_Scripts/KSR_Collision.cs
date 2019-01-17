using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_Collision : MonoBehaviour
{
 //   public GameObject mainCamera;
    public float bounceSpeed = 1f;
    private Vector3 currentVelocity;
    private Rigidbody _rb;
    private PlayerController _controller;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        currentVelocity = _rb.velocity;


    }

    private void OnCollisionEnter(Collision col)
    {
        Bounce(col.contacts[0].normal);
        if (KSR_ParticleSpawner.instance != null)
        {
            KSR_ParticleSpawner.instance.SpawnBigImpact(transform.position, transform.rotation);
        }
        else
        {
            Debug.Log("FINNS INGEN PARTICLESPAWNER");
        }

        if(col.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayCollidePlayer();
        }

        if (col.gameObject.tag == "Wall")
        {
            SoundManager.Instance.PlayCollideTerrain();
        }
    }

    private void Bounce(Vector3 collisonNormal)
    {
        var movementSpeed = currentVelocity.magnitude;
        var direction = Vector3.Reflect(currentVelocity.normalized, collisonNormal);
        _rb.velocity = direction * Mathf.Clamp(Mathf.Max(movementSpeed, bounceSpeed), -10, 10);
        Debug.Log("bouncen är: " + _rb.velocity);
    }
}
