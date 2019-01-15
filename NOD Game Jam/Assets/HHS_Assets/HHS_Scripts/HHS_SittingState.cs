using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[CreateAssetMenu(menuName = "Player/State/HHS_States/HHS_PlayerSittingState")]
public class HHS_SittingState : PlayerBaseState
{
    public override void StateUpdate() {


        if (RewierdPlayer.GetButtonDown("A")) {
            TransitionToState<HHS_AirState>();
        }

    }

    public override void Enter() {
        base.Enter();
        Velocity = Vector3.zero;
    }
}
