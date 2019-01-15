using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHS_Teacher : MonoBehaviour
{
    public GameObject[] Students;

    public float MinStudentActivateTime;
    public float MaxStudentActivateTime;

    private Coroutine StudentActivation;

    [SerializeField]
    private SpriteRenderer Icon;

    [SerializeField]
    private Sprite IdleIcon, AlertIcon, BustIcon;


    [HideInInspector]
    public bool teacherMad = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject PickStudent() {
        return Students[Random.Range(0, Students.Length)];
    }

    public IEnumerator ActiveStudentQuestion() {
        float delayTime = Random.Range(MinStudentActivateTime, MaxStudentActivateTime);
        yield return new WaitForSeconds(delayTime);
        GameObject student = PickStudent();
        //Activate student;

        //FLYTTA NEDAN TILL EGEN METOD


        yield return new WaitForSeconds(2f);
        Icon.sprite = AlertIcon;
        yield return new WaitForSeconds(1f);
        CheckIfBusted();
        teacherMad = true;
        //Animation till arg
        yield return new WaitForSeconds(2f); //TeacherActiveTime, balansera
        teacherMad = false;
        Icon.sprite = IdleIcon;
        //Teacher alert icon away
        StudentActivation = StartCoroutine(ActiveStudentQuestion());
 
    }




    private void CheckIfBusted() {
        foreach(HHS_Player player in HHS_GameManager.instance.activePlayers) {
            bool bustedSomeone = false;
            if (!player.IsHidden()) {
                player.Bust();
                Icon.sprite = BustIcon;
                bustedSomeone = true;
            }

            if (!bustedSomeone) {
                Icon.sprite = IdleIcon;
            }
            //Starta olika animationer beroende på om spelare hittas eller ej.
            //StudentActivation = StartCoroutine(ActiveStudentQuestion());
            //StopCoroutine(StudentActivation);
        }
    }


}
