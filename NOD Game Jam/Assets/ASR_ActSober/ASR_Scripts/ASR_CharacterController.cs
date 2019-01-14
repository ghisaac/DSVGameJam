using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_CharacterController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _horizontal;
    public float Force = 20f;

    private float _inputModifier = 1f;
    private const float INPUT_MODIFIER_MIN = 1F;

    public Transform ForcePositionTransform;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        InputModification();
        Move();
    }

    private void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        //Vector3 direction = -transform.right * _horizontal;
        //_rigidbody.AddForce(direction * Force * _inputModifier, ForceMode.Acceleration);

        Vector3 direction = -ForcePositionTransform.right * _horizontal;
        _rigidbody.AddForceAtPosition(direction * Force * _inputModifier, ForcePositionTransform.position, ForceMode.Acceleration);
    }

    private void InputModification(){

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0){
            _inputModifier += Time.deltaTime;
        } else {
            _inputModifier = Mathf.Clamp(_inputModifier - Time.deltaTime, INPUT_MODIFIER_MIN, _inputModifier);
        }
        Debug.Log("Inputmodifier = " + _inputModifier);
    }

    public void AddForce(Vector3 force){

        _rigidbody.AddForceAtPosition(force, ForcePositionTransform.position, ForceMode.Acceleration);

    }

}
