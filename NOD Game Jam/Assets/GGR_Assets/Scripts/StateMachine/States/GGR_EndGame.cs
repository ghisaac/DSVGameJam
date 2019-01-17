using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/EndGame")]
    public class GGR_EndGame : GGR_State
    {
        public override bool Run()
        {
            List<Player> allPlayers = Player.AllPlayers;
            allPlayers = allPlayers.OrderBy(p => GGR_GameData.GetPlayerScore(p)).ToList();
            Player.DistributePoints(allPlayers.ToArray());
            SceneManager.LoadScene("ScoreScreenScene");
            return true;
        }
    }
}