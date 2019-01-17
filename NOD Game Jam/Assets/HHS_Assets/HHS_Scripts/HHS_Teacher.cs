using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHS_Teacher : MonoBehaviour
{
    public GameObject[] Students;

    public float MinStudentActivateTime;
    public float MaxStudentActivateTime;

    public Coroutine StudentActivation;
    private Animator animator;
    private Animator studentAnimator;
    public Coroutine RaiseHandRoutine;

    [SerializeField]
    private SpriteRenderer Icon;

    [SerializeField]
    private Sprite IdleIcon, AlertIcon, BustIcon;

    [HideInInspector]
    public bool HandRaised = false;

    [HideInInspector]
    public bool teacherMad = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StopStudent() {
            StopCoroutine(StudentActivation);


    }

    public GameObject PickStudent() {
        return Students[Random.Range(0, Students.Length)];
    }

    public IEnumerator ActiveStudentQuestion() {
        GameObject student = PickStudent();
        studentAnimator = student.GetComponentInChildren<Animator>();
        ResetStudents();
        ResetPlayerRaisedHand();
        float delayTime = Random.Range(MinStudentActivateTime, MaxStudentActivateTime);
        yield return new WaitForSeconds(delayTime);
        
        student.GetComponentInChildren<SpriteRenderer>().enabled = true;
        //Activate student;
        //Animate student
        //Speech bubble over student
        //FLYTTA NEDAN TILL EGEN METOD

        if (!HandRaised)
        {
            RaiseHandRoutine = StartCoroutine(RaiseHand());
        }
        else
        {
            StopCoroutine(RaiseHand());
        }



 
    }

    private void ResetStudents() {
        foreach(GameObject student in Students) {
            student.GetComponentInChildren<SpriteRenderer>().enabled = false;

        }

    }

    private void ResetPlayerRaisedHand() {
        foreach (HHS_Player player in HHS_GameManager.instance.activePlayers) {
            player.GetComponentInChildren<SpriteRenderer>().enabled = false;
       
        }
    }

    public IEnumerator RaiseHand() {
       SoundManager.Instance.PlayStudentDemandHelp();   
        HandRaised = true;
        studentAnimator.SetBool("Raised Hand", true);
        yield return new WaitForSeconds(1f);
        Icon.sprite = AlertIcon;
        yield return new WaitForSeconds(1f);
        // transform.eulerAngles = new Vector3(0, 270, 0);
        animator.SetBool("Turn Around", true);
        Debug.Log("turn");
        CheckIfBusted();
        teacherMad = true;
       SoundManager.Instance.PlayTeacherAlerted();   
        //Animation till arg
        animator.SetTrigger("Bust");
        yield return new WaitForSeconds(1f); //TeacherActiveTime, balansera
      //  transform.eulerAngles = new Vector3(0, 90, 0);
        teacherMad = false;
        //Teacher alert icon away
      SoundManager.Instance.PlayTeacherIdle();  
        animator.SetBool("Turn Around", false);
        StartCoroutine(WaitForAnimationAndSetSprite(1f, IdleIcon));
       // animator.ResetTrigger("Bust");
        HandRaised = false;
        studentAnimator.SetBool("Raised Hand", false);
        StartStudent();
        yield return 0;
    }

    public void StartStudent() {
        StudentActivation = StartCoroutine(ActiveStudentQuestion());
    }

    public void StartRaiseHand() {
        RaiseHandRoutine = StartCoroutine(RaiseHand());
    }

    public IEnumerator WaitForAnimationAndSetSprite(float waitTime, Sprite sprite)
    {
      
        yield return new WaitForSeconds(waitTime);
        Icon.sprite = sprite;
    }

    public IEnumerator WaitForAnimationAndBust(float waitTime, HHS_Player player)
    {

        yield return new WaitForSeconds(waitTime);
        player.Bust();
    }


    private void CheckIfBusted() {
        foreach(HHS_Player player in HHS_GameManager.instance.activePlayers) {
            bool bustedSomeone = false;
            player.GetComponentInChildren<Animator>().SetBool("Raised Hand", false);
            if (player.GetComponent<PlayerController>().stateMachine.CurrentState is HHS_GroundState) {
                player.GetComponentInChildren<Animator>().SetTrigger("Busted");
                player.GetComponent<HHS_Player>().TriggerParticles();
                StartCoroutine(WaitForAnimationAndBust(1.5f, player));
                Icon.sprite = BustIcon;
                bustedSomeone = true;

               
            }

            if (!bustedSomeone) {
                Icon.sprite = IdleIcon;
            }
            else {
               SoundManager.Instance.PlayCaught(); 
            }
            //Starta olika animationer beroende på om spelare hittas eller ej.
            //StudentActivation = StartCoroutine(ActiveStudentQuestion());
            //StopCoroutine(StudentActivation);
        }
    }


}
