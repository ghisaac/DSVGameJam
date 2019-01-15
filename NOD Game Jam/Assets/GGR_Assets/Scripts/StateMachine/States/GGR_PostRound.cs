using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/PostRound")]
    public class GGR_PostRound : GGR_State
    {
        private GGR_Location goalLocation;
        private Dictionary<Player, Vector3[]> playerPaths;
        private bool postRoundDone = false;

        public override void Enter()
        {
            postRoundDone = false;
            playerPaths = new Dictionary<Player, Vector3[]>();
            Debug.Log("Round ended, entering post round");
            goalLocation = GGR_GameData.DequeueCurrentLocation();
            Camera.main.GetComponent<GGR_CameraMovement>().ZoomInToPosition(goalLocation.position, ZoomDone);
            GGR_GameData.FreezeAllPlayers();
            SetAllPlayerPaths();
            foreach (Player player in playerPaths.Keys)
            {
                GGR_GameData.GivePlayerScore(player, GGR_Helper.GetPathLength(playerPaths[player]));
            }
        }

        public override bool Run()
        {
            if (!postRoundDone)
                return false;

            if (GGR_GameData.GetLocationsRemaining() == 0)
                return true;
            else
            {
                Owner.TransitionTo<GGR_PreRound>();
                return false;
            }
        }

        private void SetAllPlayerPaths()
        {
            foreach (PlayerController pc in GGR_GameData.GetAllPlayerControllers())
            {
                NavMeshPath path = new NavMeshPath();
                NavMesh.CalculatePath(GGR_Helper.PositionOnPlane(pc.transform.position), goalLocation.position, NavMesh.AllAreas, path);
                playerPaths.Add(pc.myPlayer, path.corners);
            }
        }

        private void ZoomDone()
        {
            //poff fyverkerier
            GGR_GameData.instance.StartCoroutine(DrawPaths(() => postRoundDone = true));
        }

        private IEnumerator DrawPaths(Action callback)
        {

            callback();

        }
    }
}
