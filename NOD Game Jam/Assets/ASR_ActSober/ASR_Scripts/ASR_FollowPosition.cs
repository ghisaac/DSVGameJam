using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_FollowPosition : MonoBehaviour
{

    public Transform FollowTransform;

    public Vector3 Offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = FollowTransform.position + Offset;
    }
}
