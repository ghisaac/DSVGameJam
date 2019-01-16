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
    public Animator animator;
    public Vector3 groundPlane = Vector3.up;
    [HideInInspector] public Player myPlayer;

    void Awake()
    {
        stateMachine.Initialize(this);
    }

    public void CreatePlayerController(Player player)
    {
        myPlayer = player;
        //Here you can also choose where this player should be positioned.
    }

    void Update()
    {
        stateMachine.Update();
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
