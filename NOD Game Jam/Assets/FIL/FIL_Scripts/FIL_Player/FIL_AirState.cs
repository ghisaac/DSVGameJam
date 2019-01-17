using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FIL;


/*
 *Kopia av andras kod. Skapad för att fungera i våran scen med små ändraingar.
 */
[CreateAssetMenu(menuName = "Player/State/FIL_PlayerAirState")]
public class FIL_AirState : FIL_PlayerBaseState
{
    public float Acceleration;
    public float Friction;
    public float MaxSpeed;
    public float Gravity;
    public float JumpForce;
    public int maxJumps = 1;
    public int jumpsPreformed = 0;

    public override void StateUpdate()
    {
        Velocity += Vector3.down * Gravity * Time.deltaTime;
        Fric();
        Move();
        Collide();

        //Detta är tillagt för att tillåta spelaren att hoppa flera gånger i luften.
        if (RewierdPlayer.GetButtonDown("A") && jumpsPreformed < maxJumps)
        {
            jumpsPreformed++;
            Velocity += Vector3.up * JumpForce;
            controller.animator.SetTrigger("Jump");
            SoundManager.Instance.PlayPlayerJump();
        }
        //slut här.
        transform.position += controller.Velocity * Time.deltaTime;
    }

    private void Collide()
    {
        List<RaycastHit> allhits = PreventCollision();
        RaycastHit hit = CheckGround(allhits);
        if (hit.collider != null)
        {
            jumpsPreformed = 0;
            StateMachine.TransitionToState<FIL_GroundState>();
            SoundManager.Instance.PlayPlayerLand();
        }
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
        Vector3 movement = Vector3.ProjectOnPlane(rawInput, Vector3.up).normalized;

        controller.Velocity += movement * Acceleration * Time.deltaTime;
        if (Vector3.ProjectOnPlane(Velocity, Vector3.up).magnitude > MaxSpeed)
            controller.Velocity = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(controller.Velocity, Vector3.up), MaxSpeed) + Vector3.Project(controller.Velocity, Vector3.up);
    }

    private void Fric()
    {
        controller.Velocity = controller.Velocity.magnitude <= (Friction * Time.deltaTime) ? Vector3.zero : controller.Velocity - controller.Velocity.normalized * Friction * Time.deltaTime;
    }
}

