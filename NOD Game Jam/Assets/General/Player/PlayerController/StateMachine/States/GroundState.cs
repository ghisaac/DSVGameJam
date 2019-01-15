using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[CreateAssetMenu(menuName = "Player/State/PlayerGroundState")]
public class GroundState : PlayerBaseState
{
    public float Acceleration;
    public float Friction;
    public float MaxSpeed;
    public float JumpForce;
    
    public override void StateUpdate()
    {
        Fric();
        Move();
        Velocity += Vector3.down * 10 * Time.deltaTime;
        Collide();
        if (RewierdPlayer.GetButtonDown("A"))
        {
            Velocity += Vector3.up * JumpForce;
            controller.animator.SetTrigger("Jump");
            StateMachine.TransitionToState<AirState>();
        }

        transform.position += controller.Velocity * Time.deltaTime;
    }

    private void Collide()
    {
        List<RaycastHit> allhits = PreventCollision();
        RaycastHit hit = CheckGround(allhits);
        if (hit.collider != null)
            controller.groundPlane = hit.normal;
        else
            StateMachine.TransitionToState<AirState>();
    }

    private RaycastHit CheckGround(List<RaycastHit> allhits)
    {
        foreach (RaycastHit hit in allhits)
            if (Vector3.Dot(hit.normal, Vector3.up) > .7f)
                return hit;
        return new RaycastHit();
    }

    private void Move()
    {
        Vector3 rawInput = new Vector3(RewierdPlayer.GetAxis("Horizontal"), 0f, RewierdPlayer.GetAxis("Vertical"));
        Vector3 movement = Vector3.ProjectOnPlane(rawInput, controller.groundPlane).normalized;

        controller.Velocity += movement * Acceleration * Time.deltaTime;
        if(Vector3.ProjectOnPlane(Velocity, controller.groundPlane).magnitude > MaxSpeed)
            controller.Velocity = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(controller.Velocity, controller.groundPlane), MaxSpeed) + Vector3.Project(controller.Velocity, controller.groundPlane);
    }

    private void Fric()
    {
        controller.Velocity = controller.Velocity.magnitude <= (Friction * Time.deltaTime) ? Vector3.zero : controller.Velocity - controller.Velocity.normalized * Friction * Time.deltaTime;
    }
}
