using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GGR_Helper 
{
    public static float GetPathLength(Vector3[] pathCorners)
    {
        float sum = 0;
        for(int i = 0; i < pathCorners.Length-1; i++)
        {
            sum += Vector3.Distance(pathCorners[i], pathCorners[i + 1]);
        }
        return sum;
    }

    public static Vector3 PositionOnPlane(Vector3 position)
    {
        position.y = 0;
        return position;
    }

    public static Vector3? GetPathPositionByFactor(float factor, Vector3[] path, out int corners)
    {
        float totalLength = GetPathLength(path);
        float remainingDistance = totalLength * factor;
        corners = 0;
        for(int i = 0; i < path.Length-1; i++)
        {
            corners = i+1;
            float distance = Vector3.Distance(path[i], path[i + 1]);
            if (remainingDistance > distance)
            {
                remainingDistance -= distance;
                continue;
            }
            return path[i] + (path[i + 1] - path[i]).normalized * remainingDistance;
        }
        return null;
    }
}
