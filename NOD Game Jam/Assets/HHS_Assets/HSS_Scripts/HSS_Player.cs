using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSS_Player : MonoBehaviour
{
    private bool hidden = false;
    public Vector3 Startposition;
    public LayerMask Chairlayer;
    public int Points;
    private GameObject goal;

    public int PlayerID;



    public void SetGoal(GameObject newGoal) {
        goal = newGoal;
    }

    public GameObject GetGoal() {
        return goal;
    }


    public bool IsHidden() {
        return hidden;
    }

    public void Bust() {
        ResetPosition();
        //Animation
        //Ljud

    }

    public void ResetPosition() {
        transform.position = Startposition;
    }

    private void CheckForNearbyChair() {
        Collider[] colliderhits = Physics.OverlapSphere(transform.position, 10f, Chairlayer);
        float closestDistance = 9000f;
        if(colliderhits.Length > 0) {
            Collider chosenChair = null;
            foreach (Collider collider in colliderhits) {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance) {
                    chosenChair = collider;
                }
            }
            hidden = true;
            //Kör animation
            //Flytta position
            //Rotera?
            //Kolla mot målstol
            CheckIfAtGoal(chosenChair.gameObject);
            
        }

    }

    private void CheckIfAtGoal(GameObject chair) {
        if(chair == goal) {
            HSS_GameManager.instance.PlayerReachedGoal(PlayerID);
            //LOCK ME
        }
    }

    private void Start() {
        Startposition = transform.position;
    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (hidden) {
                //StartWalking()
                hidden = false;
            }
            else {
                CheckForNearbyChair();
            }
            
        }
    }

}
