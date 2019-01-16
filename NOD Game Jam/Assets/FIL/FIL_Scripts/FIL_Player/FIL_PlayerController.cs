using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FIL;

public class FIL_PlayerController : MonoBehaviour
{

    public StateMachine stateMachine;
    public Vector3 Velocity;
    public LayerMask CollisionLayers;
    [SerializeField] CapsuleCollider collider;
    public CapsuleCollider Collider { get { return collider; } private set { } }
    public float skinWidth;
    public Animator animator;
    public Vector3 groundPlane = Vector3.up;

    public int lives = 3;
    public GameObject[] hearts;

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

    public void CreatePlayerController(Player player)
    {
        myPlayer = player;
    }

    void Update()
    {
        stateMachine.Update();
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }



    public void LoseLife()
    {
        lives--;

        if(lives < 0)
        {
            FIL_GameManager.instance.PlayerDeath(gameObject);
        }
        else
        {
            hearts[lives].SetActive(false);
            FIL_GameManager.instance.RespawnPlayer(this);
        }

    }
}
