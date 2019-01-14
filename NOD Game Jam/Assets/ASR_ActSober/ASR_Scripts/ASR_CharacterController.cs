using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_CharacterController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _horizontal;
    public float Force = 20f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 direction = -transform.right * _horizontal;

        _rigidbody.AddForce(direction * Force, ForceMode.Acceleration);

    }
}
