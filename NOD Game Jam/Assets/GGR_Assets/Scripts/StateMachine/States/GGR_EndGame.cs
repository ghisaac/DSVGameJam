using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/EndGame")]
    public class GGR_EndGame : GGR_State
    {
        public override bool Run()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("THE END!");
                return true;
            }
            return false;
        }
    }
}