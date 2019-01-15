using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/StartGame")]
    public class GGR_StartGame : GGR_State
    {

        public override bool Run()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
                Owner.TransitionTo<GGR_RunGame>();
                
            return false;
        }
    }
}