using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/Round")]
    public class GGR_Round : GGR_State
    {
        public float roundTime;
        private float currentTime;

        public override void Enter()
        {
            currentTime = roundTime;
            //unfreeze playuhss
        }

        public override bool Run()
        {
            if(currentTime <= 0)
            {
                Owner.TransitionTo<GGR_PostRound>();
            }
            currentTime -= Time.deltaTime;
            return false;
        }
    }
}