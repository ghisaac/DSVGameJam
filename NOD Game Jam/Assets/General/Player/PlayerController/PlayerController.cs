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

    [Header("DEBUGGING")]
    public bool TestPlayer;
    private static int testPlayerIdCounter = 0;

    [HideInInspector] public int myTestPlayerId = -1;
    [HideInInspector] public Player myPlayer;

    void Awake()
    {
        stateMachine.Initialize(this);
        if (TestPlayer)
            myTestPlayerId = testPlayerIdCounter++;
    }


    void Update()
    {
        stateMachine.Update();
        this.transform.position += Velocity * Time.deltaTime;
    }
}
