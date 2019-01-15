using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/PreRound")]
    public class GGR_PreRound : GGR_State
    {
        public override bool Run()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
                Owner.TransitionTo<GGR_Round>();
            return false;
        }
    }
}