using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerController controller;
    protected Transform transform { get { return controller.transform; } }
    protected Vector2 Velocity { get { return controller.Velocity; } set { controller.Velocity = value; } }
    public override void Initialize(object owner)
    {
        controller = (PlayerController)owner;
    }

}
