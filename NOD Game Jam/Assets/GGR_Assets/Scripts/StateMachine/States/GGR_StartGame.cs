using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/StartGame")]
    public class GGR_StartGame : GGR_State
    {
        public override void Enter()
        {
            GGR_GameData.FindPlayers();
            /*
            Player johannaPlayer = new Player(0, "Johanna");
            Player danielPlayer = new Player(1, "Daniel");
            GGR_GameData.SpawnPlayer(johannaPlayer);
            GGR_GameData.SpawnPlayer(danielPlayer);
            */
        }

        public override bool Run()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Owner.TransitionTo<GGR_RunGame>();
                
            return false;
        }
    }
}