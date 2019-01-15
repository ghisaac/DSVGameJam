using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/PreRound")]
    public class GGR_PreRound : GGR_State
    {
        public float showPictureLength;
        

        private float currentTime;
        private GGR_Location currentLocation;

        public override void Enter()
        {
            GGR_GameData.FreezeAllPlayers();
            for(int i = 0; i < GGR_GameData.GetAllPlayers().Count; i++)
            {
                GGR_GameData.SetPlayerPosition(GGR_GameData.GetAllPlayers()[i], GGR_GameData.GetSpawnPosition(i));
            }
            currentTime = 0;
            currentLocation = GGR_GameData.GetNextLocation();
        }

        public override bool Run()
        {
            if(currentTime >= showPictureLength)
            {
                Owner.TransitionTo<GGR_Round>();
            }
            currentLocation.Render();
            currentTime += Time.deltaTime;
            return false;
        }

    }
}