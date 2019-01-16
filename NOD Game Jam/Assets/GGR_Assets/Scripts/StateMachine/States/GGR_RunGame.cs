using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/This")]
    public class GGR_RunGame : GGR_State
    {
        // Attributes
        public GGR_StateMachine StateMachine;

        public override void Enter()
        {
            StateMachine.Awake();
            base.Enter();
        }

        public override bool Run()
        {
            if (StateMachine.Run())
                Owner.TransitionTo<GGR_EndGame>();
            return false;
        }
    }
}