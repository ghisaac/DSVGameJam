using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerController controller { get { return (PlayerController)StateMachine.owner; } }
    protected Transform transform { get { return controller.transform; } }
    protected Vector3 Velocity { get { return controller.Velocity; } set { controller.Velocity = value; } }
    protected Rewired.Player RewierdPlayer { get { return Rewired.ReInput.players.GetPlayer(controller.myTestPlayerId >= 0 ? controller.myTestPlayerId : controller.myPlayer.RewierdId); } }

    //Protected methods
    /// <summary>
    /// This method prevents the character from falling through the ground, walls and other players.
    /// </summary>
    /// <returns>The returned list can be used if you want to make checks on the hits, etc</returns>
    protected List<RaycastHit> PreventCollision()
    {
        return PhysicsHelper.PreventCollision(() => { return CapsuleCollision(ref controller.Velocity, controller.Collider); }, ref controller.Velocity, transform, Time.deltaTime, controller.skinWidth);
    }

    //Private methods
    private RaycastHit CapsuleCollision(ref Vector3 velocity, CapsuleCollider collider, float distance = float.MaxValue)
    {
        float height = collider.height / 2 - collider.radius;
        Vector3 CapsulePoint1 = transform.position + Vector3.up * height + collider.center;
        Vector3 CapsulePoint2 = transform.position + Vector3.down * height + collider.center;
        RaycastHit hit;
        Physics.CapsuleCast(CapsulePoint1, CapsulePoint2, collider.radius, velocity.normalized, out hit, distance, controller.CollisionLayers);
        return hit;
    }
}
