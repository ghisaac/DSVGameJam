using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator Animator;
    public float maxSpeed;
    private PlayerController controller;
    private Vector3 lookDirection = Vector3.one;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {

        Vector3 newlookDirection = Vector3.ProjectOnPlane(controller.Velocity, controller.groundPlane);
        if (newlookDirection.magnitude > 1f)
            lookDirection = newlookDirection;
        Quaternion wantToLook = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, wantToLook, 0.1f);
        Animator.SetFloat("Velocity", Mathf.Clamp01(lookDirection.magnitude / maxSpeed));
        Animator.SetBool("Airborne", controller.stateMachine.CurrentState == controller.stateMachine.GetState<AirState>());
    }
}
