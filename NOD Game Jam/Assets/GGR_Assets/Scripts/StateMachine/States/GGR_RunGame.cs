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
        private GGR_StateMachine myMachine;

        public override void Enter()
        {
            myMachine = Instantiate(StateMachine);
        }

        public override bool Run()
        {
            if (myMachine.Run())
                Owner.TransitionTo<GGR_EndGame>();
            return false;
        }
    }
}