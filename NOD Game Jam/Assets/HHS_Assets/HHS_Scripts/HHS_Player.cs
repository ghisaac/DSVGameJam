using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHS_Player : MonoBehaviour
{
    public Vector3 Startposition;
    public LayerMask Chairlayer;
    public int Points;
    private GameObject goal;
    [HideInInspector]
    public SpriteRenderer goalindicator;

    public int PlayerID;


    public void SetGoal(GameObject newGoal) {
        goal = newGoal;
        goalindicator.transform.position = newGoal.transform.position + new Vector3(2,0,0);
    }

    public GameObject GetGoal() {
        return goal;
    }

    public void Bust() {
        ResetPosition();
        //Animation
        //Ljud

    }

    public void ResetPosition() {
        transform.position = Startposition;
    }

    //private void CheckForNearbyChair() {
    //    Collider[] colliderhits = Physics.OverlapSphere(transform.position, 2f, Chairlayer);
    //    float closestDistance = 9000f;
    //    if(colliderhits.Length > 0) {
    //        Collider chosenChair = null;
    //        foreach (Collider collider in colliderhits) {
    //            float distance = Vector3.Distance(transform.position, collider.transform.position);
    //            if (distance < closestDistance) {
    //                chosenChair = collider;
    //            }
    //        }
    //        hidden = true;
    //        GetComponent<MeshRenderer>().material.color = Color.blue;
    //        //Kör animation
    //        transform.position = chosenChair.gameObject.transform.position + new Vector3(0, 0, 1);
    //        //Flytta position
    //        //transform.rotation = new Quaternion(
    //        //Rotera?
    //        //Kolla mot målstol
    //        CheckIfAtGoal(chosenChair.gameObject);
            
    //    }

    //}

    //private void CheckIfAtGoal(GameObject chair) {
    //    if(chair == goal) {
    //        HHS_GameManager.instance.PlayerReachedGoal(PlayerID);
    //        //LOCK ME
    //    }
    //}

    private void Start() {
        Startposition = transform.position;
    }


    // Update is called once per frame
    //void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        if (hidden) {
    //            //StartWalking()
    //            hidden = false;
    //            GetComponent<MeshRenderer>().material.color = Color.white;
    //        }
    //        else {
    //            CheckForNearbyChair();
    //        }
            
    //    }
    //}

}
