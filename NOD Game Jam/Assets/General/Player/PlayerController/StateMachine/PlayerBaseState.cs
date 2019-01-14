﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerController controller { get { return (PlayerController)StateMachine.owner; } }
    protected Transform transform { get { return controller.transform; } }
    protected Vector3 Velocity { get { return controller.Velocity; } set { controller.Velocity = value; } }
    protected Rewired.Player RewierdPlayer { get { return Rewired.ReInput.players.GetPlayer(0); } }

    //Protected methods
    /// <summary>
    /// This method prevents the character from falling through the ground, walls and other players.
    /// </summary>
    /// <returns>The returned list can be used if you want to make checks on the hits, etc</returns>
    protected List<RaycastHit> PreventCollision()
    {
        return PhysicsHelper.PreventCollision(CapsuleCast, ref controller.Velocity, transform, Time.deltaTime, 0.1f);
    }

    //Private methods
    private RaycastHit CapsuleCast()
    {
        float height = controller.Collider.height;
        float radius = controller.Collider.radius;
        RaycastHit hitinfo;
        Physics.CapsuleCast(controller.Collider.center + Vector3.down * height / 2f, transform.position + Vector3.up * height / 2f, radius, Velocity.normalized, out hitinfo, float.MaxValue, controller.CollisionLayers);
        return hitinfo;
    }

    
}
