using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public StateMachine stateMachine;
    public Vector3 Velocity;
    public LayerMask CollisionLayers;

    void Awake()
    {
        stateMachine.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
