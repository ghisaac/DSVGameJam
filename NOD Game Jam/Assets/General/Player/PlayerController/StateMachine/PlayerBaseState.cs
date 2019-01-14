using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerController controller { get { return (PlayerController)StateMachine.owner; } }

    protected Transform transform { get { return controller.transform; } }
    protected Vector2 Velocity { get { return controller.Velocity; } set { controller.Velocity = value; } }
    protected Rewired.Player RewierdPlayer { get { return Rewired.ReInput.players.GetPlayer(controller.myPlayer.RewierdId); } }
}
