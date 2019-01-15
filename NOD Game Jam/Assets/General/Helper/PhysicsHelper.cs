using System;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsHelper
{
    /// <summary>
    /// This method stops the character from moving through collider in the specified layer mask and evaluates collision based on the given hit function.
    /// </summary>
    /// <param name="raycastFunction">Function that describe collision, has to be infinite distance and only return one hit.</param>
    /// <param name="velocity">A reference to the characters Velocity this will be changed by collisions</param>
    /// <param name="transform">Character reference to transform</param>
    /// <param name="DeltaTime">The deltatime the collision happens in</param>
    /// <param name="skinWidth">SkinWidth for security</param>
    /// <returns></returns>
    public static List<RaycastHit> PreventCollision(Func<RaycastHit> raycastFunction, ref Vector3 velocity, Transform transform, float DeltaTime, float skinWidth = 0.0f, float bounciness = 0.0f)
    {
        RaycastHit hit;
        List<RaycastHit> raycastHits = new List<RaycastHit>();
        while ((hit = raycastFunction()).collider != null)
        {
            float distanceToCorrect = skinWidth / Vector3.Dot(velocity.normalized, hit.normal);
            float distanceToMove = hit.distance + distanceToCorrect;

            if (distanceToMove <= velocity.magnitude * DeltaTime)
            {
                raycastHits.Add(hit);
                if (distanceToMove > 0.0f)
                    transform.position += velocity.normalized * distanceToMove;
                velocity += CalculateNormalForce(hit.normal, velocity) * (1.0f + bounciness);
            }
            else
                break;
        }
        return raycastHits;
    }

    /// <summary>
    /// Return the normal force of a described collision
    /// </summary>
    /// <param name="normal">The plane of intersection</param>
    /// <param name="velocity">The velocity that the collision happens with</param>
    /// <returns></returns>
    public static Vector3 CalculateNormalForce(Vector3 normal, Vector3 velocity)
    {
        float dot = Vector3.Dot(velocity, normal.normalized);
        return -normal.normalized * (dot > 0.0f ? 0 : dot);
    }

}