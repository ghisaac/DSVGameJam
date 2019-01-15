using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/Round")]
    public class GGR_Round : GGR_State
    {
        public override bool Run()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
                Owner.TransitionTo<GGR_PostRound>();
            return false;
        }
    }
}