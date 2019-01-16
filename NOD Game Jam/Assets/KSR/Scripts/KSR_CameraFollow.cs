using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_CameraFollow : MonoBehaviour
{

    public Transform player;
    public float cameraDistance = 3.0f;
    public float cameraHeight = 2.5f;
    public float followDelay = 1.2f;
    public float rotationDelay = 1.8f;
    public float maxDistance = 10f;
    public bool delayCam = false;
    public float timeToBeDelayed = 1f;
    private Vector3 _followPos;
    private Quaternion followRotation;
    private float delayTime = 0f;
    private float _increaseDelay = 0f;

    private void Awake()
    {
        _increaseDelay = followDelay;
    }
    void Update()
    {
        _followPos = player.TransformPoint(0, cameraHeight, Mathf.Clamp(-cameraDistance, -maxDistance, 0));
        transform.position = _followPos;
        if (!delayCam)
        {
            transform.position = _followPos;

        }
        else
        {
            CameraDelay();
        }
      //  followRotation = Quaternion.LookRotation(player.position - transform.position, player.up);
        transform.LookAt(player);
    }


    public void CameraDelay() //delay vid collision
    {

        if (delayTime >= timeToBeDelayed)// tidsbaserat att kameran åker tillbaka
        {
            delayCam = false;
            delayTime = 0;

        }
        if (_increaseDelay >= 1f)// ifall Lerpen är 1 så lerpar den 100% och bör avslutas
        {
            delayCam = false;
            delayTime = 0;
            _increaseDelay = followDelay;
        }
        transform.position = Vector3.Lerp(transform.position, _followPos, _increaseDelay);
        //transform.position = Vector3.Lerp(transform.position, _followPos, followDelay * Time.deltaTime);
        delayTime += Time.deltaTime;
        _increaseDelay += 0.001f; //öka på hur mycket vector3 Lerpar
        Debug.Log("inne i delay");

    }


}




