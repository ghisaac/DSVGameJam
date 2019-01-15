using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHS_Teacher : MonoBehaviour
{
    public GameObject[] Students;

    public float MinStudentActivateTime;
    public float MaxStudentActivateTime;

    public Coroutine StudentActivation;
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
        
    }

    public void StopStudent() {
            StopCoroutine(StudentActivation);


    }

    public GameObject PickStudent() {
        return Students[Random.Range(0, Students.Length)];
    }

    public IEnumerator ActiveStudentQuestion() {
        GameObject student = PickStudent();
        ResetStudents();
        ResetPlayerRaisedHand();
        float delayTime = Random.Range(MinStudentActivateTime, MaxStudentActivateTime);
        yield return new WaitForSeconds(delayTime);
        
        student.GetComponentInChildren<SpriteRenderer>().enabled = true;
        //Activate student;
        //Animate student
        //Speech bubble over student
        //FLYTTA NEDAN TILL EGEN METOD
        RaiseHandRoutine = StartCoroutine(RaiseHand());


 
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
        //SoundManager.Instance.PlayStudentDemandHelp();   LJUD HÄR
        HandRaised = true;
        yield return new WaitForSeconds(1f);
        Icon.sprite = AlertIcon;
        yield return new WaitForSeconds(1f);
        transform.eulerAngles = new Vector3(0, 270, 0);
        CheckIfBusted();
        teacherMad = true;
        //SoundManager.Instance.PlayTeacherAlerted();   LJUD KOMMER HÄR
        //Animation till arg
        yield return new WaitForSeconds(2f); //TeacherActiveTime, balansera
        transform.eulerAngles = new Vector3(0, 90, 0);
        teacherMad = false;
        Icon.sprite = IdleIcon;
        //Teacher alert icon away
        //SoundManager.Instance.PlayTeacherIdle();   LJUD KOMMER HÄR
        HandRaised = false;
        StartStudent();
        yield return 0;
    }

    public void StartStudent() {
        StudentActivation = StartCoroutine(ActiveStudentQuestion());
    }

    public void StartRaiseHand() {
        RaiseHandRoutine = StartCoroutine(RaiseHand());
    }


    private void CheckIfBusted() {
        foreach(HHS_Player player in HHS_GameManager.instance.activePlayers) {
            bool bustedSomeone = false;
            if (player.GetComponent<PlayerController>().stateMachine.CurrentState is HHS_GroundState) {
                player.Bust();
                Icon.sprite = BustIcon;
                bustedSomeone = true;
            }

            if (!bustedSomeone) {
                Icon.sprite = IdleIcon;
            }
            else {
                //SoundManager.Instance.PlayCaught(); LJUD HÄR
            }
            //Starta olika animationer beroende på om spelare hittas eller ej.
            //StudentActivation = StartCoroutine(ActiveStudentQuestion());
            //StopCoroutine(StudentActivation);
        }
    }


}
