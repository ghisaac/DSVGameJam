using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/PostRound")]
    public class GGR_PostRound : GGR_State
    {
        public override bool Run()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
                Owner.TransitionTo<GGR_PreRound>();
            if (Input.GetKeyDown(KeyCode.Backspace))
                return true;
            return false;
        }
    }
}
