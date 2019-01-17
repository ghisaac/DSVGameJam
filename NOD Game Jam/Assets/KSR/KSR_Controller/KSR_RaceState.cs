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
    private float _rotationSpeed = 100f;
    private GameObject _playerCamera;

    public override void Enter()
    {
        _rb = controller.GetComponent<Rigidbody>();
        _rotation = transform.rotation.eulerAngles;
        _playerCamera = controller.gameObject.transform.GetChild(2).gameObject;
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
        controller.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        _currentThrust = 0.0f;
        float thrustInput = RewierdPlayer.GetAxis("Vertical");
        if (thrustInput > _deadzone)
        {
            controller.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            _currentThrust = thrustInput * thrustFwd;
        }
        else if (thrustInput < _deadzone)
        {
            _currentThrust = thrustInput * thrustBack;
            //KSR_ParticleSpawner.instance.SpawnBrakeParticle(controller.transform.position, controller.transform.rotation);

            if (_rb.velocity.magnitude > 0.2)
            controller.gameObject.transform.GetChild(4).gameObject.SetActive(true);

            else if (_rb.velocity.magnitude < 0.2)
                controller.gameObject.transform.GetChild(4).gameObject.SetActive(false);
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
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector3 cameraRotation = new Vector3(0f, 0, 0f);
        float yRotation = RewierdPlayer.GetAxis("RightHorizontal");
        if(Mathf.Abs(yRotation) > _deadzone) {

            cameraRotation = new Vector3(0f, yRotation, 0f);
            _playerCamera.transform.RotateAround(controller.gameObject.transform.position, cameraRotation, Time.deltaTime * _rotationSpeed);
        }
        else
        {
       //     _playerCamera.transform.rotation = new Quaternion(0,0,0,0);
        }
        
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
        controller.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.PlayBoost();
    }

    public float GetHorizontalAxis()
    {
        return RewierdPlayer.GetAxis("RightHorizontal");
    }
    public float GetVerticalAxis()
    {
        return RewierdPlayer.GetAxis("RightVertical");
    }

}