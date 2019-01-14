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

    private Vector3 groundPlane = Vector3.up;
    public override void StateUpdate()
    {
        if(!GetGround())
        {
            //Switch to air
            return;
        }
        Move();
        PreventCollision();
        transform.position += Velocity * Time.deltaTime;
    }

    private bool GetGround()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, .5f, controller.CollisionLayers);
        groundPlane = hit.normal;

        return hit.collider != null;
    }

    private void Move()
    {
        
        Vector3 rawInput = new Vector3(RewierdPlayer.GetAxis("Horizontal"), 0f, RewierdPlayer.GetAxis("Vertical"));
        Vector3 movement = Vector3.ProjectOnPlane(rawInput, groundPlane).normalized;
        Velocity += movement * Acceleration * Time.deltaTime;
    }
}
