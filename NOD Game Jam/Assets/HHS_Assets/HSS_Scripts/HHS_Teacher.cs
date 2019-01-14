using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHS_Teacher : MonoBehaviour
{
    public GameObject[] Students;

    public float MinStudentActivateTime;
    public float MaxStudentActivateTime;

    private Coroutine StudentActivation;

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
        //Teacher alert icon
        yield return new WaitForSeconds(1f);
        GetComponent<MeshRenderer>().material.color = Color.red;
        CheckIfBusted();
        teacherMad = true;
        //Animation till arg
        yield return new WaitForSeconds(2f); //TeacherActiveTime, balansera
        teacherMad = false;
        //Animation tillbaka till idle
        //Teacher alert icon
        StudentActivation = StartCoroutine(ActiveStudentQuestion());
 
    }




    private void CheckIfBusted() {
        foreach(HSS_Player player in HSS_GameManager.instance.activePlayers) {
            if (!player.IsHidden()) {
                player.Bust();
            }
            //Starta olika animationer beroende på om spelare hittas eller ej.
            //StudentActivation = StartCoroutine(ActiveStudentQuestion());
            //StopCoroutine(StudentActivation);
        }
    }


}
