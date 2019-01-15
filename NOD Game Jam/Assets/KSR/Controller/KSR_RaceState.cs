using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[CreateAssetMenu(menuName ="KSR/Controller/KSR_RaceState")]
public class KSR_RaceState : PlayerBaseState
{
    private Rigidbody _rb;
    public float _deadzone = 0.1f;

    public float thrustFwd = 100.0f;
    public float thrustBack = 25.0f;
    public float turnSpeed = 10f;
    public float dashSpeed = 100f;
    public float boostSpeed = 1000f;

    private float _currentThrust;
    private float _currentTurn;
    private Vector3 _rotation;

    public override void Enter()
    {
        _rb = controller.GetComponent<Rigidbody>();
        _rotation = transform.rotation.eulerAngles;
    }

    public override void StateUpdate()
    {
        ReadInput();
        FixedUpdate();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(_currentThrust) > 0)
        {
            _rb.AddForce(transform.forward * _currentThrust);
        }

        if (Mathf.Abs(_currentTurn) > 0)
        {

            _rb.AddRelativeTorque(Vector3.up * _currentTurn * turnSpeed);
        }
    }

    private void ReadInput()
    {
        _currentThrust = 0.0f;
        float thrustInput = RewierdPlayer.GetAxis("Vertical");
        if (thrustInput > _deadzone)
        {

            _currentThrust = thrustInput * thrustFwd;
        }
        else if (thrustInput < _deadzone)
        {
            _currentThrust = thrustInput * thrustBack;
        }

        _currentTurn = 0.0f;
        float turnInput = RewierdPlayer.GetAxis("Horizontal");
        if (Mathf.Abs(turnInput) > _deadzone)
        {
            _currentTurn = turnInput;
        }
        else if (Mathf.Abs(turnInput) < _deadzone)
        {
            _currentTurn = 0;

        }
        Dash();
    }

    private void Dash()
    {
        if (RewierdPlayer.GetButtonDown("LT"))
        {
            _rb.AddForce(transform.right * -dashSpeed);
        }

        if (RewierdPlayer.GetButtonDown("RT"))
        {
            _rb.AddForce(transform.right * dashSpeed);
        }
    }

    public void Boost()
    {
        _rb.AddForce(transform.forward  * boostSpeed);
    }

}