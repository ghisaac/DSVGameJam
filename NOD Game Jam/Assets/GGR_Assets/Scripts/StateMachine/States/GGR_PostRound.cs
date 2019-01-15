using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/PostRound")]
    public class GGR_PostRound : GGR_State
    {
        public override void Enter()
        {
            GGR_GameData.DequeueCurrentLocation();
            
        }
        public override bool Run()
        {
            if (GGR_GameData.GetLocationsRemaining() == 0)
            {
                return true;
            }
            else
            {
                Owner.TransitionTo<GGR_PreRound>();
                return false;
            }
        }
    }
}
