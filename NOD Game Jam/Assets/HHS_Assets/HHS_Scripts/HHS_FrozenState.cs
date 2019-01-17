using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/State/HHS_States/HHS_PlayerFrozenState")]
public class HHS_FrozenState : PlayerBaseState
{
    public override void Enter()
    {
        controller.transform.eulerAngles = new Vector3(0, 90, 0);
    }
}
