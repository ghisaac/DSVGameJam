using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_CameraFollow : MonoBehaviour
{
    public GameObject _player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float axis = _player.GetComponent<PlayerController>().stateMachine.GetState<KSR_RaceState>().getHorizontalAxis();

         transform.Rotate(_player.transform.position, axis);

        transform.position = _player.transform.position;
    }
}
