using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[CreateAssetMenu(menuName = "Player/State/HHS_States/HHS_PlayerGroundState")]
public class HHS_GroundState : PlayerBaseState {
    public float Acceleration;
    public float Friction;
    public float MaxSpeed;
    public float JumpForce;
    public LayerMask ChairLayer;

    private bool hidden = false;

    private Vector3 groundPlane = Vector3.up;
    public override void StateUpdate() {

        Velocity += Vector3.down * 10 * Time.deltaTime;

        Fric();
        Move();
        Collide();
        if (RewierdPlayer.GetButtonDown("A")) {
            CheckForNearbyChair();
        }

        transform.position += controller.Velocity * Time.deltaTime;
    }

    private void CheckForNearbyChair() {
        Collider[] colliderhits = Physics.OverlapSphere(transform.position, 2f, ChairLayer);
        float closestDistance = 9000f;
        if (colliderhits.Length > 0) {
            Collider chosenChair = null;
            foreach (Collider collider in colliderhits) {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance) {
                    chosenChair = collider;
                }
            }
            hidden = true;
            controller.GetComponent<MeshRenderer>().material.color = Color.blue;
            //Kör animation
            transform.position = chosenChair.gameObject.transform.position + new Vector3(0, 0, 1);
            //Flytta position
            //transform.rotation = new Quaternion(
            //Rotera?
            //Kolla mot målstol
            CheckIfAtGoal(chosenChair.gameObject);

        }

    }

    private void CheckIfAtGoal(GameObject chair) {
        if (chair == controller.GetComponent<HHS_Player>().GetGoal()) {
            //HHS_GameManager.instance.PlayerReachedGoal();
            //TransitionToState<HHS_FrozenState>();
        }
        else {
            TransitionToState<HHS_SittingState>();
        }
    }


    private void Collide() {
        List<RaycastHit> allhits = PreventCollision();
        RaycastHit hit = CheckGround(allhits);
        if (hit.collider != null)
            groundPlane = hit.normal;
        else
            StateMachine.TransitionToState<HHS_AirState>();
    }

    private RaycastHit CheckGround(List<RaycastHit> allhits) {
        foreach (RaycastHit hit in allhits)
            if (Vector3.Dot(hit.normal, Vector3.up) > .7f)
                return hit;
        return new RaycastHit();
    }

    private void Move() {
        Vector3 rawInput = new Vector3(RewierdPlayer.GetAxis("Horizontal"), 0f, RewierdPlayer.GetAxis("Vertical"));
        Vector3 movement = Vector3.ProjectOnPlane(rawInput, groundPlane).normalized;

        controller.Velocity += movement * Acceleration * Time.deltaTime;
        if (Vector3.ProjectOnPlane(Velocity, groundPlane).magnitude > MaxSpeed)
            controller.Velocity = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(controller.Velocity, groundPlane), MaxSpeed) + Vector3.Project(controller.Velocity, groundPlane);
    }

    private void Fric() {
        controller.Velocity -= controller.Velocity.normalized * Friction * Time.deltaTime;
    }
}

