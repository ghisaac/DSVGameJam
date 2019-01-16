using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_Collision : MonoBehaviour
{
    public GameObject mainCamera;
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
        if (col.gameObject.tag == "Wall")
        {
            Bounce(col.contacts[0].normal);

            if(mainCamera != null)
                mainCamera.GetComponent<KSR_CameraFollow>().delayCam = true;
            // Debug.Log("WALL");
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
