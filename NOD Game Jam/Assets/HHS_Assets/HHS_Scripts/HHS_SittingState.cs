using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[CreateAssetMenu(menuName = "Player/State/HHS_States/HHS_PlayerSittingState")]
public class HHS_SittingState : PlayerBaseState
{

    

    public override void StateUpdate() {

        controller.transform.eulerAngles = new Vector3(0, 90, 0);

        if (RewierdPlayer.GetButtonDown("A")) {
            TransitionToState<HHS_AirState>();
        }

        if (RewierdPlayer.GetButtonDown("X")) {
            if(!HHS_GameManager.instance.Teacher.HandRaised) {
                HHS_GameManager.instance.Teacher.StopStudent();
                HHS_GameManager.instance.Teacher.StartRaiseHand();
                controller.GetComponentInChildren<SpriteRenderer>().enabled = true;

    
                controller.animator.SetBool("Raised Hand", true);
                
                //Animate player
                Debug.Log("RAISING HAND!");
            }
            else {
                Debug.Log("CANT RAISE IT!");
            }

        }

    }

    public override void Enter() {
        base.Enter();
        Velocity = Vector3.zero;
        controller.animator.SetFloat("Velocity", 0);
      //  SoundManager.Instance.PlaySitDown();  
    }

    public override void Exit() {
        base.Exit();
        controller.GetComponentInChildren<SpriteRenderer>().enabled = false;
        controller.animator.SetBool("Raised Hand", false);
 
    }
}
