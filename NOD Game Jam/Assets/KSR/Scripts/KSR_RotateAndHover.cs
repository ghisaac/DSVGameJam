using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_RotateAndHover : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationAngle;
    public float rotationSpeed;
    [Header("Hover")]
    public float hoverSpeed;
    public float hoverHeight;


    void Awake()
    {
        transform.Rotate(new Vector3(rotationAngle, 0f, 0f));
    }


    void Update()
    {
        Move();
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        float y = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + y, 0f, 10f), transform.position.z);

    }

   

}
