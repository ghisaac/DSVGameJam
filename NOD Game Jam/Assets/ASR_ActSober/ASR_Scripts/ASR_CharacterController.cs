using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_CharacterController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _horizontal;
    public float Force = 20f;
    private bool _isKnockedOut;
    public Player Player;

    private float _inputModifier = 1f;
    private const float INPUT_MODIFIER_MIN = 1F;
    private Vector3 StartPosition;

    public Transform RaycastTransform;
    public Transform ForcePositionTransform;
    public LayerMask FloorMask;
    public float RayMaxDistance = 2;

    [Header("DEBUGGING")]
    public bool UseKeyboard;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StartPosition = transform.position;
    }

    private void Update()
    {
        InputModification();
        Move();
        ClampPosition();
        CheckHeadCollision();

        if (Input.GetKeyDown(KeyCode.R))
        {
            
            OnRespawn();
        }
    }

    private void Move()
    {
        if (UseKeyboard){
            _horizontal = Input.GetAxisRaw("Horizontal");
        } else {
            _horizontal = Rewired.ReInput.players.GetPlayer(0).GetAxisRaw("Horizontal");
        }

        //Vector3 direction = -transform.right * _horizontal;
        //_rigidbody.AddForce(direction * Force * _inputModifier, ForceMode.Acceleration);

        Vector3 direction = -ForcePositionTransform.right * _horizontal;
        _rigidbody.AddForceAtPosition(direction * Force * _inputModifier, ForcePositionTransform.position, ForceMode.Acceleration);
    }

    private void ClampPosition()
    {
        Vector3 tempVec = transform.position;
        tempVec.x = Mathf.Clamp(tempVec.x, StartPosition.x - 1.5f, StartPosition.x + 1.5f);
        transform.position = tempVec;
    }

    private void InputModification(){

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0){
            _inputModifier += Time.deltaTime;
        } else {
            _inputModifier = INPUT_MODIFIER_MIN; //Mathf.Clamp(_inputModifier - Time.deltaTime, INPUT_MODIFIER_MIN, _inputModifier);
        }
        
    }

    public void AddForce(Vector3 force)
    {
        if (!_isKnockedOut)
        {
            _rigidbody.AddForceAtPosition(force, ForcePositionTransform.position, ForceMode.Acceleration);
        }

    }

    private void CheckHeadCollision()
    {
        RaycastHit leftSide, rightSide;

        Debug.DrawRay(RaycastTransform.position, -transform.right * RayMaxDistance, Color.red);
        Debug.DrawRay(RaycastTransform.position, transform.right * RayMaxDistance, Color.green);
        if (Physics.Raycast(RaycastTransform.position, -transform.right, out leftSide, RayMaxDistance, FloorMask) || Physics.Raycast(RaycastTransform.position, transform.right, out rightSide, RayMaxDistance, FloorMask))
        {
            Debug.Log("You ded");
            OnKnockout();
        }
        

        
    }

    private void OnKnockout()
    {
        //kalla på metod i gamemanager
        _isKnockedOut = true;

        //Kom ihåg att ta bort kommentar!!!
        //this.enabled = false;
    }

    public void OnRespawn()
    {
        //this.enabled = true;
        transform.SetPositionAndRotation(StartPosition, Quaternion.identity);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        
    }

    public void ActivatePlayer()
    {
        this.enabled = true;
        _isKnockedOut = false;
    }

    public void SetStartPosition(Vector3 startPos)
    {
        StartPosition = startPos;
    }

}
