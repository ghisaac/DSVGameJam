using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public StateMachine stateMachine;
    public Vector3 Velocity;
    public LayerMask CollisionLayers;
    [SerializeField] CapsuleCollider collider;
    public CapsuleCollider Collider { get { return collider; } private set { } }
    public float skinWidth;
    [HideInInspector] public Player myPlayer;

    void Awake()
    {
        stateMachine.Initialize(this);
    }


    void Update()
    {
        stateMachine.Update();
        this.transform.position += Velocity * Time.deltaTime;
    }
}
