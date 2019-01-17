using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/PostRound")]
    public class GGR_PostRound : GGR_State
    {
        public LineRenderer lineRendererPrefab;
        public float drawSpeed;
        public ParticleSystem confetti;
        private GGR_Location goalLocation;
        private Dictionary<Player, Vector3[]> playerPaths;
        private bool postRoundDone = false;
        private Dictionary<Player, float> roundScores;

        public override void Enter()
        {
            postRoundDone = false;
            playerPaths = new Dictionary<Player, Vector3[]>();
            roundScores = new Dictionary<Player, float>();
            Debug.Log("Round ended, entering post round");
            goalLocation = GGR_GameData.DequeueCurrentLocation();
            GGR_GameData.FreezeAllPlayers();
            SetAllPlayerPaths();
            GGR_GameData.instance.timesUp.SetActive(true);
            foreach (Player player in playerPaths.Keys)
            {
                float score = GGR_Helper.GetPathLength(playerPaths[player]);
                GGR_GameData.GivePlayerScore(player, score);
                roundScores.Add(player, score);
            }
            GGR_CameraMovement.instance.ZoomInToPosition(goalLocation.position, ZoomDone);
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
            GGR_GameData.instance.StartCoroutine(ConfettiCloud());
            GGR_GameData.instance.StartCoroutine(DrawPaths(() => postRoundDone = true));
            GGR_GameData.instance.timesUp.SetActive(false);
        }

        private IEnumerator ConfettiCloud()
        {
            ParticleSystem particles = ObjectPool.Instantiate(confetti.gameObject, goalLocation.position).GetComponent<ParticleSystem>();
            particles.Play();
            SoundManager.Instance.PlayVictorySound();
            while (particles.IsAlive())
                yield return null;

            ObjectPool.Destroy(particles.gameObject);
        }

        private IEnumerator DrawPaths(Action callback)
        {
            List<LineRenderer> lineRenderers = new List<LineRenderer>();
            for(int i = 0; i < playerPaths.Keys.Count; i++)
            {
                lineRenderers.Add(ObjectPool.Instantiate(lineRendererPrefab.gameObject).GetComponent<LineRenderer>());
                lineRenderers[i].material = Instantiate(lineRenderers[i].material);
                lineRenderers[i].material.color = playerPaths.Keys.ToList()[i].PlayerColor;

            }

            List<Player> playerList = playerPaths.Keys.ToList();
            float currentTime = 0;
            while(currentTime < GetMaxPathDistance()/drawSpeed)
            {
                Debug.Log("drawing");
                for(int i = 0; i < lineRenderers.Count; i++)
                {
                    int corners = 0;
                    Vector3[] path = playerPaths[playerList[i]];
                    Vector3? endPos = GGR_Helper.GetPathPositionByFactor((currentTime * drawSpeed) / GGR_Helper.GetPathLength(path), path, out corners);
                    if (endPos == null)
                        continue;

                    lineRenderers[i].positionCount = corners + 1;
                    for(int j = 0; j < corners; j++)
                    {
                        lineRenderers[i].SetPosition(j, path[j]);
                    }
                    lineRenderers[i].SetPosition(corners, (Vector3)endPos);
                }



                currentTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(5);
            ShowScore();
           foreach(LineRenderer lr in lineRenderers)
           {
                lr.positionCount = 0;
                ObjectPool.Destroy(lr.gameObject);
           }
            callback();
       

        }

        private float GetMaxPathDistance() {
            float maxDistance = float.NegativeInfinity;
            foreach(Vector3[] path in playerPaths.Values)
            {
                float distance = GGR_Helper.GetPathLength(path);
                if (distance > maxDistance)
                    maxDistance = distance;
            }
            Debug.Log(maxDistance);
            return maxDistance;
        }

        private void ShowScore()
        {
            foreach(Player p in roundScores.Keys)
            {
                Debug.Log("round score"+" name" + p.Name + "score:" + roundScores[p]);
                Debug.Log("total score" + p.Name + "score:" + GGR_GameData.GetPlayerScore(p));
            }
            
        }
    }
}
