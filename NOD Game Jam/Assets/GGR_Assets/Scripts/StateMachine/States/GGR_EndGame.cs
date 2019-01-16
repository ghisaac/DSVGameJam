using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/EndGame")]
    public class GGR_EndGame : GGR_State
    {
        public override bool Run()
        {
            
            //Player.DistributePoints();
            SceneManager.LoadScene("ScoreScreenScene");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("THE END!");
                return true;
            }
            return false;
        }
    }
}