using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Kopia av andras kod. Skapad för att fungera i våran scen.
 */
namespace FIL
{

    public class FIL_PlayerAnimation : MonoBehaviour
    {
        public Animator Animator;
        public float maxSpeed;
        private FIL_PlayerController controller;
        private Vector3 lookDirection;

        private void Start()
        {
            controller = GetComponent<FIL_PlayerController>();
        }

        void Update()
        {

            Vector3 newlookDirection = Vector3.ProjectOnPlane(controller.Velocity, controller.groundPlane);
            if (newlookDirection.magnitude > 1f)
                lookDirection = newlookDirection;
            Quaternion wantToLook = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, wantToLook, 0.1f);
            Animator.SetFloat("Velocity", Mathf.Clamp01(lookDirection.magnitude / maxSpeed));
            Animator.SetBool("Airborne", controller.stateMachine.CurrentState == controller.stateMachine.GetState<FIL_AirState>());
        }
    }
}
