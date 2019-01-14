using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerController controller;
    protected Transform transform { get { return controller.transform; } }
    protected Vector2 Velocity { get { return controller.Velocity; } set { controller.Velocity = value; } }


    //Protected methods
    /// <summary>
    /// This method prevents the character from falling through the ground, walls and other players.
    /// </summary>
    /// <returns>The returned list can be used if you want to make checks on the hits, etc</returns>
    protected List<RaycastHit> PreventCollistion()
    {
        return PhysicsHelper.PreventCollision(CapsuleCast, ref controller.Velocity, transform, Time.deltaTime, 0.1f);
    }

    //Private methods
    private RaycastHit CapsuleCast()
    {
        float height = 10f;
        float radius = 1f;
        RaycastHit hitinfo;
        Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, Velocity.normalized, out hitinfo, float.MaxValue, controller.CollisionLayers);
        return hitinfo;
    }
}
