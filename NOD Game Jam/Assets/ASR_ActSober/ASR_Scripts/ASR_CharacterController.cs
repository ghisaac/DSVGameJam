using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ASR_CharacterController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [HideInInspector]
    public Animator CharacterAnimator;
    private float _horizontal;
    public float Force = 20f;
    private bool _isKnockedOut;
    public Player Player;

    private float _inputModifier = 1f;
    private const float INPUT_MODIFIER_MIN = 1F;
    private Vector3 StartPosition;

    public Transform RaycastTransform;
    public Transform ForcePositionTransform;
    public Transform BurpTransform;
    public LayerMask FloorMask;
    public float RayMaxDistance = 2;

    public float InputModifierModifier = 0.5f;

    public int Score = 0;
    
    //public TextMeshProUGUI ScoreUI;

    [Header("DEBUGGING")]
    public bool UseKeyboard;


	public void Initialize()
	{
        Debug.Log("Initialize");
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        CharacterAnimator = GetComponent<Animator>();
        StartPosition = transform.position;
        enabled = false;
	}

	private void Update()
    {
        // Debugging
        if (Input.GetKeyDown(KeyCode.R))
        {

            OnRespawn();
        }

        InputModification();
        Move();
        ClampPosition();
        CheckHeadCollision();


    }

    private void Move()
    {
        if (UseKeyboard){
            _horizontal = Input.GetAxisRaw("Horizontal");
        } else {
            _horizontal = Rewired.ReInput.players.GetPlayer(0).GetAxisRaw("Horizontal");
        }

        //_horizontal = Player.Input.GetAxisRaw("Horizontal");

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
            _inputModifier += Time.deltaTime * InputModifierModifier;
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
            //Debug.Log("You ded");
            OnKnockout();
        }
        

        
    }

    private void OnKnockout()
    {
        //kalla på metod i gamemanager
        //_isKnockedOut = true;
        SoundManager.Instance.PlayCollidePlayer();
        ASR_GameManager.Instance.PlayerKnockedOut(this);
        Deactivate();

        //Kom ihåg att ta bort kommentar!!!
        //this.enabled = false;
    }

    public void Deactivate(){
        this.enabled = false;
        _isKnockedOut = true;
    }

    public void OnRespawn()
    {
        _rigidbody.isKinematic = true;
        //this.enabled = true;
        transform.SetPositionAndRotation(StartPosition, Quaternion.identity);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        
    }

    public void ActivatePlayer()
    {
        this.enabled = true;
        _isKnockedOut = false;
        _rigidbody.isKinematic = false;
    }

    public void SetStartPosition(Vector3 startPos)
    {
        StartPosition = startPos;
    }

    public void AddScore(int awardedScore)
    {
        Score += awardedScore;
    }

}
