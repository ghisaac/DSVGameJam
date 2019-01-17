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

    public override void StateUpdate() {
        Fric();
        Move();
        Velocity += Vector3.down * 10 * Time.deltaTime;
        Collide();
        if (RewierdPlayer.GetButtonDown("A")) {

            CheckForNearbyChair();
        }

        transform.position += controller.Velocity * Time.deltaTime;
        AnimationUpdate();
    }

    private void AnimationUpdate()
    {
        Vector3 newlookDirection = Vector3.ProjectOnPlane(controller.Velocity, controller.groundPlane);
        Vector3 lookDirection = Vector3.zero;
        if (newlookDirection.magnitude > 1f)
            lookDirection = newlookDirection;
        Quaternion wantToLook = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, wantToLook, 0.1f);
        controller.animator.SetFloat("Velocity", Mathf.Clamp(lookDirection.magnitude / MaxSpeed, 0, 0.8f));
        controller.animator.SetBool("Airborne", controller.stateMachine.CurrentState == controller.stateMachine.GetState<AirState>());
    }

    private void Collide() {
        List<RaycastHit> allhits = PreventCollision();
        RaycastHit hit = CheckGround(allhits);
        if (hit.collider != null)
            controller.groundPlane = hit.normal;
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
        Vector3 rawInput = new Vector3(RewierdPlayer.GetAxis("Vertical"), 0f, -RewierdPlayer.GetAxis("Horizontal"));
        Vector3 movement = Vector3.ProjectOnPlane(rawInput, controller.groundPlane).normalized;

        controller.Velocity += movement * Acceleration * Time.deltaTime;
        if (Vector3.ProjectOnPlane(Velocity, controller.groundPlane).magnitude > MaxSpeed)
            controller.Velocity = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(controller.Velocity, controller.groundPlane), MaxSpeed) + Vector3.Project(controller.Velocity, controller.groundPlane);
    }

    private void Fric() {
        controller.Velocity = controller.Velocity.magnitude <= (Friction * Time.deltaTime) ? Vector3.zero : controller.Velocity - controller.Velocity.normalized * Friction * Time.deltaTime;
    }



           

    private void CheckForNearbyChair() {
        Collider[] colliderhits = Physics.OverlapSphere(transform.position, 0.5f, ChairLayer);
        float closestDistance = 9000f;
        if (colliderhits.Length > 0) {
            Collider chosenChair = null;
            foreach (Collider collider in colliderhits) {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance) {
                    chosenChair = collider;
                }
            }
            //Kör animation
            transform.position = new Vector3(chosenChair.gameObject.transform.position.x - 1f, transform.position.y, chosenChair.gameObject.transform.position.z);
           // chosenChair.GetComponentInChildren<Animator>().SetBool("Pulled Out", true);
            //transform.rotation = new Quaternion(
            //Rotera?
            //Kolla mot målstol
            CheckIfAtGoal(chosenChair.gameObject);

        }

    }

    private void CheckIfAtGoal(GameObject chair) {
        if (chair == controller.GetComponent<HHS_Player>().GetGoal()) {
            HHS_GameManager.instance.PlayerReachedGoal(controller.GetComponent<HHS_Player>().PlayerID);
            TransitionToState<HHS_FrozenState>();

        }
        else {
            TransitionToState<HHS_SittingState>();
        }
    }
}


