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
            Timer.StartTimer(roundTime);
            currentTime = roundTime;
            GGR_GameData.UnfreezeAllPlayers();
        }

        public override bool Run()
        {
            GGR_CameraMovement.instance.FollowPlayers();
            if(currentTime <= 0)
            {
                Owner.TransitionTo<GGR_PostRound>();
            }
            currentTime -= Time.deltaTime;
            return false;
        }
    }
}