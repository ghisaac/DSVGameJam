using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_RotateObject : MonoBehaviour
{
    public float RotateY = 15f;
    public float RotateX = 15f;
    public float RotateZ = 15f;

    void Update () {
        transform.Rotate(Vector3.up, RotateY * Time.deltaTime);
        transform.Rotate(Vector3.right, (RotateX  * Time.deltaTime));
        transform.Rotate(Vector3.forward, (RotateZ * Time.deltaTime));

    }
}
